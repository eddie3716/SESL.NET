namespace SESL.NET.Function;

internal class NoOpExternalFunctionValueProvider<TExternalFunctionKey> : IExternalFunctionValueProvider<TExternalFunctionKey>
{
	public bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Variant value, params Variant[] operands)
	{
		value = new Variant();
		return false;
	}
}