﻿using System;

namespace SESL.NET.Function.Commands
{
    public class NaturalLogarithmCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return operands[0].NaturalLog();
        }
    }
}
