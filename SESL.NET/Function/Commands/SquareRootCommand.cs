﻿using System;

namespace SESL.NET.Function.Commands
{
    public class SquareRootCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Sqrt(operands[0].ToDouble()));
        }
    }
}
