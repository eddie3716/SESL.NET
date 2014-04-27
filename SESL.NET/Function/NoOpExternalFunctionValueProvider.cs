using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function
{
	internal class NoOpExternalFunctionValueProvider<TExternalFunctionKey> : IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		public bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Value value, params Value[] operands)
		{
			value = new Value(String.Empty);
			return false;
		}
	}
}
