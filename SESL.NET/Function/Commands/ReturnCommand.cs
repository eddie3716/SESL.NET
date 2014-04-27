using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
    public class ReturnCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            throw new FunctionReturnException(operands[0]);
        }
    }
}
