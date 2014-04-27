﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
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
	[TestClass]
	public class FunctionEvaluationTests
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
		// public void FunctionEvaluation_MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void FunctionEvaluation_MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void FunctionEvaluation_NumericTest()
		{
			string expression = "6";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(6));
		}

		[TestMethod]
		public void FunctionEvaluation_FunctionTest1()
		{
			string expression = "Bob";
			string variableKey = "bob";
			int functionId = 0;

			int temp1 = 0;
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName(variableKey, out temp1, out temp1))
				.OutRef(functionId)
				.Return(true);

			var myFunc = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var temp = new Value(0.0);
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();
			externalFunctionValueProvider.Stub(x => x.TryGetExternalFunctionValue(functionId, out temp))
				.IgnoreArguments()
				.OutRef(new Value(2.0))
				.Return(true);

			var result = myFunc.Evaluate(externalFunctionValueProvider);

			Assert.AreEqual(result.ToInt32(), (2));
		}

		[TestMethod]
		[ExpectedException(typeof(ExternalFunctionValueNotFoundException))]
		public void FunctionEvaluation_BadFunctionTest()
		{
			string expression = "Bob";
			string functionKey = "bob";
			int functionId = 1;

			int temp1 = 0;
			var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
			externalFunctionKeyProvider.Expect(x => x.TryGetExternalFunctionKeyFromName(functionKey, out temp1, out temp1))
				.OutRef(functionId)
				.Return(true);

			var myFunc = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

			var temp = new Value(0.0);
			var tempOperands = new Value[0];
			var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();
			externalFunctionValueProvider.Expect(x => x.TryGetExternalFunctionValue(functionId, out temp, tempOperands))
				.OutRef(new Value(0.0))
				.Return(false);

			myFunc.Evaluate(externalFunctionValueProvider);
		}

		#region Addition

		[TestMethod]
		public void FunctionEvaluation_PlusTest()
		{
			string expression = "6+7";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(13));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_PlusExceptionTest()
		{
			string expression = "6+";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Subtraction

		[TestMethod]
		public void FunctionEvaluation_MinusTest()
		{
			string expression = "34-2";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(32));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_MinusExceptionTest()
		{
			string expression = "6-";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Multiplication

		[TestMethod]
		public void FunctionEvaluation_MultiplyTest()
		{
			string expression = "3*2";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(6));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_MultiplyExceptionTest()
		{
			string expression = "6*";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Division

		[TestMethod]
		public void FunctionEvaluation_DivideTest()
		{
			string expression = "6/2";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(3));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_DivideExceptionTest()
		{
			string expression = "6/";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Exponent

		[TestMethod]
		public void FunctionEvaluation_ExponentTest()
		{
			string expression = "2^3";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(8.0));
		}

		[TestMethod]
		public void FunctionEvaluation_ExponentTest2()
		{
			string expression = "2 ^ - 3";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result.ToDouble(), 1.0 / 8.0);
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_ExponentExceptionTest()
		{
			string expression = "6^";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Unary Minus

		[TestMethod]
		public void FunctionEvaluation_UnaryMinusTest()
		{
			string expression = "1~";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result.ToInt32(), (-1));
		}

		public void FunctionEvaluation_UnaryMinusTest2()
		{
			string expression = "-1";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (-1));
		}

		public void FunctionEvaluation_UnaryMinusTest3()
		{
			string expression = "- 1";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (-1));
		}

		public void FunctionEvaluation_UnaryMinusTest4()
		{
			string expression = "-1.0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (-1));
		}

		public void FunctionEvaluation_UnaryMinusTest5()
		{
			string expression = "- 1.0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (-1));
		}

		public void FunctionEvaluation_UnaryMinusTest6()
		{
			string expression = "1+ - 1.0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (0));
		}

		public void FunctionEvaluation_UnaryMinusTest7()
		{
			string expression = "1 +- 1.0 + 1";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, (1));
		}


		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_UnaryMinusExceptionTest()
		{
			string expression = "~";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Greater Than

		[TestMethod]
		public void FunctionEvaluation_GreaterThanTest()
		{
			string expression = "7>6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_NotGreaterThanTest()
		{
			string expression = "5>6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_GreaterThanExceptionTest()
		{
			string expression = "6>";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Less Than

		[TestMethod]
		public void FunctionEvaluation_NotLessThanTest()
		{
			string expression = "7<6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_LessThanTest()
		{
			string expression = "5<6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_LessThanExceptionTest()
		{
			string expression = "6<";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Greater Than Or Equal

		[TestMethod]
		public void FunctionEvaluation_GreaterThanOrEqualTest()
		{
			string expression = "7>=6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_GreaterThanOrEqualEqualTest()
		{
			string expression = "7>=7";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_NotGreaterOrEqualThanTest()
		{
			string expression = "5>=6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_GreaterThanOrEqualExceptionTest()
		{
			string expression = "6>=";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Less Than Or Equal

		[TestMethod]
		public void FunctionEvaluation_LessThanOrEqualTest()
		{
			string expression = "5<=6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_LessThanOrEqualEqualTest()
		{
			string expression = "7<=7";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_NotLessOrEqualThanTest()
		{
			string expression = "7<=6";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_LessThanOrEqualExceptionTest()
		{
			string expression = "6<=";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Not Equal

		[TestMethod]
		public void FunctionEvaluation_NotEqualTest()
		{
			string expression = "5!=4";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_NotNotEqualTest()
		{
			string expression = "4!=4";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_NotEqualExceptionTest()
		{
			string expression = "4!=";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Equal

		[TestMethod]
		public void FunctionEvaluation_EqualTest()
		{
			string expression = "4 = 4";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_InequalTest()
		{
			string expression = "5=4";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_EqualExceptionTest()
		{
			string expression = "5=";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region And

		[TestMethod]
		public void FunctionEvaluation_AndTrueTest()
		{
			string expression = "5 and 4";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_AndRightZeroTest()
		{
			string expression = "-1 and 0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_AndLeftZeroTest()
		{
			string expression = " 0 and 2";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_AndExceptionTest()
		{
			string expression = "4 and";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Or

		[TestMethod]
		public void FunctionEvaluation_OrLeftZeroTest()
		{
			string expression = "0 or 1";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_OrRightZeroTest()
		{
			string expression = "1 or 0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_OrBothZeroTest()
		{
			string expression = "0 or 0";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_OrExceptionTest()
		{
			string expression = "1 or";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Absolute Value

		[TestMethod]
		public void FunctionEvaluation_AbsoluteValueTest()
		{
			string expression = "abs(-2)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result.ToInt32(), (2));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_AbsoluteValueExceptionTest()
		{
			string expression = "abs()";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Max

		[TestMethod]
		public void FunctionEvaluation_MaxLeftTest()
		{
			string expression = "max(3, 2)";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(3));
		}

		[TestMethod]
		public void FunctionEvaluation_MaxRightTest()
		{
			string expression = "max(2,3)";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(3));
		}

		[TestMethod]
		public void FunctionEvaluation_MaxEqualsTest()
		{
			string expression = "max(3,3)";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(3));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_MaxExceptionTest()
		{
			string expression = "max(3,)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		}

		#endregion

		#region Min

		[TestMethod]
		public void FunctionEvaluation_MinLeftTest()
		{
			string expression = "min(2,3)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(2));
		}

		[TestMethod]
		public void FunctionEvaluation_MinRightTest()
		{
			string expression = "min(3,2)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(2));
		}

		[TestMethod]
		public void FunctionEvaluation_MinEqualsTest()
		{
			string expression = "min(3,3)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.AreEqual(result, new Value(3));
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientOperandsException))]
		public void FunctionEvaluation_MinExceptionTest()
		{
			string expression = "min(3,)";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

			myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		#endregion

		#region Case

		[TestMethod]
		public void FunctionEvaluation_CaseTest1()
		{
			string expression = "case(1, return 'bob')";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToString().Equals("bob"));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidFunctionResultException))]
		public void FunctionEvaluation_CaseTest2()
		{
			string expression = "case(0, return 'bob')";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		}

		[TestMethod]
		public void FunctionEvaluation_CaseTest3()
		{
			string expression = "case(0, return 'bob') case(1, return 'hank')";
			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToString().Equals("hank"));
		}

		#endregion

		#region Return

		[TestMethod]
		public void FunctionEvaluation_ReturnTest()
		{
			string expression = "return 1<1";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_ReturnTest1()
		{
			string expression = "return 1<1 return 1>0";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void FunctionEvaluation_ReturnTest2()
		{
			string expression = "return IF(1>1, 123, 321) return 1>0";

			var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
			var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

			Assert.IsTrue(result.ToInt32().Equals(321));
		}

		#endregion

		[TestMethod]
		public void FunctionEvaluation_OrderMaintainedForOperatorsOfSameMathematicalPrecedence()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "5/10*2");

			// (5 / 10) * 2 = 1
			Assert.AreEqual(1, compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()).ToInt32());
		}

		[TestMethod]
		public void FunctionEvaluation_ParenthasesAlterOrderOfNonCommutativeOperators()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "5/(10*2)");

			// 5 / (10 * 2) = 0.25
			Assert.AreEqual(new Value(0.25), compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()));
		}

		[TestMethod]
		public void FunctionEvaluation_ParenthasesDoNotAlterOrderOfCommutativeOperators()
		{
			var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "(5/10)*2");

			// (5 / 10) * 2 = 1
			Assert.AreEqual(1, compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()).ToInt32());
		}
	}
}
