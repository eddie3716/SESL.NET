namespace SESL.NET.Function.Commands
{
    public class GreaterThanOrEqualCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return operands[0].IsGreaterThanOrEqualTo(operands[1]);
        }
    }
}
