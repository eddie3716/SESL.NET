using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class FunctionReturnException : FunctionException
	{
		public Value Result { get; private set; }

		public FunctionReturnException(Value result)
		{
			Result = result;
		}

		public FunctionReturnException(Value result, string message)
			: base(message)
		{
			Result = result;
		}

		public FunctionReturnException(Value result, string message, System.Exception inner)
			: base(message, inner)
		{
			Result = result;
		}

		protected FunctionReturnException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
