using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class FunctionException : MathException
	{
		public FunctionException(): base()
		{
		}

		public FunctionException(string message): base(message)
		{
		}

		public FunctionException(string message, System.Exception inner): base(message, inner)
		{
		}

		protected FunctionException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
