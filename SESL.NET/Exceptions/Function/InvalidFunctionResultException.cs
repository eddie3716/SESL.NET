﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Exception
{
	[global::System.Serializable]
	public class InvalidFunctionResultException : FunctionException
	{
		public int ResultCount { get; private set; }
	
		public InvalidFunctionResultException(int resultCount)
		{
			ResultCount = resultCount;
		}

		public InvalidFunctionResultException(int resultCount, string message)
			: base(message)
		{
			ResultCount = resultCount;
		}

		public InvalidFunctionResultException(int resultCount, string message, System.Exception inner)
			: base(message, inner)
		{
			ResultCount = resultCount;
		}

		protected InvalidFunctionResultException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
