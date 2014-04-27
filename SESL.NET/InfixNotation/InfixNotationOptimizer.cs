using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Compilation;
using SESL.NET.Function;
using SESL.NET.Exception;

namespace SESL.NET.InfixNotation
{
	public class InfixNotationOptimizer : IOptimizer
	{
		public IList<FunctionNode<TExternalFunctionKey>> Optimize<TExternalFunctionKey>(IList<FunctionNode<TExternalFunctionKey>> functionNodes)
		{
			var currentI = functionNodes.Count - 1;
			return this.Optimize(functionNodes, 0, ref currentI);
		}

		private IList<FunctionNode<TExternalFunctionKey>> Optimize<TExternalFunctionKey>(IList<FunctionNode<TExternalFunctionKey>> functionNodes, int operandsNeeded, ref int currentI)
		{
			var optimizedFunctionNodes = new List<FunctionNode<TExternalFunctionKey>>();
			int operandsFound = 0;
			
			while (currentI >= 0 && (operandsNeeded == 0 || (operandsFound != operandsNeeded)))
			{
				var currentFunctionNode = functionNodes[currentI--];
				++operandsFound;
				var currentTokenType = currentFunctionNode.Semantics.Type; 
				switch (currentTokenType)
				{
					case Syntax.TokenType.And:
					case Syntax.TokenType.Or:
						var demorganOptimizedFunctionNode = new FunctionNode<TExternalFunctionKey>();
						demorganOptimizedFunctionNode.Functions.Add(Optimize(functionNodes, 1, ref currentI).ToFunction());
						demorganOptimizedFunctionNode.Functions.Insert(0, Optimize(functionNodes, 1, ref currentI).ToFunction());
						demorganOptimizedFunctionNode.Semantics = new Syntax.TokenSemantics(currentTokenType == Syntax.TokenType.And ? Syntax.TokenType.AndOptimized : Syntax.TokenType.OrOptimized, 0, 2);
						optimizedFunctionNodes.Insert(0, demorganOptimizedFunctionNode);
						break;
					default:
						var optimizedFunctionNode = currentFunctionNode.Clone();
						optimizedFunctionNode.Functions = currentFunctionNode
															.Functions
															.Select(f => Optimize(f.GetFunctionNodes().ToList()).ToFunction()).ToList();
						
						optimizedFunctionNodes.Insert(0, optimizedFunctionNode);
						if (optimizedFunctionNode.Semantics.Operands > 0)
						{
							optimizedFunctionNodes.InsertRange(0, Optimize(functionNodes, optimizedFunctionNode.Semantics.Operands, ref currentI));
						}
						break;
				}
			}

			if (operandsNeeded > operandsFound)
			{
				throw new InsufficientOperandsException(operandsNeeded, operandsFound, "Optimizer");
			}

			return optimizedFunctionNodes;
		}
	}
}
