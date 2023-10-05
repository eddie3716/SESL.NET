using System.Collections.Generic;
using System.Linq;
using SESL.NET.Syntax;

namespace SESL.NET.InfixNotation
{
    public class InfixNotationGrammar : IGrammar
	{
		private static readonly string STRING_CHARS = "'\"";
		private static readonly string IDENTIFIER_STARTCHARS = "abcdefghijklmnopqrstuvzwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ{}[]_$:";
		private static readonly string IDENTIFIER_CHARS      = "abcdefghijklmnopqrstuvzwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ{}[]_$:0123456789";

		private static readonly string NUMBER_STARTCHARS     = "01234567890.";
		private static readonly string NUMBER_CHARS          = "01234567890.";

		private static readonly string NEGATIVE_SYMBOL       = "-";
		private static readonly string UNARY_NEGATIVE_SYMBOL = "~";

		private static readonly string WHITESPACE_CHARS      = " \t\n\r";

		private static readonly Dictionary<string, TokenSemantics> _innerDictionary = new()
		{
			{"+", new TokenSemantics(TokenType.Plus, 2)},
			{"-", new TokenSemantics(TokenType.Minus, 2)},
			{"*", new TokenSemantics(TokenType.Multiply, 2)},
			{"/", new TokenSemantics(TokenType.Divide, 2)},
			{"^", new TokenSemantics(TokenType.Exponent, 2)},
			{"~", new TokenSemantics(TokenType.UnaryMinus, 1)},
			{">", new TokenSemantics(TokenType.GreaterThan, 2)},
			{">=", new TokenSemantics(TokenType.GreaterThanOrEqual, 2)},
			{"<", new TokenSemantics(TokenType.LessThan, 2)},
			{"<=", new TokenSemantics(TokenType.LessThanOrEqual, 2)},
			{"!=", new TokenSemantics(TokenType.NotEqual, 2)},
			{"<>", new TokenSemantics(TokenType.NotEqual, 2)},
			{"=", new TokenSemantics(TokenType.Equal, 2)},
			{"(", new TokenSemantics(TokenType.LeftParenthesis)},
			{")", new TokenSemantics(TokenType.RightParenthesis)},
			{",", new TokenSemantics(TokenType.Comma)},
			{"or", new TokenSemantics(TokenType.Or, 2)},
			{"||", new TokenSemantics(TokenType.Or, 2)},
			{"and", new TokenSemantics(TokenType.And, 2)},
			{"return", new TokenSemantics(TokenType.Return, 1)},
			{"&&", new TokenSemantics(TokenType.And, 2)},
			{"abs", new TokenSemantics(TokenType.AbsoluteValue, 1)},
			{"min", new TokenSemantics(TokenType.Min, 2)},
			{"max", new TokenSemantics(TokenType.Max, 2)},
			{"sin", new TokenSemantics(TokenType.Sine, 2)},
			{"cos", new TokenSemantics(TokenType.Cosine, 2)},
			{"tan", new TokenSemantics(TokenType.Tangent, 2)},
			{"asin", new TokenSemantics(TokenType.ArcSine, 2)},
			{"acos", new TokenSemantics(TokenType.ArcCosine, 2)},
			{"atan", new TokenSemantics(TokenType.ArcTangent, 2)},
			{"atan2", new TokenSemantics(TokenType.ArcTangent2, 2)},
			{"sinh", new TokenSemantics(TokenType.HyperbolicSine, 2)},
			{"cosh", new TokenSemantics(TokenType.HyperbolicCosine, 2)},
			{"tanh", new TokenSemantics(TokenType.HyperbolicTangent, 2)},
			{"log", new TokenSemantics(TokenType.Logarithm, 2)},
			{"ln", new TokenSemantics(TokenType.NaturalLogarithm, 1)},
			{"log10", new TokenSemantics(TokenType.LogarithmBase10, 1)},
			{"sqrt", new TokenSemantics(TokenType.SquareRoot, 1)},
			{"mod", new TokenSemantics(TokenType.Modulus, 2)},
			{"Root", new TokenSemantics(TokenType.RootNewtonsMethod, 0, 2)},
			{"Root1", new TokenSemantics(TokenType.RootNewtonsMethod2, 0, 2)},
			{"if", new TokenSemantics(TokenType.If, 0, 3)},
			{"case", new TokenSemantics(TokenType.Case, 0, 2)},
			{"iserror", new TokenSemantics(TokenType.IsError, 0, 1)}
		};
		
		private readonly static IEnumerable<string> _oneCharSymbols = _innerDictionary.Keys.Where(k => k.Length == 1 && !k.Any(c => IDENTIFIER_CHARS.Contains(c)));
		private readonly static IEnumerable<string> _twoCharSymbols = _innerDictionary.Keys.Where(k => k.Length == 2 && !k.Any(c => IDENTIFIER_CHARS.Contains(c)));

		public InfixNotationGrammar() { }

        public IEnumerable<string> OneCharSymbols => _oneCharSymbols;

        public IEnumerable<string> TwoCharSymbols => _twoCharSymbols;

        public IEnumerable<char> StringChars => STRING_CHARS;

        public IEnumerable<char> IdentifierStartCharacters => IDENTIFIER_STARTCHARS;

        public IEnumerable<char> IdentifierCharacters => IDENTIFIER_CHARS;

        public string NegativeSymbol => NEGATIVE_SYMBOL;

        public string UnaryMinusSymbol => UNARY_NEGATIVE_SYMBOL;

        public IEnumerable<char> NumberStartCharacters => NUMBER_STARTCHARS;

        public IEnumerable<char> NumberCharacters => NUMBER_CHARS;

        public IEnumerable<char> WhiteSpaceCharacters => WHITESPACE_CHARS;

        public TokenSemantics this[string key] => _innerDictionary[key];

        public bool ContainsKey(string key)
		{
			return _innerDictionary.ContainsKey(key);
		}
	}
}
