﻿using System;

namespace SESL.NET.Function.Commands
{
    public class ArcTangent2Command<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return new Value(Math.Atan2(operands[0].ToDouble(), operands[1].ToDouble()));
        }
    }
}
