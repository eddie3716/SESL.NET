namespace SESL.NET.Function.Commands
{
    public class AndCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return Value.And(ref operands[0], ref operands[1]);
        }
    }
}
