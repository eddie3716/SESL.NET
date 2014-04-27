using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SESL.NET.Function;
using SESL.NET.Compilation;
using SESL.NET.InfixNotation;

namespace SESL.NET.Test
{
	[TestFixture]
	public class NumericalDerivativeTests
	{

		[Test]
		public void NumericalDerivative_ExponentWithDelta()
		{
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

			string expression = "MyVariable^2 + 1";
			string variableName = "myvariable";
			int variableKey = 0;
			var variableValue = 5.0;
			var delta = 0.0001;

			int temp1 = 0;
			externalFunctionKeyProvider.Stub(context => context.TryGetExternalFunctionKeyFromName(variableName, out temp1, out temp1))
				.OutRef(variableKey)
				.Return(true);

			var temp = new Value(0.0);
			externalFunctionValueProvider.Stub(context => context.TryGetExternalFunctionValue(variableKey, out temp))
				.IgnoreArguments()
				.OutRef(new Value(variableValue)).Repeat.Times(2)
				.Return(true).Repeat.Times(2);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var numericalDerivative = compiledFunction.NumericalDerivative(variableKey, new Value(delta));

			var result = numericalDerivative.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(10, result.ToInt32());
		}

		[Test]
		public void NumericalDerivative_Exponent()
		{
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

			string expression = "MyVariable^2 + 1";
			string variableName = "myvariable";
			int variableKey = 0;
			var variableValue = 5.0;

			int temp1 = 0;
			externalFunctionKeyProvider.Stub(context => context.TryGetExternalFunctionKeyFromName(variableName, out temp1, out temp1))
				.OutRef(variableKey)
				.Return(true);

			var temp = Value.Zero;
			externalFunctionValueProvider.Stub(context => context.TryGetExternalFunctionValue(variableKey, out temp))
				.IgnoreArguments()
				.OutRef(new Value(variableValue)).Repeat.Times(2)
				.Return(true).Repeat.Times(2);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var numericalDerivative = compiledFunction.NumericalDerivative(variableKey);

			var result = numericalDerivative.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(10, result.ToInt32());
		}
	}
}
