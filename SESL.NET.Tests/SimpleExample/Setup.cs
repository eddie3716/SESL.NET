using System;
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

		public bool TryGetExternalFunctionValue(ExternalFunctionEnum externalFunctionKey, out Variant value, params Variant[] operands)
		{
			value = Variant.Void;
			if (externalFunctionKey == ExternalFunctionEnum.FooTwoValues)
			{
				value = operands[0] + operands[1];
			}
			else if (externalFunctionKey == ExternalFunctionEnum.BarThreeValues)
			{
				value = operands[0] * operands[1] * operands[2];
			}
			else if (externalFunctionKey == ExternalFunctionEnum.FooBarValues)
			{
				value = new Variant(DecimalMath.DecimalEx.E);
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}
