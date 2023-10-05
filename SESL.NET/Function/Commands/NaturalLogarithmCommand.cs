using System;

namespace SESL.NET.Function.Commands
{
    public class NaturalLogarithmCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Log(operands[0].ToDouble()));
        }
    }
}
