using System.Collections.Generic;
using System.Linq;
using SESL.NET.Syntax;

namespace SESL.NET.Function;

public static class FunctionNodeHelper
{
	public static bool IsEqual<TExternalFunctionKey>(this IList<FunctionNode<TExternalFunctionKey>> first, IList<FunctionNode<TExternalFunctionKey>> second)
	{
		var zippedList = first.Zip(second, (x, y) => new { x, y });
		if (zippedList.Count() != first.Count && first.Count != second.Count)
		{
			return false;
		}
		foreach (var item in zippedList)
		{
			if (item.x.Equals(null) || item.y.Equals(null) || !item.x.Equals(item.y))
			{
				return false;
			}
		}

		return true;
	}

	public static Function<TExternalFunctionKey> ToFunction<TExternalFunctionKey>(this IList<FunctionNode<TExternalFunctionKey>> nodes)
	{
		return new Function<TExternalFunctionKey>(nodes);
	}

	public static IEnumerable<TExternalFunctionKey> GetParametersKeys<TExternalFunctionKey>(this IList<FunctionNode<TExternalFunctionKey>> nodes)
	{
		return nodes.Where(n => n.Semantics.Type == TokenType.ExternalFunction).Select(n => n.ExternalFunctionKey).Distinct();
	}

	public static IEnumerable<FunctionNode<TExternalFunctionKey>> GetFunctionNodes<TExternalFunctionKey>(this Function<TExternalFunctionKey> function)
	{
		return function.FunctionNodes;
	}
}