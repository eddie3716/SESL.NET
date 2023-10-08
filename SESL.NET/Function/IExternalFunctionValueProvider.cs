namespace SESL.NET.Function
{
    public interface IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Variant value, params Variant[] operands);
	}
}
