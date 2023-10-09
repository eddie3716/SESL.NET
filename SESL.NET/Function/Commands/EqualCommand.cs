namespace SESL.NET.Function.Commands;

class EqualCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
{
    public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
    {
        return operands[0].IsEqualTo(operands[1]);
    }
}