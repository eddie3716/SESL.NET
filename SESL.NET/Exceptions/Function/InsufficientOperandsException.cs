using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class InsufficientOperandsException : FunctionException
	{
		public int OperandsNeeded { get; private set; }

		public int OperandsAvailable { get; private set; }

		public string Operation { get; private set; }

		public InsufficientOperandsException(int operandsNeeded, int operandsAvailable, string operation)
		{
			OperandsNeeded = operandsNeeded;
			OperandsAvailable = operandsAvailable;
			Operation = operation;
		}

		public InsufficientOperandsException(int operandsNeeded, int operandsAvailable, string operation, string message)
			: base(message)
		{
			OperandsNeeded = operandsNeeded;
			OperandsAvailable = operandsAvailable;
			Operation = operation;
		}

		public InsufficientOperandsException(int operandsNeeded, int operandsAvailable, string operation, string message, System.Exception inner)
			: base(message, inner)
		{
			OperandsNeeded = operandsNeeded;
			OperandsAvailable = operandsAvailable;
			Operation = operation;
		}

		protected InsufficientOperandsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
