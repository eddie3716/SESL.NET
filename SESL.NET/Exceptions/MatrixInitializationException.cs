﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class MatrixInitializationException : MathException
	{
		public MatrixInitializationException(): base()
		{
		}

		public MatrixInitializationException(string message): base(message)
		{
		}

		public MatrixInitializationException(string message, System.Exception inner): base(message, inner)
		{
		}

		protected MatrixInitializationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
