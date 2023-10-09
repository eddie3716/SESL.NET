using NUnit.Framework;

namespace SESL.NET.Tests;

[TestFixture]
public class ValueOperationTests
{
	[Test]
	public void ValueOperation_Negate()
	{
		var value = new Variant(3.0m);
		var negatedInt = value.Negate().DecimalValue;
		Assert.AreEqual(negatedInt, -3m);
	}

	[Test]
	public void ValueOperation_Addition()
	{
		var value1 = new Variant(3.0m);
		var value2 = new Variant(5m);
		var result = value1.Plus(value2);
		Assert.AreEqual(result.DecimalValue, 8m);
	}

	[Test]
	public void ValueOperation_Addition_Ones()
	{
		var value1 = new Variant(1);
		var value2 = new Variant(1);
		var result = value1.Plus(value2);
		Assert.AreEqual(result.DecimalValue, 2m);
	}

	[Test]
	public void ValueOperation_Subtraction()
	{
		var value1 = new Variant(3.0m);
		var value2 = new Variant(5);
		var result = value1.Minus(value2);
		Assert.AreEqual(result.DecimalValue, -2m);
	}

	[Test]
	public void ValueOperation_Modulo()
	{
		var value1 = new Variant(8.0m);
		var value2 = new Variant(2);
		var result = value1.Mod(value2);
		Assert.AreEqual(result.DecimalValue, new Variant(0).DecimalValue);
	}

	[Test]
	public void ValueOperation_Multiply()
	{
		var value1 = new Variant(8.0m);
		var value2 = new Variant(2);
		var result = value1.Times(value2);
		Assert.AreEqual(result.DecimalValue, 16);
	}

	[Test]
	public void ValueOperation_Divide()
	{
		var value1 = new Variant(8.0m);
		var value2 = new Variant(2);
		var result = value1.DividedBy(value2);
		Assert.AreEqual(result.DecimalValue, 4);
	}

	[Test]
	public void ValueOperation_Pow()
	{
		var baseValue = new Variant(13M);
		var exponent = new Variant(5L);

		var result = baseValue.ToPowerOf(exponent);

		Assert.AreEqual(result.DecimalValue, 371293M);
	}

	[Test]
	public void ValueOperation_Abs()
	{
		var value = new Variant(-13M);

		var result = value.Abs();

		Assert.AreEqual(result.DecimalValue, 13M);
	}

	[Test]
	public void ValueOperation_Min()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.Min(value2);

		Assert.AreEqual(result.DecimalValue, -13M);
	}

	[Test]
	public void ValueOperation_Max()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.Max(value2);

		Assert.AreEqual(result.DecimalValue, 13M);
	}

	[Test]
	public void ValueOperation_GreaterThan1()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.IsGreaterThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan2()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(-13M);

		var result = value1.IsGreaterThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan3()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan4()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(1);

		var result = value1.IsGreaterThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan5()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-1);

		var result = value1.IsGreaterThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan6_1()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan6_2()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan7_1()
	{
		var value1 = new Variant(0);

		var value2 = new Variant(1);

		var result = value1.IsGreaterThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThan7_2()
	{
		var value1 = new Variant(0);

		var value2 = new Variant(1);

		var result = value1.IsGreaterThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual1()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual2()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(-13M);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual3()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual4()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(1);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual5()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-1);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual6()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-13L);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual7_1()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_GreaterThanOrEqual7_2()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsGreaterThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan1()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.IsLessThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan2()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(-13M);

		var result = value1.IsLessThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan3()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(0);

		var result = value1.IsLessThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan4()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(1);

		var result = value1.IsLessThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan5()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-1);

		var result = value1.IsLessThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan6_1()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsLessThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan6_2()
	{
		var value1 = new Variant(1);

		var value2 = new Variant(0);

		var result = value1.IsLessThan(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan7_1()
	{
		var value1 = new Variant(0);

		var value2 = new Variant(1);

		var result = value1.IsLessThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThan7_2()
	{
		var value1 = new Variant(0);

		var value2 = new Variant(1);

		var result = value1.IsLessThan(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual1()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13M);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual2()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(-13M);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual3()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(0);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual4()
	{
		var value1 = new Variant(13M);

		var value2 = new Variant(1);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual5()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-1);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual6()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-13L);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual7_1()
	{
		var value1 = new Variant(0);

		var value2 = new Variant(1);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_LessThanOrEqual7_2()
	{
		var value1 = new Variant(0); ;

		var value2 = new Variant(1);

		var result = value1.IsLessThanOrEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_NotEquals()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(13L);

		var result = value1.DoesNotEqual(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_Equals()
	{
		var value1 = new Variant(-13M);

		var value2 = new Variant(-13L);

		var result = value1.IsEqualTo(value2);

		Assert.IsTrue(result.BoolValue);
	}

	[Test]
	public void ValueOperation_And()
	{
		var value1 = new Variant(false);

		var value2 = new Variant(true);

		var result = value1.And(value2);

		Assert.IsFalse(result.BoolValue);
	}

	[Test]
	public void ValueOperation_Or()
	{
		var value1 = new Variant(false);

		var value2 = new Variant(true);

		var result = value1.Or(value2);

		Assert.IsTrue(result.BoolValue);
	}
}