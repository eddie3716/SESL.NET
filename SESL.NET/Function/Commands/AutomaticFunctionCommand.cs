namespace SESL.NET.Function.Commands;

public class AutomaticFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
{
	public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
	{
		return functionNode.FunctionCommand(functionNode, externalFunctionValueProvider, operands);
	}
}
