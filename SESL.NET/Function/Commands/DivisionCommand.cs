namespace SESL.NET.Function.Commands
{
    public class DivisionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return operands[0] / operands[1];
        }
    }
}
