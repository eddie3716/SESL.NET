namespace SESL.NET.Function.Commands
{
    public class ExponentCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return Variant.Pow(ref operands[0], ref operands[1]);
        }
    }
}
