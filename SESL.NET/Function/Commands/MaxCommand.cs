namespace SESL.NET.Function.Commands
{
    public class MaxCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Max(ref operands[0], ref operands[1]);
        }
    }
}
