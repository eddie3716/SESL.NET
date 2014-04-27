using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function;

namespace SESL.NET.Compilation
{
    public interface IParser
    {
        IList<FunctionNode<TExternalFunctionKey>> GetFunctionNodes<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider);
    }
}
