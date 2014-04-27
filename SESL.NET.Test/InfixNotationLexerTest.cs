using SESL.NET.Compilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SESL.NET.InfixNotation;
using SESL.NET.Syntax;

namespace SESL.NET.Test
{
    
    
    /// <summary>
    ///This is a test class for LexerTest and is intended
    ///to contain all LexerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InfixNotationLexerTest
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
        //public void InfixNotationLexer_MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void InfixNotationLexer_MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Lexer Constructor
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_LexerConstructorTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner(" 1 + 1 - somevar + func ^ 2   ");
            InfixNotationLexer target = new InfixNotationLexer(grammar, scanner);
            Assert.IsInstanceOfType(target, typeof(ILexer));
        }

        /// <summary>
        ///A test for GetToken
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_GetTokenTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner(" 1 + 1 - somevar + func ^ 2   "); ;
            ILexer target = new InfixNotationLexer(grammar, scanner);
            Token expected = new Token("1", new TokenSemantics(TokenType.Value, 0));
            Token actual;
            target.Next();
            actual = target.GetToken();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_NextTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner(" 1 + 1 - somevar + func ^ 2   ");
            ILexer target = new InfixNotationLexer(grammar, scanner);
            bool actual;
            actual = target.Next();
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for white space
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_WhiteSpaceTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            String sourceText = " \t  \n \r    _myfunc_ ";
            IScanner<char, string> scanner = new InfixNotationScanner(sourceText);
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target = lexer.GetToken();

            Token actual = new Token("_myfunc_", new TokenSemantics(TokenType.ExternalFunction, 0));

            Assert.AreEqual(actual, target);
        }

        /// <summary>
        ///A test for white space
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_MultiTokenTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            String sourceText = " \t  \n \r    _myfunc_ + _myvar_ > 5 * (6)  \r ";
            IScanner<char, string> scanner = new InfixNotationScanner(sourceText);
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target1 = lexer.GetToken();
            Token actual1 = new Token("_myfunc_", new TokenSemantics(TokenType.ExternalFunction));
            Assert.AreEqual(actual1, target1);

            lexer.Next();
            Token target2 = lexer.GetToken();
            Token actual2 = new Token("+", new TokenSemantics(TokenType.Plus, 2, 0));
            Assert.AreEqual(actual2, target2);

            lexer.Next();
            Token target3 = lexer.GetToken();
            Token actual3 = new Token("_myvar_", new TokenSemantics(TokenType.ExternalFunction));
            Assert.AreEqual(actual3, target3);

            lexer.Next();
            Token target4 = lexer.GetToken();
            Token actual4 = new Token(">", new TokenSemantics(TokenType.GreaterThan, 2));
            Assert.AreEqual(actual4, target4);

            lexer.Next();
            Token target5 = lexer.GetToken();
            Token actual5 = new Token("5", new TokenSemantics(TokenType.Value));
            Assert.AreEqual(actual5, target5);

            lexer.Next();
            Token target6 = lexer.GetToken();
            Token actual6 = new Token("*", new TokenSemantics(TokenType.Multiply, 2));
            Assert.AreEqual(actual6, target6);

            lexer.Next();
            Token target7 = lexer.GetToken();
            Token actual7 = new Token("(", new TokenSemantics(TokenType.LeftParenthesis));
            Assert.AreEqual(actual7, target7);

            lexer.Next();
            Token target8 = lexer.GetToken();
            Token actual8 = new Token("6", new TokenSemantics(TokenType.Value));
            Assert.AreEqual(actual8, target8);

            lexer.Next();
            Token target9 = lexer.GetToken();
            Token actual9 = new Token(")", new TokenSemantics(TokenType.RightParenthesis));
            Assert.AreEqual(actual9, target9);
        }

        /// <summary>
        ///A test for ProcessIdentifier
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessIdentifierTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner(" iserror  ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            Token expected = new Token("iserror", new TokenSemantics(TokenType.IsError, 0, 1));
            lexer.Next();
            Token actual = lexer.GetToken();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProcessNumber
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessNumberTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner("   123.321 ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            Token expected = new Token("123.321", new TokenSemantics(TokenType.Value));
            lexer.Next();
            Token actual = lexer.GetToken();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProcessString
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessStringTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner("  ' 123.321 ' ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            Token expected = new Token(" 123.321 ", new TokenSemantics(TokenType.Value));
            lexer.Next();
            Token actual = lexer.GetToken();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProcessSymbol
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessSymbolTest()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner("    >= ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target = lexer.GetToken();
 
            Token actual = new Token(">=", new TokenSemantics(TokenType.GreaterThanOrEqual, 2));

            Assert.AreEqual(actual, target);
        }

        /// <summary>
        ///A test for ProcessSymbol
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessSymbolTest1()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner("  >   ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target = lexer.GetToken();

            Token actual = new Token(">", new TokenSemantics(TokenType.GreaterThan, 2));

            Assert.AreEqual(actual, target);
        }

        /// <summary>
        ///A test for ProcessFunction
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessUnknownIdentifierTest1()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner(" _myfunc_ ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target = lexer.GetToken();

            Token actual = new Token("_myfunc_", new TokenSemantics(TokenType.ExternalFunction));

            Assert.AreEqual(actual, target);
        }

        /// <summary>
        ///A test for ProcessVariable
        ///</summary>
        [TestMethod()]
        public void InfixNotationLexer_ProcessUnknownIdentifierTest2()
        {
            IGrammar grammar = new InfixNotationGrammar();
            IScanner<char, string> scanner = new InfixNotationScanner("  _myvar_   ");
            ILexer lexer = new InfixNotationLexer(grammar, scanner);

            lexer.Next();
            Token target = lexer.GetToken();

            Token actual = new Token("_myvar_", new TokenSemantics(TokenType.ExternalFunction));

            Assert.AreEqual(actual, target);
        }
    }
}
