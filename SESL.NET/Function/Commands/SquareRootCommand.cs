﻿using System;

namespace SESL.NET.Function.Commands
{
    public class SquareRootCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Sqrt(ref operands[0]);
        }
    }
}
