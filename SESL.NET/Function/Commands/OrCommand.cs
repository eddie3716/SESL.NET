namespace SESL.NET.Function.Commands
{
    public class OrCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Or(ref operands[0], ref operands[1]);
        }
    }
}
