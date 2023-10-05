using System;

namespace SESL.NET.Function.Commands
{
    public class ArcSineCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Asin(operands[0].ToDouble()));
        }
    }
}
