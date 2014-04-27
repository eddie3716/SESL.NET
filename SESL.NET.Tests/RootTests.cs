using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
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

			int temp1 = 0;
			externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName(variableName, out temp1, out temp1))
				.OutRef(variableKey)
				.Return(true);

			var temp = Value.Void;
			var tempOperands = new Value[0];
			externalFunctionValueProvider.Expect(context => context.TryGetExternalFunctionValue(variableKey, out temp, tempOperands))
				.OutRef(new Value(variableValue)).Repeat.Times(2)
				.Return(true).Repeat.Times(2);

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

			int temp1 = 0;
			externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName(variableName, out temp1, out temp1))
				.OutRef(variableKey).Repeat.Times(4)
				.Return(true).Repeat.Times(4);
   
			var temp = Value.Void;
			var tempOperands = new Value[0];
			externalFunctionValueProvider.Expect(context => context.TryGetExternalFunctionValue(variableKey, out temp, tempOperands))
				.OutRef(new Value(variableValue)).Repeat.Times(4)
				.Return(true).Repeat.Times(4);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var result = compiledFunction.Root(externalFunctionValueProvider, variableKey, new Value(0.1), 10);

			Assert.AreEqual(0.618033988749895, result.ToDouble());
		}
	}
}
