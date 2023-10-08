﻿using System;

namespace SESL.NET.Function.Commands
{
    public class ArcSineCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.ASin(ref operands[0]);
        }
    }
}
