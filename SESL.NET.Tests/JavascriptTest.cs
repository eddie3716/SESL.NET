using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using SESL.NET;
using SESL.NET.Exception;
using SESL.NET.Function;
using SESL.NET.Compilation;
using SESL.NET.InfixNotation;

namespace SESL.NET.Test
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

			int temp1 = 0;
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName(variableKey, out temp1, out temp1))
				.OutRef(functionId)
				.Return(true);

			var javascript = new InfixNotationToJavaScript().Convert(externalFunctionKeyProvider, expression);

			Assert.IsTrue(javascript.Length > 0);
		}
	}
}
