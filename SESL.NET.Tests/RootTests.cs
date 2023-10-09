using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using SESL.NET.Function;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests;

[TestFixture]
public class RootTests
{
	[Test]
	public void Root_Exponent()
	{
		var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
		var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();

		List<IExternalFunctionValueProvider<int>> summationContexts = new();
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

		var temp = Variant.Void;
		var tempOperands = Array.Empty<Variant>();
		externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out temp, tempOperands)
			.Returns(
				x =>
				{
					x[1] = new Variant(variableValue);
					return true;
				}
			);

		var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

		var result = compiledFunction.Root(externalFunctionValueProvider, variableKey, new Variant(0.1m), 10);

		Assert.AreEqual(1.0m, result.DecimalValue);
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

		externalFunctionValueProvider.TryGetExternalFunctionValue(variableKey, out Arg.Any<Variant>(), Array.Empty<Variant>())
			.Returns(
				x =>
				{
					x[1] = new Variant(variableValue);
					return true;
				}
			);

		var compiledFunction = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

		var result = compiledFunction.Root(externalFunctionValueProvider, variableKey, new Variant(0.1m), 10);

		Assert.AreEqual(0.6180339887498948482045868344m, result.DecimalValue);
	}
}