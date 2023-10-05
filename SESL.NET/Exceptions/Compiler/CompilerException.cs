using System;

namespace SESL.NET.Exception
{
    [Serializable]
	public class CompilerException : MathException
	{
		public CompilerException(): base()
		{
		}

		public CompilerException(string message) : base(message)
		{
		}

		public CompilerException(string message, System.Exception inner)
			: base(message, inner)
		{
		}

		protected CompilerException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
