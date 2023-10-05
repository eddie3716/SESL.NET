using SESL.NET.InfixNotation;
using NUnit.Framework;
using SESL.NET.Function;
using System.Collections.Generic;
using SESL.NET.Syntax;

namespace SESL.NET.Tests
{


    /// <summary>
    ///This is a test class for InfixNotationOptimizerTest and is intended
    ///to contain all InfixNotationOptimizerTest Unit Tests
    ///</summary>
    [TestFixture]
	public class InfixNotationOptimizerTest
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
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for Optimize
		///</summary>
		public static void OptimizeTestHelper<TExternalFunctionKey>()
		{
			InfixNotationOptimizer target = new(); // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> functionNodes = null; // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> expected = null; // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> actual;
			actual = target.Optimize(functionNodes);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void InfixNotationLexer_OptimizeSimple_Success()
		{
			InfixNotationOptimizer target = new();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[Test]
		public void InfixNotationLexer_OptimizeSimpleAnd_Success()
		{
			InfixNotationOptimizer target = new();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.And, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						
							new List<FunctionNode<int>> 
							{
								new() {
									Value = new Value(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
						,
						
							new List<FunctionNode<int>> 
							{
								new() {
									Value = new Value(2),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
					}
						
				}
			};
			
			var actualOptimizedFunctionNodes = target.Optimize(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[Test]
		public void InfixNotationLexer_OptimizeComplexAnd_Success()
		{
			InfixNotationOptimizer target = new();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.And, 2),
				},
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.And, 2),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.And, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						
							new List<FunctionNode<int>> 
							{
								new() {
									Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
									}
								}
							}
						.ToFunction()
						,
						
							new List<FunctionNode<int>> 
							{
								new() {
									Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
									}
								}
							}
						.ToFunction()
					}
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[Test]
		public void InfixNotationLexer_OptimizeSimpleOr_Success()
		{
			InfixNotationOptimizer target = new();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Or, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						
							new List<FunctionNode<int>> 
							{
								new() {
									Value = new Value(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
						,
						
							new List<FunctionNode<int>> 
							{
								new() {
									Value = new Value(2),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
					}
						
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[Test]
		public void InfixNotationLexer_OptimizeComplexOr_Success()
		{
			InfixNotationOptimizer target = new();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Or, 2),
				},
				new() {
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Or, 2),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Or, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						
							new List<FunctionNode<int>> 
							{
								new() {
									Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
									}
								}
							}
						.ToFunction()
						,
						
							new List<FunctionNode<int>> 
							{
								new() {
									Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,
										
											new List<FunctionNode<int>> 
											{
												new() {
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
									}
								}
							}
						.ToFunction()
					}
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}
	}
}
