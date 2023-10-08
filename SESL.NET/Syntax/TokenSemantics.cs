using System;

namespace SESL.NET.Syntax
{
    public struct TokenSemantics
	{
		private TokenType _type;
		public TokenType Type 
		{
			get
			{
				return _type;
			}
			private set
			{
				_type = value;
			}
		}

		private int _nestedFunctions;
		public int NestedFunctions 
		{
			get
			{
				return _nestedFunctions;
			}
			private set
			{
				_nestedFunctions = value;
			}
		}

		private int _operands;
		public int Operands
		{
			get
			{
				return _operands;
			}
			private set
			{
				_operands = value;
			}
		}

		public TokenSemantics(TokenType type, int operands = 0, int nestedFunctions = 0)
		{
			_type = type;
			_nestedFunctions = nestedFunctions;
			_operands = operands;
		}

		public override bool Equals(object obj)
		{
			bool result = false;

            if (obj is TokenSemantics semantics)
            {
                result = NestedFunctions == semantics.NestedFunctions && Type == semantics.Type && Operands == semantics.Operands;
            }

            return result;
		}

        public bool IsOperator => Operands > 0;

        public int Precedence
		{
			get
			{
				switch (Type)
				{
					case TokenType.LeftParenthesis:
					case TokenType.RightParenthesis:
					case TokenType.If:
					case TokenType.Case:
					case TokenType.IsError:
						return 1;
					case TokenType.UnaryMinus:
						return 2;
					case TokenType.Exponent:
						return 3;
					case TokenType.Multiply:
					case TokenType.Divide:
						return 4;
					case TokenType.Plus:
					case TokenType.Minus:
						return 5;
					case TokenType.Comma:
					case TokenType.AbsoluteValue:
					case TokenType.Max:
					case TokenType.Min:
					case TokenType.Sine:
					case TokenType.Cosine:
					case TokenType.Tangent:
					case TokenType.ArcSine:
					case TokenType.ArcCosine:
					case TokenType.ArcTangent:
					case TokenType.HyperbolicSine:
					case TokenType.HyperbolicCosine:
					case TokenType.HyperbolicTangent:
					case TokenType.NaturalLogarithm:
					case TokenType.Logarithm:
					case TokenType.LogarithmBase10:
					case TokenType.EToThePower:
					case TokenType.SquareRoot:
					case TokenType.Modulus:
						return 6;
					case TokenType.GreaterThan:
					case TokenType.GreaterThanOrEqual:
					case TokenType.LessThan:
					case TokenType.LessThanOrEqual:
						return 7;
					case TokenType.Equal:
					case TokenType.NotEqual:
						return 8;
					case TokenType.And:
						return 9;
					case TokenType.Or:
						return 10;
					case TokenType.Return:
						return 9999;
				}
				return 0;
			}
		}

		public override int GetHashCode()
        {
            return HashCode.Combine(NestedFunctions, Type);
        }

        public static bool operator ==(TokenSemantics left, TokenSemantics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TokenSemantics left, TokenSemantics right)
        {
            return !(left == right);
        }

    }
}
