using System.Collections.Generic;
using System.Linq;
using SESL.NET.Function;
using SESL.NET.Exception;
using SESL.NET.Compilation;
using SESL.NET.Syntax;

namespace SESL.NET.InfixNotation
{
    public class InfixNotationParser : IParser
	{

		private readonly ILexer _lexer;

		private Token? _useThisToken;

		public InfixNotationParser(ILexer lexer)
		{
			_lexer = lexer;
			_useThisToken = new Token?();
		}

		public IList<FunctionNode<TExternalFunctionKey>> GetFunctionNodes<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider)
		{
			var queue = new Queue<FunctionNode<TExternalFunctionKey>>();
			var stack = new Stack<FunctionNode<TExternalFunctionKey>>();

			while (_useThisToken.HasValue || _lexer.Next())
			{
				var token = _useThisToken.HasValue ? _useThisToken.Value : _lexer.GetToken();
				_useThisToken = new Token?();
                ProcessToken(externalFunctionKeyProvider, queue, stack, token);
			}

            FinalizeQueue(queue, stack);

			return queue.ToList();
		}

		private static void FinalizeQueue<TExternalFunctionKey>(Queue<FunctionNode<TExternalFunctionKey>> queue, Stack<FunctionNode<TExternalFunctionKey>> stack)
		{
			// While there are still operator tokens in the stack:
			while (stack.Count != 0)
			{
				var optoken = stack.Pop();
				// If the operator token on the top of the stack is a parenthesis
				if (optoken.Semantics.Type == TokenType.LeftParenthesis)
				{
					// then there are mismatched parenthesis.
					throw new CompilerException("Unbalanced parenthesis.");
				}
				else
				{
					// Pop the operator onto the output queue.
					queue.Enqueue(optoken);
				}
			}
		}

