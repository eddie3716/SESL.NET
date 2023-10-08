using System;

namespace SESL.NET.Function.Commands
{
    public class EToThePowerCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Exp(ref operands[0]);            
        }
    }
}
