using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
    public class ReturnCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            throw new FunctionReturnException(operands[0]);
        }
    }
}
