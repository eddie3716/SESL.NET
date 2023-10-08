using NUnit.Framework;

namespace SESL.NET.Tests
{
    /// <summary>
    /// Summary description for ValueOperationsTest
    /// </summary>
    [TestFixture]
	public class ValueOperationTests
	{
		public ValueOperationTests()
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
		public void ValueOperation_Negate()
		{
			var value = new Variant(3.0m);
			var negatedInt = (-value).ToNumeric();
			Assert.AreEqual(negatedInt, -3m);
		}

		[Test]
		public void ValueOperation_Addition()
		{
			var value1 = new Variant(3.0m);
			var value2 = new Variant(5m);
			var result = value1 + value2;
			Assert.AreEqual(result.ToNumeric(), 8m);
		}

		[Test]
		public void ValueOperation_Addition_Ones()
		{
			var value1 = new Variant(1);
			var value2 = new Variant(1);
			var result = value1 + value2;
			Assert.AreEqual(result.ToNumeric(), 2m);
		}

		[Test]
		public void ValueOperation_Subtraction()
		{
			var value1 = new Variant(3.0m);
			var value2 = new Variant(5);
			var result = value1 - value2;
			Assert.AreEqual(result.ToNumeric(), -2m);
		}

		[Test]
		public void ValueOperation_Modulo()
		{
			var value1 = new Variant(8.0m);
			var value2 = new Variant(2);
			var result = value1 % value2;
			Assert.AreEqual(result.ToNumeric(), new Variant(0).ToNumeric());
		}

		[Test]
		public void ValueOperation_Multiply()
		{
			var value1 = new Variant(8.0m);
			var value2 = new Variant(2);
			var result = value1 * value2;
			Assert.AreEqual(result.ToNumeric(), 16);
		}

		[Test]
		public void ValueOperation_Divide()
		{
			var value1 = new Variant(8.0m);
			var value2 = new Variant(2);
			var result = value1 / value2;
			Assert.AreEqual(result.ToNumeric(), 4);
		}

		[Test]
		public void ValueOperation_Pow()
		{
			var baseValue = new Variant(13M);
			var exponent = new Variant(5L);

			var result = Variant.Pow(ref baseValue, ref exponent);

			Assert.AreEqual(result.ToNumeric(), 371293M);
		}

		[Test]
		public void ValueOperation_Abs()
		{
			var value = new Variant(-13M);

			var result = Variant.Abs(ref value);

			Assert.AreEqual(result.ToNumeric(), 13M);
		}

		[Test]
		public void ValueOperation_Min()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = Variant.Min(ref value1, ref value2);

			Assert.AreEqual(result.ToNumeric(), -13M);
		}

		[Test]
		public void ValueOperation_Max()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = Variant.Max(ref value1, ref value2);

			Assert.AreEqual(result.ToNumeric(), 13M);
		}

		[Test]
		public void ValueOperation_GreaterThan1()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan2()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(-13M);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan3()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(0);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan4()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(1);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan5()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-1);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan6_1()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan6_2()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan7_1()
		{
			var value1 = new Variant(0);

			var value2 = new Variant(1);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThan7_2()
		{
			var value1 = new Variant(0);

			var value2 = new Variant(1);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual1()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = value1 >= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual2()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(-13M);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual3()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(0);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual4()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(1);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual5()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-1);

			var result = value1 >= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual6()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-13L);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual7_1()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_GreaterThanOrEqual7_2()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan1()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan2()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(-13M);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan3()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(0);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan4()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(1);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan5()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-1);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan6_1()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan6_2()
		{
			var value1 = new Variant(1);

			var value2 = new Variant(0);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan7_1()
		{
			var value1 = new Variant(0);

			var value2 = new Variant(1);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThan7_2()
		{
			var value1 = new Variant(0);

			var value2 = new Variant(1);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual1()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13M);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual2()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(-13M);

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual3()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(0);

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual4()
		{
			var value1 = new Variant(13M);

			var value2 = new Variant(1);

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual5()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-1);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual6()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-13L);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_LessThanOrEqual7_1()
		{
			var value1 = new Variant(0);

			var value2 = new Variant(1);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}
		
		[Test]
		public void ValueOperation_LessThanOrEqual7_2()
		{
			var value1 = new Variant(0);;

			var value2 = new Variant(1);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_NotEquals()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(13L);

			var result = value1 != value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_Equals()
		{
			var value1 = new Variant(-13M);

			var value2 = new Variant(-13L);

			var result = value1 == value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_And()
		{
			var value1 = new Variant(false);

			var value2 = new Variant(true);

			var result = Variant.And(ref value1, ref value2);

			Assert.IsFalse(result.ToBoolean());
		}

		[Test]
		public void ValueOperation_Or()
		{
			var value1 = new Variant(false);

			var value2 = new Variant(true);

			var result = Variant.Or(ref value1, ref value2);

			Assert.IsTrue(result.ToBoolean());
		}
	}
}
