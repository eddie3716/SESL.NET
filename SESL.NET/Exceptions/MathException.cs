using System;

namespace SESL.NET.Exception;

[Serializable]
public abstract class MathException : System.Exception
{
	public MathException() : base()
	{
	}

	public MathException(string message) : base(message)
	{
	}

	public MathException(string message, System.Exception inner)
		: base(message, inner)
	{
	}

	protected MathException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context)
		: base(info, context) { }
}