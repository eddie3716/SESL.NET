using SESL.NET.Compilation;
using NUnit.Framework;
using System;
using SESL.NET.InfixNotation;

namespace SESL.NET.Test
{
    
    
    /// <summary>
    ///This is a test class for ScannerTest and is intended
    ///to contain all ScannerTest Unit Tests
    ///</summary>
    [TestFixture]
    public class InfixNotationScannerTest
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void InfixNotationScanner_MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void InfixNotationScanner_MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Scanner Constructor
        ///</summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InfixNotationScanner_ScannerConstructorTest()
        {
            string sourceText = string.Empty;
            InfixNotationScanner target = new InfixNotationScanner(sourceText);
        }

        /// <summary>
        ///A test for Scanner Constructor
        ///</summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InfixNotationScanner_ScannerConstructorTest1()
        {
            string sourceText = null;
            InfixNotationScanner target = new InfixNotationScanner(sourceText);
        }

        /// <summary>
        ///A test for Scanner Constructor
        ///</summary>
        [Test]
        public void InfixNotationScanner_ScannerConstructorTest2()
        {
            string sourceText = "67";
            InfixNotationScanner target = new InfixNotationScanner(sourceText);
            Assert.IsTrue(target != null);
        }

        /// <summary>
        ///A test for GetCharacter
        ///</summary>
        [Test]
        public void InfixNotationScanner_GetCharacterTest()
        {
            InfixNotationScanner target = new InfixNotationScanner("%");
            char expected = '%';
            char actual;
            actual = target.Get();
            Assert.AreEqual(expected, actual);
  
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [Test]
        public void InfixNotationScanner_NextTest()
        {
            InfixNotationScanner target = new InfixNotationScanner("%");
            bool actual;
            actual = target.Next();
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [Test]
        public void InfixNotationScanner_NextTest1()
        {
            InfixNotationScanner target = new InfixNotationScanner("%");
            bool actual;
            target.Next();
            actual = target.Next();
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for Previous
        ///</summary>
        [Test]
        public void InfixNotationScanner_PreviousTest()
        {
            InfixNotationScanner target = new InfixNotationScanner("6");
            bool actual;
            actual = target.Previous();
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///A test for Previous
        ///</summary>
        [Test]
        public void InfixNotationScanner_PreviousTest1()
        {
            InfixNotationScanner target = new InfixNotationScanner("6");
            bool actual;
            target.Next();
            actual = target.Previous();
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for CurrentIndex
        ///</summary>
        [Test]
        public void InfixNotationScanner_CurrentIndexTest()
        {
            InfixNotationScanner target = new InfixNotationScanner("67");
            int expected = -1;
            int actual;
            actual = target.CurrentIndex;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CurrentIndex
        ///</summary>
        [Test]
        public void InfixNotationScanner_CurrentIndexTest1()
        {
            InfixNotationScanner target = new InfixNotationScanner("67");
            int expected = 0;
            int actual;
            target.Next();
            actual = target.CurrentIndex;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SourceText
        ///</summary>
        [Test]
        public void InfixNotationScanner_SourceTextTest()
        {
            InfixNotationScanner target = new InfixNotationScanner("67");
            string actual;
            actual = target.Source;
            Assert.AreEqual("67", actual);
        }
    }
}
