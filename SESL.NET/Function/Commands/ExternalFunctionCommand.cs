using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
	public class ExternalFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
		{
			Value value = Value.Void;

			if (!externalFunctionValueProvider.TryGetExternalFunctionValue(functionNode.ExternalFunctionKey, out value, operands))
			{
				throw new ExternalFunctionValueNotFoundException(functionNode.ToString());
			}

			return value;
		}
	}
}
