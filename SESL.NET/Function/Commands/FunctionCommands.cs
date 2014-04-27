using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Syntax;

namespace SESL.NET.Function.Commands
{
	public class FunctionCommands<TExternalFunctionKey>
	{
		public delegate Value FunctionCommand(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands);

		private FunctionCommand[] _functionCommands;

		public FunctionCommands()
		{
			_functionCommands = new FunctionCommand[50];

			_functionCommands[(int)TokenType.Value] = new ValueCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.If] = new IfCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Case] = new CaseCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Return] = new ReturnCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.IsError] = new IsErrorCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.ExternalFunction] = new ExternalFunctionCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.AbsoluteValue] = new AbsoluteValueCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Max] = new MaxCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Min] = new MinCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Sine] = new SineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Cosine] = new CosineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Tangent] = new TangentCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.ArcSine] = new ArcSineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.ArcCosine] = new ArcCosineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.ArcTangent] = new ArcTangentCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.ArcTangent2] = new ArcTangent2Command<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.HyperbolicSine] = new HyperbolicSineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.HyperbolicCosine] = new HyperbolicCosineCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.HyperbolicTangent] = new HyperbolicTangentCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.NaturalLogarithm] = new NaturalLogarithmCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Logarithm] = new LogarithmCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.LogarithmBase10] = new LogarithmBase10Command<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.EToThePower] = new EToThePowerCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.SquareRoot] = new SquareRootCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Modulus] = new ModulusCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Exponent] = new ExponentCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.UnaryMinus] = new UnaryMinusCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Multiply] = new MultiplicationCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Divide] = new DivisionCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Plus] = new AdditionCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Minus] = new SubtractionCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.GreaterThan] = new GreaterThanCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.GreaterThanOrEqual] = new GreaterThanOrEqualCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.LessThan] = new LessThanCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.LessThanOrEqual] = new LessThanOrEqualCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Equal] = new EqualCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.NotEqual] = new NotEqualCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.And] = new AndCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.AndOptimized] = new AndOptimizedCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.Or] = new OrCommand<TExternalFunctionKey>().Execute;
			_functionCommands[(int)TokenType.OrOptimized] = new OrOptimizedCommand<TExternalFunctionKey>().Execute;
		}

		public FunctionCommands<TExternalFunctionKey>.FunctionCommand this[TokenType tokenType]
		{
			get
			{
				return _functionCommands[(int)tokenType];
			}
		}
	}
}
