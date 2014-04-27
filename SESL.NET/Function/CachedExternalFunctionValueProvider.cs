using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Function
{
	public class CachedExternalFunctionValueProvider<TExternalFunctionKey>: Dictionary<TExternalFunctionKey, Value>, IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		private IExternalFunctionValueProvider<TExternalFunctionKey> _innerExternalFunctionKeyProvider;

		public CachedExternalFunctionValueProvider(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionKeyProvider)
		{
			_innerExternalFunctionKeyProvider = externalFunctionKeyProvider;
		}

		public bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Value value, params Value[] operands)
		{
			if (this.ContainsKey(externalFunctionKey))
			{
				value = this[externalFunctionKey];

				return true;
			}

			return _innerExternalFunctionKeyProvider.TryGetExternalFunctionValue(externalFunctionKey, out value, operands);
		}
	}
}
