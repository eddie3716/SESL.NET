﻿namespace SESL.NET.Function.Commands
{
    public class GreaterThanOrEqualCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return operands[0] >= operands[1];
        }
    }
}
