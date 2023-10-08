using System;
using System.Globalization;

namespace SESL.NET.Function.Commands
{
    class HyperbolicTangentCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Tanh(ref operands[0]);
        }
    }
}
