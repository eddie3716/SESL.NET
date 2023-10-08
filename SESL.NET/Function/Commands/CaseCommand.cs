namespace SESL.NET.Function.Commands
{
    public class CaseCommand<TExternalFunctionKey>: IFunctionCommand<TExternalFunctionKey>
	{
        public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands) => functionNode.Functions[0].Evaluate(externalFunctionValueProvider).ToBoolean() ?
                functionNode.Functions[1].Evaluate(externalFunctionValueProvider) :
                Variant.Void;
    }
}
