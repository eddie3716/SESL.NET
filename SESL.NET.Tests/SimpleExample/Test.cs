using System;
using NUnit.Framework;

namespace SESL.NET.Tests.SimpleExample;

[TestFixture]
public class ReallyImportantClass
{
	[Test]
	public void DoStuff()
	{
		var functionText = "if (42, 1 + FooBarValues / (FooTwoValues(2,3) - BarThreeValues(4,5,6)), 'TurkeyBurgers') ";

		var function = new InfixNotation.InfixNotationCompiler().Compile(new MyExternalFunctionKeyProvider(), functionText);

		var value = function.Evaluate(new MyExternalFunctionValueProvider());

		Console.WriteLine(value.ToString());
	}
}