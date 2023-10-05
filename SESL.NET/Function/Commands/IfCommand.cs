﻿namespace SESL.NET.Function.Commands
{
    public class IfCommand<TExternalFunctionKey>: IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return functionNode.Functions[0].Evaluate(externalFunctionValueProvider).ToBoolean() ?
                functionNode.Functions[1].Evaluate(externalFunctionValueProvider) :
                functionNode.Functions[2].Evaluate(externalFunctionValueProvider);
        }
    }
}
