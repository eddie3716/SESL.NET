using System;

namespace SESL.NET.Function.Commands
{
    public class HyperbolicSineCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Sinh(ref operands[0]);
        }
    }
}
