namespace SESL.NET.Syntax
{
    public enum TokenType : int
	{
		Unknown,

		Value,

		//1
		LeftParenthesis,
		RightParenthesis,

		//2
		ExternalFunction,
		If,
		Case,
		IsError,
		RootNewtonsMethod,
		RootNewtonsMethod2,
		AbsoluteValue,
		Max,
		Min,
		Sine,
		Cosine,
		Tangent,
		ArcSine,
		ArcCosine,
		ArcTangent,
		ArcTangent2,
		HyperbolicSine,
		HyperbolicCosine,
		HyperbolicTangent,
		NaturalLogarithm,
		Logarithm,
		LogarithmBase10,
		EToThePower,
		SquareRoot,
		Modulus,


		//3
		Exponent,

		//4
		UnaryMinus,

		//5
		Multiply,
		Divide,

		//6
		Plus,
		Minus,

		//7
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual,

		//8
		Equal,
		NotEqual,

		//9
		And,
		AndOptimized,

		//10
		Or,
		OrOptimized,

		//11
		Comma,

		//9999
		Return
	}
}
