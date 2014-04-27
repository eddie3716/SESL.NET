using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
    public class HyperbolicCosineCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Cosh(operands[0].ToDouble()));
        }
    }
}
