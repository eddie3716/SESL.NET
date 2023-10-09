namespace SESL.NET.Function.Commands
{
    public class NotEqualCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return operands[0].DoesNotEqual(operands[1]);
        }
    }
}
