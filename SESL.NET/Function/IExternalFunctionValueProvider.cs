using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function
{
	public interface IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Value value, params Value[] operands);
	}
}
