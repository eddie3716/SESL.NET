﻿namespace SESL.NET.Function.Commands
{
    public class OrCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return Value.Or(ref operands[0], ref operands[1]);
        }
    }
}
