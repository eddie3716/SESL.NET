using System.Globalization;

namespace SESL.NET.Function.Commands
{
    public class IfCommand<TExternalFunctionKey>: IFunctionCommand<TExternalFunctionKey>
    {
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
        {
            return functionNode.Functions[0].Evaluate(externalFunctionValueProvider).BoolValue ?
                functionNode.Functions[1].Evaluate(externalFunctionValueProvider) :
                functionNode.Functions[2].Evaluate(externalFunctionValueProvider);
        }
    }
}
