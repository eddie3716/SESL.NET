namespace SESL.NET.Function.Commands
{
    public class IsErrorCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            try
            {
                if (functionNode.Functions[0] != null)
                {
                    functionNode.Functions[0].Evaluate(externalFunctionValueProvider);
                }
                return new Variant(false);
            }
            catch
            {
                return new Variant(true);
            }
        }
    }
}
