﻿using System.Collections.Generic;
using System.Linq;

namespace SESL.NET.Function;

public static class FunctionExtensions
{
	public static IEnumerable<string> GetAllParametersNames<TExternalFunctionKey>(this Function<TExternalFunctionKey> function)
	{
		foreach (var functionNode in function.FunctionNodes)
		{
			if (functionNode.Semantics.Type == Syntax.TokenType.ExternalFunction)
			{
				yield return functionNode.Variant.ToString();
			}

			foreach (var parameterName in functionNode.Functions.SelectMany(f => f.GetAllParametersNames()))
			{
				yield return parameterName;
			}

		}
	}

	public static IEnumerable<string> GetAllDistinctParametersNames<TExternalFunctionKey>(this Function<TExternalFunctionKey> function)
	{
		return function.GetAllParametersNames().Distinct();
	}
}