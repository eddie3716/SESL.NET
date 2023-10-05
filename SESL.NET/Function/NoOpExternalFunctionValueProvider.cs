namespace SESL.NET.Function
{
    internal class NoOpExternalFunctionValueProvider<TExternalFunctionKey> : IExternalFunctionValueProvider<TExternalFunctionKey>
	{
		public bool TryGetExternalFunctionValue(TExternalFunctionKey externalFunctionKey, out Value value, params Value[] operands)
		{
			value = new Value(string.Empty);
			return false;
		}
	}
}
