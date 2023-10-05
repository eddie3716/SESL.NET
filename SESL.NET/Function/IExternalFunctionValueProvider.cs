namespace SESL.NET.Function
{
    public interface IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Value value, params Value[] operands);
	}
}
