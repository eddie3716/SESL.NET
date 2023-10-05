using System;

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
