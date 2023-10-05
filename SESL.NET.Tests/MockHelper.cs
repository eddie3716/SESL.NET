using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using SESL.NET.Compilation;
using SESL.NET.Function;

namespace SESL.NET.Test
{
	public static class MockHelper
	{
		public static IExternalFunctionKeyProvider<int> GetExternalFunctionKeyProvider()
		{
			return Substitute.For<IExternalFunctionKeyProvider<int>>();
		}

		public static IExternalFunctionValueProvider<int> GetExternalFunctionValueProvider()
		{
			return Substitute.For<IExternalFunctionValueProvider<int>>();
		}
	}
}
