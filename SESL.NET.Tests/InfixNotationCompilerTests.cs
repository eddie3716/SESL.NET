using NUnit.Framework;
using SESL.NET.Exception;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests;

[TestFixture]
public class InfixNotationCompilerTests
{
	#region IF

	[Test]
	public void InfixNotationCompiler_ThrowInvalidTernaryExpression1()
	{
		Assert.Throws<CompilerException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0)"));
	}


	[Test]
	public void InfixNotationCompiler_ThrowInvalidTernaryExpression2()
	{
		Assert.Throws<CompilerException>(() => new InfixNotationCompiler().Compile(MockHelper.GetExternalFunctionKeyProvider(), "IF(0,0)"));
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