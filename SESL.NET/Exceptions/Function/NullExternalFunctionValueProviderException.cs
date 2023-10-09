using System;

namespace SESL.NET.Exception;

[Serializable]
public class NullExternalFunctionValueProviderException : FunctionException
{
	public NullExternalFunctionValueProviderException()
	{
	}

	public NullExternalFunctionValueProviderException(string message) : base(message)
	{
	}

	public NullExternalFunctionValueProviderException(string message, System.Exception inner)
		: base(message, inner)
	{
	}

	protected NullExternalFunctionValueProviderException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context)
		: base(info, context) { }
}