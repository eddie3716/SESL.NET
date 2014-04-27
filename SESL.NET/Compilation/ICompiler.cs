using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function;

namespace SESL.NET.Compilation
{
    public interface ICompiler
    {
        Function<TExternalFunctionKey> Compile<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider, string expression);
    }
}
