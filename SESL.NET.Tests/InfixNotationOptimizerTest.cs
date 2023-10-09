using SESL.NET.InfixNotation;
using NUnit.Framework;
using SESL.NET.Function;
using System.Collections.Generic;
using SESL.NET.Syntax;

namespace SESL.NET.Tests;

[TestFixture]
public class InfixNotationOptimizerTest
{

	[Test]
	public void InfixNotationLexer_OptimizeSimple_Success()
	{
		InfixNotationOptimizer target = new();

		IList<FunctionNode<int>> unOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Plus, 2),
				}
			};

		IList<FunctionNode<int>> expectedOptimizedFunctionNodes = new List<FunctionNode<int>>
			{
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
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
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
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
									Variant = new Variant(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
						,

							new List<FunctionNode<int>>
							{
								new() {
									Variant = new Variant(2),
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
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.And, 2),
				},
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
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
													Variant = new Variant(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,

											new List<FunctionNode<int>>
											{
												new() {
													Variant = new Variant(2),
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
													Variant = new Variant(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,

											new List<FunctionNode<int>>
											{
												new() {
													Variant = new Variant(2),
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
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
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
									Variant = new Variant(1),
									Semantics = new TokenSemantics(TokenType.Value),
								}
							}
						.ToFunction()
						,

							new List<FunctionNode<int>>
							{
								new() {
									Variant = new Variant(2),
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
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Semantics = new TokenSemantics(TokenType.Or, 2),
				},
				new() {
					Variant = new Variant(1),
					Semantics = new TokenSemantics(TokenType.Value),
				},
				new() {
					Variant = new Variant(2),
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
													Variant = new Variant(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,

											new List<FunctionNode<int>>
											{
												new() {
													Variant = new Variant(2),
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
													Variant = new Variant(1),
													Semantics = new TokenSemantics(TokenType.Value),
												}
											}
										.ToFunction()
										,

											new List<FunctionNode<int>>
											{
												new() {
													Variant = new Variant(2),
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