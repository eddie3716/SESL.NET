using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class ExternalFunctionValueNotFoundException : FunctionException
	{
		public string ExternalFunctionName { get; private set; }

		public ExternalFunctionValueNotFoundException(string externalFunctionName)
		{
			ExternalFunctionName = externalFunctionName;
		}

		public ExternalFunctionValueNotFoundException(string externalFunctionName, string message)
			: base(message)
		{
			ExternalFunctionName = externalFunctionName;
		}

		public ExternalFunctionValueNotFoundException(string externalFunctionName, string message, System.Exception inner)
			: base(message, inner)
		{
			ExternalFunctionName = externalFunctionName;
		}

		protected ExternalFunctionValueNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
