using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function;
using SESL.NET.Compilation;
using SESL.NET.Syntax;
using SESL.NET.Exception;
using SESL.NET.Function.Commands;

namespace SESL.NET.InfixNotation
{
	public class InfixNotationToJavaScript
	{
		private static string[] JavaScriptKeyWords = new string[] { "null", "true", "false", "nothing", "undefined", "function", "new" };


		public InfixNotationToJavaScript()
		{
		}

		public string Convert<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider, string expression)
		{
			var scanner = new InfixNotationScanner(expression);
			var lexer = new InfixNotationLexer(new InfixNotationGrammar(), scanner);
			var parser = new InfixNotationParser(lexer);

			var functionNodes = parser.GetFunctionNodes(externalFunctionKeyProvider);

			var function = functionNodes.ToFunction(); 

			var javascriptSignature = BuildJavaScriptParameterSignature(function);

			var javascriptBody = function.Evaluate(new NoOpExternalFunctionValueProvider<TExternalFunctionKey>(), new ToJavaScriptFunctionCommand<TExternalFunctionKey>()).ToString();

			var javascriptFunction = String.Format("function({0}) {{ return ({1}); }}", javascriptSignature, javascriptBody);

			return javascriptFunction;
		}

		private static string BuildJavaScriptParameterSignature<TExternalFunctionKey>(Function<TExternalFunctionKey> function)
		{
			var externalFunctionDependencyNames = function.GetAllDistinctParametersNames();

			return BuildJavaScriptParameterSignature(externalFunctionDependencyNames);
		}

		public static string BuildJavaScriptParameterSignature(IEnumerable<string> externalFunctionDependencyNames)
		{
			var stringBuilderFunctionSignature = new StringBuilder();

			foreach (var externalFunctionDependencyName in externalFunctionDependencyNames.Where(n => !JavaScriptKeyWords.Any(kw => kw == n)))
			{
				stringBuilderFunctionSignature.AppendFormat("{0}, ", externalFunctionDependencyName);
			}

			if (externalFunctionDependencyNames.Count() > 0)
			{
				stringBuilderFunctionSignature.Remove(stringBuilderFunctionSignature.Length - 2, 2);
			}

			return stringBuilderFunctionSignature.ToString();
		}
	}
}
