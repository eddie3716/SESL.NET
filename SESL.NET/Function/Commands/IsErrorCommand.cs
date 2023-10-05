namespace SESL.NET.Function.Commands
{
    public class IsErrorCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Value Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Value[] operands)
        {
            try
            {
                if (functionNode.Functions[0] != null)
                {
                    functionNode.Functions[0].Evaluate(externalFunctionValueProvider);
                }
                return new Value(false);
            }
            catch
            {
                return new Value(true);
            }
        }
    }
}
