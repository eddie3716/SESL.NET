using System;
using NSubstitute;
using SESL.NET;
using SESL.NET.Exception;
using SESL.NET.InfixNotation;
using NUnit.Framework;

namespace SESL.NET.Tests;

[TestFixture]
public class FunctionEvaluationTests
{

	[Test]
	public void FunctionEvaluation_NumericTest()
	{
		string expression = "6";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(new Variant(6), result);
	}

	[Test]
	public void FunctionEvaluation_FunctionTest1()
	{
		string expression = "Bob";
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

		var myFunc = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

		var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();
		externalFunctionValueProvider.TryGetExternalFunctionValue(functionId, out Arg.Any<Variant>(), Array.Empty<Variant>())
			.Returns(
				x =>
				{
					x[1] = new Variant(2.0m);
					return true;
				}
			);

		var result = myFunc.Evaluate(externalFunctionValueProvider);

		Assert.AreEqual(result.DecimalValue, 2);
	}

	[Test]
	public void FunctionEvaluation_BadFunctionTest()
	{
		string expression = "Bob";
		string functionKey = "bob";
		int functionId = 1;

		var externalFunctionKeyProvider = MockHelper.GetExternalFunctionKeyProvider();
		externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName(functionKey, out Arg.Any<int>(), out Arg.Any<int>())
			.Returns(
				x =>
				{
					x[1] = functionId;
					x[2] = 0;
					return true;
				}
			);

		var myFunc = new InfixNotationCompiler().Compile(externalFunctionKeyProvider, expression);

		var externalFunctionValueProvider = MockHelper.GetExternalFunctionValueProvider();
		externalFunctionValueProvider.TryGetExternalFunctionValue(functionId, out Arg.Any<Variant>(), Array.Empty<Variant>())
			.Returns(
				x =>
				{
					x[1] = new Variant(0.0m);
					return false;
				}
			);

		Assert.Throws<ExternalFunctionValueNotFoundException>(() => myFunc.Evaluate(externalFunctionValueProvider));
	}

	#region Addition

