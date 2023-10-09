namespace SESL.NET.Exception;

public class ExternalFunctionKeyNotFoundException : CompilerException
{
	public string ExternalFunctionName { get; private set; }

	public ExternalFunctionKeyNotFoundException(string externalFunctionName)
	{
		ExternalFunctionName = externalFunctionName;
	}

	public ExternalFunctionKeyNotFoundException(string externalFunctionName, string message)
		: base(message)
	{
		ExternalFunctionName = externalFunctionName;
	}

	public ExternalFunctionKeyNotFoundException(string externalFunctionName, string message, System.Exception inner)
		: base(message, inner)
	{
		ExternalFunctionName = externalFunctionName;
	}

	protected ExternalFunctionKeyNotFoundException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context)
		: base(info, context) { }
}