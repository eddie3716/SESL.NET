using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function;

namespace SESL.NET.Compilation
{
	public interface IOptimizer
	{
		IList<FunctionNode<TExternalFunctionKey>> Optimize<TExternalFunctionKey>(IList<FunctionNode<TExternalFunctionKey>> functionNodes);
	}
}
