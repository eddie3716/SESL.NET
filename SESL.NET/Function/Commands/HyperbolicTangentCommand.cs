using System;

namespace SESL.NET.Function.Commands
{
    class HyperbolicTangentCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Tanh(operands[0].ToDouble()));
        }
    }
}
