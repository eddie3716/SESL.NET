using SESL.NET.Function;
using SESL.NET.Compilation;

namespace SESL.NET.InfixNotation;

public class InfixNotationCompiler : ICompiler
{
	public InfixNotationCompiler()
	{
	}

	public Function<TExternalFunctionKey> Compile<TExternalFunctionKey>(IExternalFunctionKeyProvider<TExternalFunctionKey> externalFunctionKeyProvider, string expression)
	{
		var scanner = new InfixNotationScanner(expression);
		var lexer = new InfixNotationLexer(new InfixNotationGrammar(), scanner);
		var parser = new InfixNotationParser(lexer);
		var optimizer = new InfixNotationOptimizer();

		var unOptimizedFunctionNodes = parser.GetFunctionNodes(externalFunctionKeyProvider);

		var optimizedFunctionNodes = optimizer.Optimize(unOptimizedFunctionNodes);

		return optimizedFunctionNodes.ToFunction();
	}
}