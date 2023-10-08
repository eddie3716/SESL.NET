using System;

namespace SESL.NET.Function.Commands
{
    public class LogarithmBase10Command<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Log10(ref operands[0]);
        }
    }
}
