namespace SESL.NET.Compilation;

public interface IExternalFunctionKeyProvider<TExternalFunctionKey>
{
	bool TryGetExternalFunctionKeyFromName(string externalFunctionName, out TExternalFunctionKey externalFunctionKey, out int numberOfOperandsNeeded);
}