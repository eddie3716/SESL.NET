using NUnit.Framework;
using NSubstitute;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests;

[TestFixture]
public class JavascriptTest
{
	[Test]
	public void TestMethod1()
	{
		string expression = "BOb > (5/10*2)";
		string variableKey = "bob";
		int functionId = 0;

		var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
		externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableKey, out Arg.Any<int>(), out Arg.Any<int>())
			.Returns(
				x =>
				{
					x[1] = functionId;
					x[2] = 0;
					return true;
				}
			);

		var javascript = InfixNotationToJavaScript.Convert(externalFunctionKeyProvider, expression);

		Assert.IsTrue(javascript.Length > 0);
	}
}