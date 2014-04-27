using System;
using System.Collections;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using SESL.NET.Exception;
using System.Diagnostics;
using SESL.NET.Compilation;
using SESL.NET.Syntax;
using SESL.NET.Function.Commands;


namespace SESL.NET.Function
{
	public class Function<TExternalFunctionKey>
	{
		private IList<FunctionNode<TExternalFunctionKey>> _functionNodes;

		#region Properties

		internal IList<FunctionNode<TExternalFunctionKey>> FunctionNodes
		{
			get
			{
				return _functionNodes;
			}
		}

		#endregion

		public Function(IList<FunctionNode<TExternalFunctionKey>> functionNodes)
		{
			_functionNodes = functionNodes;
		}

		public Value Evaluate(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, IFunctionCommand<TExternalFunctionKey> functionCommand)
		{
			try
			{
				return EvaluateFunction(externalFunctionValueProvider, functionCommand);
			}
			catch (FunctionException)
			{
				throw;
			}
			catch (System.DivideByZeroException)
			{
				throw;
			}
			catch (System.Exception ex)
			{
				throw new FunctionException(String.Format("Exception while evaluating function: {0}", ex.Message), ex);
			}
		}

		public Value Evaluate(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider)
		{
			return this.EvaluateFunction(externalFunctionValueProvider, new AutomaticFunctionCommand<TExternalFunctionKey>());
		}

		private Value EvaluateFunction(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, IFunctionCommand<TExternalFunctionKey> functionCommand)
		{
			if (externalFunctionValueProvider == null)
			{
				throw new NullExternalFunctionValueProviderException();
			}

			var results = new Stack<Value>();

			var operandBuffer = new Value[0];

			foreach (var functionNode in this._functionNodes)
			{
				if (results.Count < functionNode.Semantics.Operands)
				{
					throw new InsufficientOperandsException(functionNode.Semantics.Operands, results.Count, functionNode.Semantics.ToString());
				}
				else
				{
					operandBuffer = new Value[functionNode.Semantics.Operands];
				}

				for (int i = functionNode.Semantics.Operands - 1; i >= 0; --i)
				{
					operandBuffer[i] = results.Pop();
				}

				try
				{
					var value = functionCommand.Execute(functionNode, externalFunctionValueProvider, operandBuffer);

					if (!value.IsVoid)
					{
						results.Push(value);
					}
				}
				catch (FunctionReturnException fre)
				{
					var returnResult = fre.Result;

					while (results.Count > 0)
					{
						results.Pop();
					}

					return returnResult;
				}
				catch
				{
					throw;
				}
			}

			if (results.Count != 1)
			{
				throw new InvalidFunctionResultException(results.Count);
			}
			
			var finalResult = results.Pop();

			return finalResult;
		}

		public override string ToString()
		{
			var builder = FunctionNodes.Aggregate(new StringBuilder(), (sb, fn) => sb.AppendFormat("{0} ", fn));

			return builder.Length > 0 ? builder.ToString() : "Empty Function";
		}

		public Value Root(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, TExternalFunctionKey functionKey, Value initialValue, int iterations)
		{
			return this.Root(externalFunctionValueProvider, functionKey, initialValue, iterations, Value.Delta);
		}

		public Value Root(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, TExternalFunctionKey functionKey, Value initialValue, int iterations, Value delta)
		{
			var cachedExternalFunctionValueProvider = new CachedExternalFunctionValueProvider<TExternalFunctionKey>(externalFunctionValueProvider);
			if (cachedExternalFunctionValueProvider.ContainsKey(functionKey))
			{
				cachedExternalFunctionValueProvider[functionKey] = initialValue;
			}
			else
			{
				cachedExternalFunctionValueProvider.Add(functionKey, initialValue);
			}

			var derivative = this.NumericalDerivative(functionKey, delta);

			for (int i = 0; i < iterations; i++)
			{

				cachedExternalFunctionValueProvider[functionKey] -= (this.Evaluate(cachedExternalFunctionValueProvider) / derivative.Evaluate(cachedExternalFunctionValueProvider));
			}

			return cachedExternalFunctionValueProvider[functionKey];
		}

		public Function<TExternalFunctionKey> NumericalDerivative(TExternalFunctionKey functionKey)
		{
			return this.NumericalDerivative(functionKey, Value.Delta);
		}

		public Function<TExternalFunctionKey> NumericalDerivative(TExternalFunctionKey functionKey, Value delta)
		{
			var functionNodes = new List<FunctionNode<TExternalFunctionKey>>();

			foreach (var token in this.CloneTokensWithVariableModifier(functionKey, delta / new Value(2.0)))
			{
				functionNodes.Add(token);
			}

			foreach (var token in this.CloneTokensWithVariableModifier(functionKey, -delta / new Value(2.0)))
			{
				functionNodes.Add(token);
			}

			var minusToken = new FunctionNode<TExternalFunctionKey>
			{
				Semantics = new TokenSemantics(TokenType.Minus, 2),
			};
			
			var deltaToken = new FunctionNode<TExternalFunctionKey>
			{
				Semantics = new TokenSemantics(TokenType.Value),
				Value = delta,
			};

			var divideToken = new FunctionNode<TExternalFunctionKey>
			{
				Semantics = new TokenSemantics(TokenType.Divide, 2),
			};

			functionNodes.Add(minusToken);
			functionNodes.Add(deltaToken.Clone());
			functionNodes.Add(divideToken);

			return functionNodes.ToFunction();
		}

		internal IList<FunctionNode<TExternalFunctionKey>> CloneTokensWithVariableModifier(TExternalFunctionKey functionKey, Value modifier)
		{
			var functionNodes = new List<FunctionNode<TExternalFunctionKey>>();
			foreach (var functionNode in this._functionNodes)
			{
				if (functionNode.Semantics.Type == TokenType.ExternalFunction && functionNode.ExternalFunctionKey.Equals(functionKey))
				{
					var modifierToken = new FunctionNode<TExternalFunctionKey>
					{
						Semantics = new TokenSemantics(TokenType.Value),
						Value = modifier,
					};

					var plusToken = new FunctionNode<TExternalFunctionKey>
					{
						Semantics = new TokenSemantics(TokenType.Plus, 2),
					};

					functionNodes.Add(modifierToken);
					functionNodes.Add(functionNode.Clone());
					functionNodes.Add(plusToken);
				}
				else if (functionNode.Functions.Count > 0)
				{
					functionNodes.Add(functionNode.CloneForDerivative(functionKey, modifier));
				}
				else
				{
					functionNodes.Add(functionNode.Clone());
				}
			}
			return functionNodes;
		}

		internal Function<TExternalFunctionKey> CloneFunctionWithVariableModifier(TExternalFunctionKey functionKey, Value modifier)
		{
			var newFunctionNodes = new List<FunctionNode<TExternalFunctionKey>>();
			foreach (var functionNode in this.CloneTokensWithVariableModifier(functionKey, modifier))
			{
				newFunctionNodes.Add(functionNode);
			}
			return newFunctionNodes.ToFunction();
		}

		internal Function<TExternalFunctionKey> Clone()
		{
			var newFunctionNodes = new List<FunctionNode<TExternalFunctionKey>>(this._functionNodes.Count);

			foreach (var functionNode in this._functionNodes)
			{
				newFunctionNodes.Add(functionNode.Clone());
			}

			return newFunctionNodes.ToFunction();
		}
	}
}