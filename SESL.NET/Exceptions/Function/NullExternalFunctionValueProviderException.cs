using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
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
}
