# Welcome

Simple Embedded Scripting Language (SESL.NET) is a weakly-typed, functional scripting language that you can use to add modest scripting capabilities to your .NET applications.  The last iteration is the 5th generation of a scripting library I have been working with on and off for 8 years (from 2006 to 2014).

SESL.NET started off as a project to learn how compilers (should) really work.  I think the design is fairly straight forward and easy to follow for compiler novices who also want to learn about compilers work.  However, for this day and age, for adding scripting capability to my applications, I'd probably use https://github.com/StefH/System.Linq.Dynamic.Core or some other Roslyn-based system, but for its time, circa 2006 to 2014, SESL.NET was adequate for what I needed.

SESL.NET is not nearly as complete or powerful as using some kind of mature embedded scripting language, but it supports *enough* features, allows for data interaction and manipulation within the .NET application, and is easy enough for business analysts to use.

## Features

* Weakly-typed!  A variant can be anything, and can change from one type to the next depending on the operation being performed.  Its like opening an Easter Egg!

* Functional.  Every function and every operation returns another value, even the if statement.  Recursive operations are supported, in principle, but will require custom coding in your implementation of the IExternalFunctionValueProvider.

* Simple branching logic, i.e. we support the if statement.

* Arithmetic

* Boolean logic

* Numerical Derivatives (using Newton's Method)

* Root finding

* Extensible through your own custom functions
** This is the linchpin of the system!  You tell SESL.NET what your special identifiers are, and how many operands they take, and you can plug in data and special functionality from any source your developers have access to.

## Example Usage

Here's an example of some C#code:

```
#!C#

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SESL.NET.Compilation;
using SESL.NET.Function;

namespace SESL.NET.Tests
{
	public enum ExternalFunctionEnum
	{
		FunctionNotRecognized,
		FooTwoValues,
		BarThreeValues,
		FooBarValues
	}

	public class MyExternalFunctionKeyProvider : IExternalFunctionKeyProvider<ExternalFunctionEnum>
	{
		public bool TryGetExternalFunctionKeyFromName(string externalFunctionName,
													  out ExternalFunctionEnum externalFunctionKey,
													  out int numberOfOperandsNeeded)
		{
			numberOfOperandsNeeded = -1;
			externalFunctionKey = ExternalFunctionEnum.FunctionNotRecognized;
			if (externalFunctionName == ExternalFunctionEnum.FooTwoValues.ToString().ToLower())
			{
				numberOfOperandsNeeded = 2;
				externalFunctionKey = ExternalFunctionEnum.FooTwoValues;
			}
			else if (externalFunctionName == ExternalFunctionEnum.BarThreeValues.ToString().ToLower())
			{
				numberOfOperandsNeeded = 3;
				externalFunctionKey = ExternalFunctionEnum.BarThreeValues;
			}
			else if (externalFunctionName == ExternalFunctionEnum.FooBarValues.ToString().ToLower())
			{
				numberOfOperandsNeeded = 0;
				externalFunctionKey = ExternalFunctionEnum.FooBarValues;
			}
			else
			{
				return false;
			}

			return true;
		}
	}

	public class MyExternalFunctionValueProvider : IExternalFunctionValueProvider<ExternalFunctionEnum>
	{

		public bool TryGetExternalFunctionValue(ExternalFunctionEnum externalFunctionKey, out Value value, params Value[] operands)
		{
			value = Value.Void;
			if (externalFunctionKey == ExternalFunctionEnum.FooTwoValues)
			{
				value = operands[0].Plus(operands[1]);
			}
			else if (externalFunctionKey == ExternalFunctionEnum.BarThreeValues)
			{
				value = operands[0].Time(operands[1]).Times(operands[2]);
			}
			else if (externalFunctionKey == ExternalFunctionEnum.FooBarValues)
			{
				value = new Variant((decimal)Math.E);
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}

```

```
#!C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SESL.NET.Compilation;
using SESL.NET.Function;

namespace SESL.NET.Tests
{
	public class ReallyImportantClass
	{
		public void DoStuff()
		{
			var functionText = "if (42, 1 + FooBarValues / (FooTwoValues(2,3) - BarThreeValues(4,5,6)), 'TurkeyBurgers') ";

			var function = new InfixNotation.InfixNotationCompiler().Compile<ExternalFunctionEnum>(new MyExternalFunctionKeyProvider(), functionText);

			var value = function.Evaluate(new MyExternalFunctionValueProvider());

			Console.WriteLine(value.ToString());
		}
	}
}

```

Have fun!
