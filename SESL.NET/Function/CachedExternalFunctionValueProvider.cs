using System.Collections.Generic;

namespace SESL.NET.Function
{
    public class CachedExternalFunctionValueProvider<TExternalFunctionKey>: Dictionary<TExternalFunctionKey, Variant>, IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		private readonly IExternalFunctionValueProvider<TExternalFunctionKey> _innerExternalFunctionKeyProvider;

		public CachedExternalFunctionValueProvider(IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionKeyProvider)
		{
			_innerExternalFunctionKeyProvider = externalFunctionKeyProvider;
		}

		public bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Variant value, params Variant[] operands)
		{
			if (ContainsKey(externalFunctionKey))
			{
				value = this[externalFunctionKey];

				return true;
			}

			return _innerExternalFunctionKeyProvider.TryGetExternalFunctionValue(externalFunctionKey, out value, operands);
		}
	}
}
