using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SESL.NET.Compilation;
using SESL.NET.Function;
using NUnit.Framework;

namespace SESL.NET.Tests
{
	[TestFixture]
	public class ReallyImportantClass
	{
		[Test]
		public void DoStuff()
		{
			var functionText = "if (42, 1 + FooBarValues / (FooTwoValues(2,3) - BarThreeValues(4,5,6)), 'TurkeyBurgers') ";

			var function = new InfixNotation.InfixNotationCompiler().Compile<ExternalFunctionEnum>(new MyExternalFunctionKeyProvider(), functionText);

			var value = function.Evaluate(new MyExternalFunctionValueProvider());

			Console.WriteLine(value.ToString());
		}
	}
}
