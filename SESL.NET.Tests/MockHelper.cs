using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using SESL.NET.Compilation;
using SESL.NET.Function;

namespace SESL.NET.Test
{
	public static class MockHelper
	{
		public static IExternalFunctionKeyProvider<int> GetExternalFunctionKeyProvider()
		{
			return MockRepository.GenerateMock<IExternalFunctionKeyProvider<int>>();
		}

		public static IExternalFunctionValueProvider<int> GetExternalFunctionValueProvider()
		{
			return MockRepository.GenerateMock<IExternalFunctionValueProvider<int>>();
		}
	}
}
