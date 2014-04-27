using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function.Commands
{
	public class CaseCommand<TExternalFunctionKey>: IFunctionCommand<TExternalFunctionKey>
	{
		public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
		{
			return functionNode.Functions[0].Evaluate(externalFunctionValueProvider).ToBoolean() ?
				functionNode.Functions[1].Evaluate(externalFunctionValueProvider) :
				Value.Void;
		}
	}
}
