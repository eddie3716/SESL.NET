using NSubstitute;
using SESL.NET.Compilation;
using SESL.NET.Function;

namespace SESL.NET.Tests
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
