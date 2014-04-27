using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
    class ArcCosineCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Acos(operands[0].ToDouble()));
        }
    }
}
