using SESL.NET.InfixNotation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SESL.NET.Function;
using System.Collections.Generic;
using SESL.NET.Syntax;

namespace SESL.NET.Test
{
	
	
	/// <summary>
	///This is a test class for InfixNotationOptimizerTest and is intended
	///to contain all InfixNotationOptimizerTest Unit Tests
	///</summary>
	[TestClass()]
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
		public void OptimizeTestHelper<TExternalFunctionKey>()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer(); // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> functionNodes = null; // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> expected = null; // TODO: Initialize to an appropriate value
			IList<FunctionNode<TExternalFunctionKey>> actual;
			actual = target.Optimize<TExternalFunctionKey>(functionNodes);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void InfixNotationLexer_OptimizeSimple_Success()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize<int>(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[TestMethod()]
		public void InfixNotationLexer_OptimizeSimpleAnd_Success()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.And, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Value = new Value(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						).ToFunction()
						,
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Value = new Value(2),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						).ToFunction()
					}
						
				}
			};
			
			var actualOptimizedFunctionNodes = target.Optimize<int>(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[TestMethod()]
		public void InfixNotationLexer_OptimizeComplexAnd_Success()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.And, 2),
				},
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.And, 2),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.And, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
										,
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
									}
								}
							}
						).ToFunction()
						,
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Semantics = new TokenSemantics(TokenType.AndOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
										,
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
									}
								}
							}
						).ToFunction()
					}
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize<int>(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[TestMethod()]
		public void InfixNotationLexer_OptimizeSimpleOr_Success()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Or, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Value = new Value(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						).ToFunction()
						,
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Value = new Value(2),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						).ToFunction()
					}
						
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize<int>(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}

		[TestMethod()]
		public void InfixNotationLexer_OptimizeComplexOr_Success()
		{
			InfixNotationOptimizer target = new InfixNotationOptimizer();

			IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Or, 2),
				},
				new FunctionNode<int>
				{
					Value = new Value(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Value = new Value(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Or, 2),
				},
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.Or, 2),
				}
			};

			IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new FunctionNode<int>
				{
					Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
					, Functions = new List<Function<int>>
					{
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
										,
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
									}
								}
							}
						).ToFunction()
						,
						(
							new List<FunctionNode<int>> 
							{
								new FunctionNode<int>
								{
									Semantics = new TokenSemantics(TokenType.OrOptimized, 0, 2)
									, Functions = new List<Function<int>>
									{
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
										,
										(
											new List<FunctionNode<int>> 
											{
												new FunctionNode<int>
												{
													Value = new Value(2),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										).ToFunction()
									}
								}
							}
						).ToFunction()
					}
				}
			};

			var actualOptimizedFunctionNodes = target.Optimize<int>(unOptimizedFunctionNodes);

			Assert.IsTrue(expectedOptimizedFunctionNodes.IsEqual(actualOptimizedFunctionNodes), "expectedOptimizedFunctionNodes doesn't equal actualOptimizedFunctionNodes");
		}
	}
}
