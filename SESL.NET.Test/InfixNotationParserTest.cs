﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Expressions;
using SESL.NET.Compilation;
using SESL.NET.Function;
using System.Collections.Generic;
using Rhino.Mocks;
using SESL.NET;
using SESL.NET.InfixNotation;
using SESL.NET.Syntax;

namespace SESL.NET.Test
{
	
	
	/// <summary>
	///This is a test class for InfixNotationParserTest and is intended
	///to contain all InfixNotationParserTest Unit Tests
	///</summary>
	[TestClass()]
	public class InfixNotationParserTest
	{
		private IExternalFunctionKeyProvider<int> _externalFunctionKeyProvider;

		public InfixNotationParserTest()
		{
			_externalFunctionKeyProvider = MockRepository.GenerateMock<IExternalFunctionKeyProvider<int>>();
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
		//public void InfixNotationParser_MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void InfixNotationParser_MyTestCleanup()
		//{
		//}
		//
		#endregion


		[TestMethod()]
		[DeploymentItem("SESL.NET.dll")]
		public void InfixNotationParser_GetNestedFunctionNodesTest()
		{

			int temp1 = 0;
			_externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName("func", out temp1, out temp1))
				.OutRef(1)
				.Return(true);

			var grammar = new InfixNotationGrammar();
			var scanner = new InfixNotationScanner("( 1 + 1 + func ^ 2, 6, 'Whoa!'  )");
			var lexer = new InfixNotationLexer(grammar, scanner);
			var target = new InfixNotationParser_Accessor(lexer);
			var expected1 = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				},
				new FunctionNode<int>
				{
					ExternalFunctionKey = 1,
					Semantics = new TokenSemantics(TokenType.ExternalFunction),
					Value = new Value("func")
				},
				new FunctionNode<int>
				{
					Value = new Value(2), 
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Exponent, 2),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			var expected2 = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(6), 
					Semantics = new TokenSemantics(TokenType.Value),
				}
			};

			var expected3 = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value("Whoa!"),
					Semantics = new TokenSemantics(TokenType.Value),
				}
			};

			var actual1 = target.GetNestedFunctionNodes<int>(_externalFunctionKeyProvider);
			var actual2 = target.GetNestedFunctionNodes<int>(_externalFunctionKeyProvider);
			var actual3 = target.GetNestedFunctionNodes<int>(_externalFunctionKeyProvider);
			_externalFunctionKeyProvider.VerifyAllExpectations();
			Assert.IsTrue(expected1.IsEqual(actual1), "Expected1 doesn't equal Actual1");
			Assert.IsTrue(expected2.IsEqual(actual2), "Expected2 doesn't equal Actual2");
			Assert.IsTrue(expected3.IsEqual(actual3), "Expected3 doesn't equal Actual3");
		}

		[TestMethod()]
		[DeploymentItem("SESL.NET.dll")]
		public void InfixNotationParser_GetFunctionNodesTest()
		{
			int temp1 = 0;
			_externalFunctionKeyProvider.Expect(context => context.TryGetExternalFunctionKeyFromName("func", out temp1, out temp1))
				.OutRef(1)
				.Return(true);

			var grammar = new InfixNotationGrammar();
			var scanner = new InfixNotationScanner("if( 1 + 1 - func ^ 2, 6,9  )");
			var lexer = new InfixNotationLexer(grammar, scanner);
			var target = new InfixNotationParser_Accessor(lexer);

			var expected = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.If, 0, 3),
					Functions = new List<Function<int>>
					{
						new Function<int>(null as List<FunctionNode<int>>),
						new Function<int>(null as List<FunctionNode<int>>),
						new Function<int>(null as List<FunctionNode<int>>)
					}
				}
			};
			var actual = target.GetFunctionNodes<int>(_externalFunctionKeyProvider);
			_externalFunctionKeyProvider.VerifyAllExpectations();
			Assert.IsTrue(expected.IsEqual(actual));
		}
	}
}
