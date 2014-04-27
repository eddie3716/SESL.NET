using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function;

namespace SESL.NET.Compilation
{
	public interface IExternalFunctionKeyProvider<TExternalFunctionKey> 
	{
		bool TryGetExternalFunctionKeyFromName(string externalFunctionName, out TExternalFunctionKey externalFunctionKey, out int numberOfOperandsNeeded);
	}
}
