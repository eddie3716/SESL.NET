namespace SESL.NET.Function.Commands
{
    public class AbsoluteValueCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            return Value.Abs(ref operands[0]);
        }
    }
}