	[Test]
	public void FunctionEvaluation_PlusTest()
	{
		string expression = "6+7";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(13));
	}

	[Test]
	public void FunctionEvaluation_PlusExceptionTest()
	{
		string expression = "6+";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Subtraction

	[Test]
	public void FunctionEvaluation_MinusTest()
	{
		string expression = "34-2";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(32));
	}

	[Test]
	public void FunctionEvaluation_MinusExceptionTest()
	{
		string expression = "6-";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Multiplication

	[Test]
	public void FunctionEvaluation_MultiplyTest()
	{
		string expression = "3*2";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(6));
	}

	[Test]
	public void FunctionEvaluation_MultiplyExceptionTest()
	{
		string expression = "6*";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Division

	[Test]
	public void FunctionEvaluation_DivideTest()
	{
		string expression = "6/2";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(3));
	}

	[Test]
	public void FunctionEvaluation_DivideExceptionTest()
	{
		string expression = "6/";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Exponent

	[Test]
	public void FunctionEvaluation_ExponentTest()
	{
		string expression = "2^3";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(new Variant(8), result);
	}

	[Test]
	public void FunctionEvaluation_ExponentTest2()
	{
		string expression = "2 ^ - 3";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result.DecimalValue, 1.0m / 8.0m);
	}

	[Test]
	public void FunctionEvaluation_ExponentExceptionTest()
	{
		string expression = "6^";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Unary Minus

	[Test]
	public void FunctionEvaluation_UnaryMinusTest()
	{
		string expression = "1~";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result.DecimalValue, -1);
	}

	public static void FunctionEvaluation_UnaryMinusTest2()
	{
		string expression = "-1";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, -1);
	}

	public static void FunctionEvaluation_UnaryMinusTest3()
	{
		string expression = "- 1";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, -1);
	}

	public static void FunctionEvaluation_UnaryMinusTest4()
	{
		string expression = "-1.0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, -1);
	}

	public static void FunctionEvaluation_UnaryMinusTest5()
	{
		string expression = "- 1.0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, -1);
	}

	public static void FunctionEvaluation_UnaryMinusTest6()
	{
		string expression = "1+ - 1.0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, 0);
	}

	public static void FunctionEvaluation_UnaryMinusTest7()
	{
		string expression = "1 +- 1.0 + 1";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, 1);
	}


	[Test]
	public void FunctionEvaluation_UnaryMinusExceptionTest()
	{
		string expression = "~";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Greater Than

	[Test]
	public void FunctionEvaluation_GreaterThanTest()
	{
		string expression = "7>6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_NotGreaterThanTest()
	{
		string expression = "5>6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_GreaterThanExceptionTest()
	{
		string expression = "6>";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Less Than

	[Test]
	public void FunctionEvaluation_NotLessThanTest()
	{
		string expression = "7<6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_LessThanTest()
	{
		string expression = "5<6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_LessThanExceptionTest()
	{
		string expression = "6<";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Greater Than Or Equal

	[Test]
	public void FunctionEvaluation_GreaterThanOrEqualTest()
	{
		string expression = "7>=6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_GreaterThanOrEqualEqualTest()
	{
		string expression = "7>=7";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_NotGreaterOrEqualThanTest()
	{
		string expression = "5>=6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_GreaterThanOrEqualExceptionTest()
	{
		string expression = "6>=";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Less Than Or Equal

	[Test]
	public void FunctionEvaluation_LessThanOrEqualTest()
	{
		string expression = "5<=6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_LessThanOrEqualEqualTest()
	{
		string expression = "7<=7";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_NotLessOrEqualThanTest()
	{
		string expression = "7<=6";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_LessThanOrEqualExceptionTest()
	{
		string expression = "6<=";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Not Equal

	[Test]
	public void FunctionEvaluation_NotEqualTest()
	{
		string expression = "5!=4";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_NotNotEqualTest()
	{
		string expression = "4!=4";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_NotEqualExceptionTest()
	{
		string expression = "4!=";

		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Equal

	[Test]
	public void FunctionEvaluation_EqualTest()
	{
		string expression = "4 = 4";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_InequalTest()
	{
		string expression = "5=4";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_EqualExceptionTest()
	{
		string expression = "5=";

		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region And

	[Test]
	public void FunctionEvaluation_AndTrueTest()
	{
		string expression = "5 and 4";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_AndRightZeroTest()
	{
		string expression = "-1 and 0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_AndLeftZeroTest()
	{
		string expression = " 0 and 2";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());
		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_AndExceptionTest()
	{
		string expression = "4 and";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Or

	[Test]
	public void FunctionEvaluation_OrLeftZeroTest()
	{
		string expression = "0 or 1";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_OrRightZeroTest()
	{
		string expression = "1 or 0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_OrBothZeroTest()
	{
		string expression = "0 or 0";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_OrExceptionTest()
	{
		string expression = "1 or";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Absolute Value

	[Test]
	public void FunctionEvaluation_AbsoluteValueTest()
	{
		string expression = "abs(-2)";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result.DecimalValue, 2);
	}

	[Test]
	public void FunctionEvaluation_AbsoluteValueExceptionTest()
	{
		string expression = "abs()";
		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Max

	[Test]
	public void FunctionEvaluation_MaxLeftTest()
	{
		string expression = "max(3, 2)";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(3));
	}

	[Test]
	public void FunctionEvaluation_MaxRightTest()
	{
		string expression = "max(2,3)";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(3));
	}

	[Test]
	public void FunctionEvaluation_MaxEqualsTest()
	{
		string expression = "max(3,3)";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(3));
	}

	[Test]
	public void FunctionEvaluation_MaxExceptionTest()
	{
		string expression = "max(3,)";

		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));

	}

	#endregion

	#region Min

	[Test]
	public void FunctionEvaluation_MinLeftTest()
	{
		string expression = "min(2,3)";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(2));
	}

	[Test]
	public void FunctionEvaluation_MinRightTest()
	{
		string expression = "min(3,2)";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(2));
	}

	[Test]
	public void FunctionEvaluation_MinEqualsTest()
	{
		string expression = "min(3,3)";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);

		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.AreEqual(result, new Variant(3));
	}

	[Test]
	public void FunctionEvaluation_MinExceptionTest()
	{
		string expression = "min(3,)";

		Assert.Throws<InsufficientOperandsException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression));
	}

	#endregion

	#region Case

	[Test]
	public void FunctionEvaluation_CaseTest1()
	{
		string expression = "case(1, return 'bob')";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.ToString().Equals("bob"));
	}

	[Test]
	public void FunctionEvaluation_CaseTest2()
	{
		string expression = "case(0, return 'bob')";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		Assert.Throws<InvalidFunctionResultException>(() => myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider()));
	}

	[Test]
	public void FunctionEvaluation_CaseTest3()
	{
		string expression = "case(0, return 'bob') case(1, return 'hank')";
		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.ToString().Equals("hank"));
	}

	#endregion

	#region Return

	[Test]
	public void FunctionEvaluation_ReturnTest()
	{
		string expression = "return 1<1";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_ReturnTest1()
	{
		string expression = "return 1<1 return 1>0";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void FunctionEvaluation_ReturnTest2()
	{
		string expression = "return IF(1>1, 123, 321) return 1>0";

		var myFunc = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), expression);
		var result = myFunc.Evaluate(MockHelper.GetExternalFunctionValueProvider());

		Assert.IsTrue(result.DecimalValue.Equals(321));
	}

	#endregion

	[Test]
	public void FunctionEvaluation_OrderMaintainedForOperatorsOfSameMathematicalPrecedence()
	{
		var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "5/10*2");

		// (5 / 10) * 2 = 1
		Assert.AreEqual(1, compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()).DecimalValue);
	}

	[Test]
	public void FunctionEvaluation_ParenthasesAlterOrderOfNonCommutativeOperators()
	{
		var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "5/(10*2)");

		// 5 / (10 * 2) = 0.25
		Assert.AreEqual(new Variant(0.25m), compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()));
	}

	[Test]
	public void FunctionEvaluation_ParenthasesDoNotAlterOrderOfCommutativeOperators()
	{
		var compiledFunction = new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "(5/10)*2");

		// (5 / 10) * 2 = 1
		Assert.AreEqual(1.0m, compiledFunction.Evaluate(MockHelper.GetExternalFunctionValueProvider()).DecimalValue);
	}
}