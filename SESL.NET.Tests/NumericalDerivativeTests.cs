using NUnit.Framework;
using NSubstitute;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests
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

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);

			var temp = new Value(0.0);
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Value>())
				.Returns(
					x =>
					{
						x[1] = new Value(variableValue);
						return true;
					}
				);

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

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);
				
			var temp = Value.Zero;
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Value>())
				.Returns(
					x =>
					{
						x[1] = new Value(variableValue);
						return true;
					}
				);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var numericalDerivative = compiledFunction.NumericalDerivative(variableKey);

			var result = numericalDerivative.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(10, result.ToInt32());
		}
	}
}
