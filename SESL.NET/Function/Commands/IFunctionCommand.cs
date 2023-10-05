namespace SESL.NET.Function.Commands
{
    public interface IFunctionCommand<TExternalFunctionKey>
    {
        Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands);
    }
}
