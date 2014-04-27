using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function.Commands
{
    public interface IFunctionCommand<TExternalFunctionKey>
    {
        Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands);
    }
}
