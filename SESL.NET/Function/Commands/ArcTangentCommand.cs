using System;

namespace SESL.NET.Function.Commands
{
    public class ArcTangentCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Atan(operands[0].ToDouble()));
        }
    }
}
