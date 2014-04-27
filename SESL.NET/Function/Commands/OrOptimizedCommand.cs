using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
	public class OrOptimizedCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
		{
			return new Value(functionNode.Functions[0].Evaluate(externalFunctionValueProvider).ToBoolean() || functionNode.Functions[1].Evaluate(externalFunctionValueProvider).ToBoolean());
		}
	}
}
