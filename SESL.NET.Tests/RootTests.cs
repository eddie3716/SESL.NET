using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using SESL.NET;
using SESL.NET.Function;
using SESL.NET.Compilation;
using SESL.NET.InfixNotation;

namespace SESL.NET.Test
{
	[TestFixture]
	public class RootTests
	{
		[Test]
		public void Root_Exponent()
		{
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

			List<IExternalFunctionValueProvider<int>> summationContexts = new List<IExternalFunctionValueProvider<int>>();
			string expression = "MyVariable^2 - 1";
			string variableName = "myvariable";
			int variableKey = 1;
			int variableValue = 5;

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);

			var temp = Value.Void;
			var tempOperands = new Value[0];
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out temp, tempOperands)
				.Returns(
					x =>
					{
						x[1] = new Value(variableValue);
						return true;
					}
				);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var result = compiledFunction.Root(externalFunctionValueProvider, variableKey, new Value(0.1), 10);

			Assert.AreEqual(1.0, result.ToDouble());
		}

		[Test]
		public void Root_Exponent2()
		{
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

			string expression = "MyVariable^2 + MyVariable - 1";
			string variableName = "myvariable";
			int variableKey = 1;
			int variableValue = 5;

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);
   
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Value>(), Array.Empty<Value>())
				.Returns(
					x =>
					{
						x[1] = new Value(variableValue);
						return true;
					}
				);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var result = compiledFunction.Root(externalFunctionValueProvider, variableKey, new Value(0.1), 10);

			Assert.AreEqual(0.618033988749895, result.ToDouble());
		}
	}
}
