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
			var variableValue = 5.0m;
			var delta = 0.0001m;

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);

			var temp = new Variant(0.0m);
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Variant>())
				.Returns(
					x =>
					{
						x[1] = new Variant(variableValue);
						return true;
					}
				);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var numericalDerivative = compiledFunction.NumericalDerivative(variableKey, new Variant(delta));

			var result = numericalDerivative.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(10m, result.ToNumeric());
		}

		[Test]
		public void NumericalDerivative_Exponent()
		{
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

			string expression = "MyVariable^2 + 1";
			string variableName = "myvariable";
			int variableKey = 0;
			var variableValue = 5.0m;

			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableName, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = variableKey;
						return true;
					}
				);
				
			var temp = new Variant(0);
			externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Variant>())
				.Returns(
					x =>
					{
						x[1] = new Variant(variableValue);
						return true;
					}
				);

			var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var numericalDerivative = compiledFunction.NumericalDerivative(variableKey);

			var result = numericalDerivative.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(10m, result.ToNumeric());
		}
	}
}
