using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function.Commands
{
	internal class AutomaticFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
		{
			return functionNode.FunctionCommand(functionNode, externalFunctionValueProvider, operands);
		}
	}
}
