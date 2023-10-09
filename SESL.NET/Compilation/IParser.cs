using System.Collections.Generic;
using SESL.NET.Function;

namespace SESL.NET.Compilation;

public interface IParser
{
    IList<FunctionNode<TExternalFunctionKey>> GetFunctionNodes<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider);
}