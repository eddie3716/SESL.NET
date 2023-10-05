namespace SESL.NET.Function.Commands
{
    public class MaxCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return Value.Max(ref operands[0], ref operands[1]);
        }
    }
}
