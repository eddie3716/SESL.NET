using System;
using System.Collections.Generic;
using System.Linq;
using SESL.NET.Syntax;
using SESL.NET.InfixNotation;

namespace SESL.NET.Function.Commands
{
    internal class ToJavaScriptFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		private static readonly Dictionary<TokenType, string> TokenToJavaScriptSyntaxMap = new()
        {
			{TokenType.ExternalFunction, "{0}"}
			,{TokenType.Value, "{0}"}
			,{TokenType.Plus, "Number(Number({0} + {1}).toFixed(2))"}
			,{TokenType.Minus, "Number(Number({0} - {1}).toFixed(2))"}
			,{TokenType.Multiply, "Number(Number({0} * {1}).toFixed(2))"}
			,{TokenType.Divide, "Number(Number({0} / {1}).toFixed(2))"}
			,{TokenType.UnaryMinus, "-({0})"}
			,{TokenType.GreaterThan, "({0} > {1})"}
			,{TokenType.GreaterThanOrEqual, "({0} >= {1})"}
			,{TokenType.LessThan, "({0} < {1})"}
			,{TokenType.LessThanOrEqual, "({0} <= {1})"}
			,{TokenType.NotEqual, "({0} != {1})"}
			,{TokenType.Equal, "({0} == {1})"}
			,{TokenType.And, "({0} && {1})"}
			,{TokenType.Or, "({0} || {1})"}
			,{TokenType.Return, "return ({0})"}
			,{TokenType.Modulus, "Number(Number({0} % {1}).toFixed(2))"}
			,{TokenType.If, "(function({0}) {{ if ({1}) {{ return ({2}); }} else {{ return ({3}); }} }})({0})"}
			//{TokenType.Case, "(function({0}) {{ if ({1}) {{ return ({2}); }} }})({0})"},
			//{TokenType.IsError, "(function({0}) {{ try {{ {1} return false; }} catch {{ return true; }} }})({0})"}
		};

		public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
		{
			if (TokenToJavaScriptSyntaxMap.ContainsKey(functionNode.Semantics.Type))
			{
				var formatString = TokenToJavaScriptSyntaxMap[functionNode.Semantics.Type];

				var formatStringParams = Array.Empty<string>();
				 
				if (functionNode.Semantics.IsOperator)
				{
					formatStringParams = operands.Take(functionNode.Semantics.Operands).Select(v => v.ToString()).ToArray();
				}
				else if (functionNode.Functions.Any())
				{
					var parameters = functionNode.Functions.SelectMany(f => f.GetAllDistinctParametersNames()).Distinct();
					var firstFormatParam = InfixNotationToJavaScript.BuildJavaScriptParameterSignature(parameters);
					formatStringParams = Enumerable.Repeat(firstFormatParam, 1).Union(functionNode.Functions.Select(f => f.Evaluate(externalFunctionValueProvider, this)).Select(v => v.ToString())).ToArray();
				}
				else
				{
					formatStringParams = new string[] { functionNode.Variant.ToString() };
				}

				var result = string.Format(formatString, formatStringParams);

				return new Variant(result);
			}
			else
			{
				throw new IndexOutOfRangeException(string.Format("Token {0} may not be converted to javascript.", functionNode.Semantics.Type));
			}
		}
	}
}
