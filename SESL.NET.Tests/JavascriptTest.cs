using NUnit.Framework;
using NSubstitute;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests
{
    /// <summary>
    /// Summary description for JavascriptTest
    /// </summary>
    [TestFixture]
	public class JavascriptTest
	{
		public JavascriptTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[Test]
		public void TestMethod1()
		{
			string expression = "BOb > (5/10*2)";
			string variableKey = "bob";
			int functionId = 0;

			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(variableKey, out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = functionId;
						x[2] = 0;
						return true;
					}
				);

			var javascript = InfixNotationToJavaScript.Convert(externalFunctionKeyProvider, expression);

			Assert.IsTrue(javascript.Length > 0);
		}
	}
}
