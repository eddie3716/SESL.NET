namespace SESL.NET.Function.Commands
{
    public interface IFunctionCommand<TExternalFunctionKey>
    {
        Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands);
    }
}
