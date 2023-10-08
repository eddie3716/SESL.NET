using NUnit.Framework;
using SESL.NET.Compilation;
using SESL.NET.Function;
using System.Collections.Generic;
using NSubstitute;
using SESL.NET.InfixNotation;
using SESL.NET.Syntax;

namespace SESL.NET.Tests
{


    /// <summary>
    ///This is a test class for InfixNotationParserTest and is intended
    ///to contain all InfixNotationParserTest Unit Tests
    ///</summary>
    [TestFixture]
	public class InfixNotationParserTest
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

		private static IExternalFunctionKeyProvider<int> _externalFunctionKeyProvider;

		#region Additional test attributes
		[SetUp]
		public void InfixNotationParser_TestFixtureSetUp()
		{
			_externalFunctionKeyProvider = Substitute.For<IExternalFunctionKeyProvider<int>>();
		}

		#endregion


		[Test]
		[Ignore("not needed for some reason..back in 2014 this made sense why??")]
		public void InfixNotationParser_GetNestedFunctionNodesTest()
		{
			_externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName("func", out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = 1;
						return true;
					}
				);

			var grammar = new InfixNotationGrammar();
			var scanner = new InfixNotationScanner("( 1 + 1 + func ^ 2, 6, 'Whoa!'  )");
			var lexer = new InfixNotationLexer(grammar, scanner);
			var target = new InfixNotationParser(lexer);
			var expected1 = new List<FunctionNode<int>>
			{
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				},
				new() {
					ExternalFunctionKey = 1,
					Semantics = new TokenSemantics(TokenType.ExternalFunction),
					Variant = new Variant("func")
				},
				new() {
					Variant = new Variant(2), 
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Exponent, 2),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			var expected2 = new List<FunctionNode<int>>
			{
				new() {
					Variant = new Variant(6), 
					Semantics = new TokenSemantics(TokenType.Value),
				}
			};

			var expected3 = new List<FunctionNode<int>>
			{
				new() {
					Variant = new Variant("Whoa!"),
					Semantics = new TokenSemantics(TokenType.Value),
				}
			};

			var actual1 = target.GetNestedFunctionNodes(_externalFunctionKeyProvider);
			var actual2 = target.GetNestedFunctionNodes(_externalFunctionKeyProvider);
			var actual3 = target.GetNestedFunctionNodes(_externalFunctionKeyProvider);
			Assert.IsTrue(expected1.IsEqual(actual1), "Expected1 doesn't equal Actual1");
			Assert.IsTrue(expected2.IsEqual(actual2), "Expected2 doesn't equal Actual2");
			Assert.IsTrue(expected3.IsEqual(actual3), "Expected3 doesn't equal Actual3");
		}

		[Test]
		public void InfixNotationParser_GetFunctionNodesTest()
		{
			_externalFunctionKeyProvider.TryGetExternalFunctionKeyFromName("func", out Arg.Any<int>(), out Arg.Any<int>())
				.Returns(
					x =>
					{
						x[1] = 0;
						return true;
					}
				);

			var grammar = new InfixNotationGrammar();
			var scanner = new InfixNotationScanner("if( 1 + 1 - func ^ 2, 6,9  )");
			var lexer = new InfixNotationLexer(grammar, scanner);
			var target = new InfixNotationParser(lexer);

			var expected = new List<FunctionNode<int>>
			{
				new() {
					Semantics = new TokenSemantics(TokenType.If, 0, 3),
					Functions = new List<Function<int>>
					{
						new(null),
						new(null),
						new(null)
					}
				}
			};
			var actual = target.GetFunctionNodes(_externalFunctionKeyProvider);
			Assert.IsTrue(expected.IsEqual(actual));
		}
	}
}
