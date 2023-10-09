using System.Collections.Generic;
using SESL.NET.Function;

namespace SESL.NET.Compilation;

public interface IOptimizer
{
	IList<FunctionNode<TExternalFunctionKey>> Optimize<TExternalFunctionKey>(IList<FunctionNode<TExternalFunctionKey>> functionNodes);
}