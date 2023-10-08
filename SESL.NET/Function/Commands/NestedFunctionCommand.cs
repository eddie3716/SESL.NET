namespace SESL.NET.Function.Commands
{
    public class NestedFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return functionNode.Functions[0].Evaluate(externalFunctionValueProvider);
        }
    }
}
