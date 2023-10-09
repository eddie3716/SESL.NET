namespace SESL.NET.Function.Commands
{
    public class AndOptimizedCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
		{
			return new Variant(functionNode.Functions[0].Evaluate(externalFunctionValueProvider).BoolValue && functionNode.Functions[1].Evaluate(externalFunctionValueProvider).BoolValue);
		}
	}
}
