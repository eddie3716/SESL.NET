using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SESL.NET.Test
{
	/// <summary>
	/// Summary description for ValueOperationsTest
	/// </summary>
	[TestClass]
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

		[TestMethod]
		public void ValueOperation_Negate()
		{
			var value = new Value(3.0);
			var negatedInt = value.Negate().ToInt32();
			Assert.AreEqual(negatedInt, -3);
		}

		[TestMethod]
		public void ValueOperation_Addition()
		{
			var value1 = new Value(3.0);
			var value2 = new Value(5);
			var result = value1 + value2;
			Assert.AreEqual(result.ToInt32(), 8);
		}

		[TestMethod]
		public void ValueOperation_Addition_Ones()
		{
			var value1 = Value.One;
			var value2 = Value.One;
			var result = value1 + value2;
			Assert.AreEqual(result.ToInt32(), 2);
		}

		[TestMethod]
		public void ValueOperation_Subtraction()
		{
			var value1 = new Value(3.0);
			var value2 = new Value(5);
			var result = value1 - value2;
			Assert.AreEqual(result.ToInt32(), -2);
		}

		[TestMethod]
		public void ValueOperation_Modulo()
		{
			var value1 = new Value(8.0);
			var value2 = new Value(2);
			var result = value1 % value2;
			Assert.AreEqual(result.ToInt32(), Value.Zero.ToInt32());
		}

		[TestMethod]
		public void ValueOperation_Multiply()
		{
			var value1 = new Value(8.0);
			var value2 = new Value(2);
			var result = value1 * value2;
			Assert.AreEqual(result.ToInt32(), 16);
		}

		[TestMethod]
		public void ValueOperation_Divide()
		{
			var value1 = new Value(8.0);
			var value2 = new Value(2);
			var result = value1 / value2;
			Assert.AreEqual(result.ToInt32(), 4);
		}

		[TestMethod]
		public void ValueOperation_RecursivePow1()
		{
			Int64 baseInt = 13;
			Int64 exponentInt = 5;
			Int64 result = Value.RecursivePow(ref baseInt, exponentInt);
			Assert.AreEqual(result, 371293L);
		}

		[TestMethod]
		public void ValueOperation_RecursivePow2()
		{
			Decimal baseDecimal = 13M;
			Int64 exponentInt = 5;
			Decimal result = Value.RecursivePow(ref baseDecimal, exponentInt);
			Assert.AreEqual(result, 371293M);
		}

		[TestMethod]
		public void ValueOperation_Pow()
		{
			var baseValue = new Value(13M);
			var exponent = new Value(5L);

			var result = Value.Pow(ref baseValue, ref exponent);

			Assert.AreEqual(result.ToDecimal(), 371293M);
		}

		[TestMethod]
		public void ValueOperation_Abs()
		{
			var value = new Value(-13M);

			var result = Value.Abs(ref value);

			Assert.AreEqual(result.ToDecimal(), 13M);
		}

		[TestMethod]
		public void ValueOperation_Min()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = Value.Min(ref value1, ref value2);

			Assert.AreEqual(result.ToDecimal(), -13M);
		}

		[TestMethod]
		public void ValueOperation_Max()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = Value.Max(ref value1, ref value2);

			Assert.AreEqual(result.ToDecimal(), 13M);
		}

		[TestMethod]
		public void ValueOperation_GreaterThan1()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan2()
		{
			var value1 = new Value(13M);

			var value2 = new Value(-13M);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan3()
		{
			var value1 = new Value(13M);

			var value2 = Value.Zero;

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan4()
		{
			var value1 = new Value(13M);

			var value2 = Value.One;

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan5()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-1);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan6_1()
		{
			var value1 = new Value(1);

			var value2 = new Value(0);

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan6_2()
		{
			var value1 = Value.One;

			var value2 = Value.Zero;

			var result = value1 > value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan7_1()
		{
			var value1 = new Value(0);

			var value2 = new Value(1);

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThan7_2()
		{
			var value1 = Value.Zero;

			var value2 = Value.One;

			var result = value1 > value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual1()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = value1 >= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual2()
		{
			var value1 = new Value(13M);

			var value2 = new Value(-13M);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual3()
		{
			var value1 = new Value(13M);

			var value2 = Value.Zero;

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual4()
		{
			var value1 = new Value(13M);

			var value2 = Value.One;

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual5()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-1);

			var result = value1 >= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual6()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-13L);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual7_1()
		{
			var value1 = new Value(1);

			var value2 = new Value(0);

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_GreaterThanOrEqual7_2()
		{
			var value1 = Value.One;

			var value2 = Value.Zero;

			var result = value1 >= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan1()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan2()
		{
			var value1 = new Value(13M);

			var value2 = new Value(-13M);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan3()
		{
			var value1 = new Value(13M);

			var value2 = Value.Zero;

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan4()
		{
			var value1 = new Value(13M);

			var value2 = Value.One;

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan5()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-1);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan6_1()
		{
			var value1 = Value.One;

			var value2 = Value.Zero;

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan6_2()
		{
			var value1 = new Value(1);

			var value2 = new Value(0);

			var result = value1 < value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan7_1()
		{
			var value1 = new Value(0);

			var value2 = new Value(1);

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThan7_2()
		{
			var value1 = Value.Zero;

			var value2 = Value.One;

			var result = value1 < value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual1()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13M);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual2()
		{
			var value1 = new Value(13M);

			var value2 = new Value(-13M);

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual3()
		{
			var value1 = new Value(13M);

			var value2 = Value.Zero;

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual4()
		{
			var value1 = new Value(13M);

			var value2 = Value.One;

			var result = value1 <= value2;

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual5()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-1);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual6()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-13L);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_LessThanOrEqual7_1()
		{
			var value1 = new Value(0);

			var value2 = new Value(1);

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}
		
		[TestMethod]
		public void ValueOperation_LessThanOrEqual7_2()
		{
			var value1 = Value.Zero;

			var value2 = Value.One;

			var result = value1 <= value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_NotEquals()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(13L);

			var result = value1 != value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_Equals()
		{
			var value1 = new Value(-13M);

			var value2 = new Value(-13L);

			var result = value1 == value2;

			Assert.IsTrue(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_And()
		{
			var value1 = new Value(false);

			var value2 = new Value(true);

			var result = Value.And(ref value1, ref value2);

			Assert.IsFalse(result.ToBoolean());
		}

		[TestMethod]
		public void ValueOperation_Or()
		{
			var value1 = new Value(false);

			var value2 = new Value(true);

			var result = Value.Or(ref value1, ref value2);

			Assert.IsTrue(result.ToBoolean());
		}
	}
}