		private void ProcessToken<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider, Queue<FunctionNode<TExternalFunctionKey>> queue, Stack<FunctionNode<TExternalFunctionKey>> stack, Token token)
		{
			var semantics = token.Semantics;
			var tokenType = semantics.Type;

			var tokenName = token.Name;
			var nestedFunctions = semantics.NestedFunctions;

			var functionNode = new FunctionNode<TExternalFunctionKey>()
			{
				Semantics = semantics
			};

			switch (tokenType)
			{
				case TokenType.LeftParenthesis:
					ApplyOrderOfOperations(ref functionNode, queue, stack);
					break;
				case TokenType.RightParenthesis:
					if (stack.Count > 0)
					{
						var opsFunctionNode = stack.Peek();
						// Until the token at the top of the stack is a left parenthesis
						while (opsFunctionNode.Semantics.Type != TokenType.LeftParenthesis)
						{
							// pop operators off the stack onto the output queue
							queue.Enqueue(stack.Pop());
							if (stack.Count > 0)
							{
								opsFunctionNode = stack.Peek();
							}
							else
							{
								// If the stack runs out without finding a left parenthesis,
								// then there are mismatched parentheses.
								throw new CompilerException("Unable to match a left parenthesis with a right parenthesis.");
							}
						}
						// Pop the left parenthesis from the stack, but not onto the output queue.
						stack.Pop();
					}
					break;
				case TokenType.Value:
					decimal decimalValue;
					double doubleValue;
					int intValue;
					
					if (int.TryParse(tokenName, out intValue))
					{
						functionNode.Value = new Value(intValue);
					}
					else if (decimal.TryParse(tokenName, out decimalValue))
					{
						functionNode.Value = new Value(decimalValue);
					}
					else if (double.TryParse(tokenName, out doubleValue))
					{
						functionNode.Value = new Value(doubleValue);
					}
					else
					{
						functionNode.Value = new Value(tokenName);
					}
					queue.Enqueue(functionNode);
					break;
				case TokenType.ExternalFunction:
					TExternalFunctionKey parameterKey;
					int numberOfOperandsNeeded;
					if (externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(tokenName, out parameterKey, out numberOfOperandsNeeded))
					{
						functionNode.ExternalFunctionKey = parameterKey;
						functionNode.Value = new Value(tokenName);
						functionNode.Semantics = new TokenSemantics(semantics.Type, numberOfOperandsNeeded, semantics.NestedFunctions);
					}
					else
					{
						throw new ExternalFunctionKeyNotFoundException(string.Format("Parameter {0} is not handled by the compiler context", tokenName));
					}
					queue.Enqueue(functionNode);
					break;
				default:
					if (semantics.IsOperator)
					{
						ApplyOrderOfOperations(ref functionNode, queue, stack);
					}
					else if (nestedFunctions > 0)
					{
						for (int i = 0; i < nestedFunctions; i++)
						{
							functionNode.Functions.Add(GetNestedFunctionNodes(externalFunctionKeyProvider).ToFunction());
						}

						queue.Enqueue(functionNode);
					}
					break;
			}
		}

		private static void ApplyOrderOfOperations<TExternalFunctionKey>(
			ref FunctionNode<TExternalFunctionKey> functionNode,
			Queue<FunctionNode<TExternalFunctionKey>> output,
			Stack<FunctionNode<TExternalFunctionKey>> ops)
		{
			if (ops.Count > 0)
			{
				int functionNodePrecedence = functionNode.Semantics.Precedence;

				int opsFunctionNodePrecedence;

				var opsFunctionNode = ops.Peek();

				while (opsFunctionNode.Semantics.IsOperator && (opsFunctionNodePrecedence = opsFunctionNode.Semantics.Precedence) > 0)
				{
					if (opsFunctionNodePrecedence > functionNodePrecedence)
					{
						break;
					}
					else
					{
						output.Enqueue(ops.Pop());
						if (ops.Count > 0)
						{
							opsFunctionNode = ops.Peek();
						}
						else
						{
							break;
						}
					}
				}
			}
			ops.Push(functionNode);
		}

		public IList<FunctionNode<TExternalFunctionKey>> GetNestedFunctionNodes<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider)
		{
			// Extracts a sub function.  We want to build the things between function's 
			// parens into a function of its own.

			var queue = new Queue<FunctionNode<TExternalFunctionKey>>();
			var stack = new Stack<FunctionNode<TExternalFunctionKey>>();

			if (_useThisToken != null || _lexer.Next())
			{
				int parenthesisCount = 0;
				var token = _useThisToken != null ? _useThisToken.Value : _lexer.GetToken();
				_useThisToken = null;

				if (token.Semantics.Type == TokenType.LeftParenthesis)
				{
                    ProcessToken(externalFunctionKeyProvider, queue, stack, token);
					parenthesisCount++;

					while (parenthesisCount > 0 && _lexer.Next())
					{
						token = _lexer.GetToken();

						if ((parenthesisCount == 1) && token.Semantics.Type == TokenType.Comma)
						{
                            ProcessToken(externalFunctionKeyProvider, queue, stack, new Token(")", new TokenSemantics(TokenType.RightParenthesis, 0)));
							_useThisToken = new Token("(", new TokenSemantics(TokenType.LeftParenthesis, 0));
							break;
						}
						else
						{
                            ProcessToken(externalFunctionKeyProvider, queue, stack, token);

							// End of some parameter list
							if (token.Semantics.Type == TokenType.RightParenthesis)
							{
								parenthesisCount--;
							}
							// Beginning of an inner parameter list
							else if (token.Semantics.Type == TokenType.LeftParenthesis)
							{
								parenthesisCount++;
							}
						}
					}
				}
				else
				{
					throw new CompilerException("Nested function must begin with parenthesis");
				}
			}
			else
			{
				throw new CompilerException("Unable to construct nested function");
			}

            FinalizeQueue(queue, stack);

			return queue.ToList();
		}
	}
}
