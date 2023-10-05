namespace SESL.NET.Function.Commands
{
    public class NestedFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return functionNode.Functions[0].Evaluate(externalFunctionValueProvider);
        }
    }
}
