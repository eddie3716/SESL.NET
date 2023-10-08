using SESL.NET.Exception;

namespace SESL.NET.Function.Commands
{
    public class ExternalFunctionCommand<TExternalFunctionKey> : IFunctionCommand<TExternalFunctionKey>
	{
		public Variant Execute(FunctionNode<TExternalFunctionKey> functionNode, IExternalFunctionValueProvider<TExternalFunctionKey> externalFunctionValueProvider, params Variant[] operands)
		{
			Variant value = Variant.Void;

			if (!externalFunctionValueProvider.TryGetExternalFunctionValue(functionNode.ExternalFunctionKey, out value, operands))
			{
				throw new ExternalFunctionValueNotFoundException(functionNode.ToString());
			}

			return value;
		}
	}
}
