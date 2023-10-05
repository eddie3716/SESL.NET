using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using SESL.NET;
using SESL.NET.Exception;
using SESL.NET.Function;
using SESL.NET.Compilation;
using SESL.NET.InfixNotation;

namespace SESL.NET.Test
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestFixture]
	public class InfixNotationCompilerTests
	{

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
		// public void InfixNotationCompiler_MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void InfixNotationCompiler_MyTestCleanup() { }
		//
		#endregion

		#region IF

		[Test]
		public void InfixNotationCompiler_ThrowInvalidTernaryExpression1()
		{
			Assert.Throws<SESL.NET.Exception.CompilerException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0)"));
		}


		[Test]
		public void InfixNotationCompiler_ThrowInvalidTernaryExpression2()
		{
			Assert.Throws<SESL.NET.Exception.CompilerException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0,0)"));
		}

		[Test]
		public void InfixNotationCompiler_TernaryExpressionSyntaxValiddouble()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(1,1,0)");

			Assert.IsNotNull(compiledFunction);
		}


		[Test]
		public void InfixNotationCompiler_TernaryExpressionValiddouble()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(1,1,0)");

			Assert.IsNotNull(compiledFunction);
		}

		[Test]
		public void InfixNotationCompiler_TernaryExpressionEvaluateFalseValiddouble()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0,1,0)");

			Assert.IsNotNull(compiledFunction);
		}

		[Test]
		public void InfixNotationCompiler_TernaryExpressionValidComplexdouble2()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(1,MAX(5,10),0)");

			Assert.IsNotNull(compiledFunction);
		}

		[Test]
		public void InfixNotationCompiler_TernaryExpressionValidComplexdouble3()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0,MAX(1,0),MIN(1,0))");

			Assert.IsNotNull(compiledFunction);
		}

		[Test]
		public void InfixNotationCompiler_CaseExpressionValidComplex()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "CASE(IF(0,MAX(1,0),MIN(1,0)), return 0)");

			Assert.IsNotNull(compiledFunction);
		}

		#endregion
	}
}
