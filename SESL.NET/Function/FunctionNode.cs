using System;
using System.Collections.Generic;
using System.Text;
using SESL.NET.Function.Commands;
using SESL.NET.Syntax;

namespace SESL.NET.Function
{
    public class FunctionNode<TExternalFunctionKey>
	{
		private static readonly FunctionCommands<TExternalFunctionKey> _functionCommands = new();

		public TExternalFunctionKey ExternalFunctionKey;
		public IList<Function<TExternalFunctionKey>> Functions = new List<Function<TExternalFunctionKey>>();
		public Variant Variant = null;
		public TokenSemantics Semantics;

		public override string ToString()
		{
			var theBuilder = new StringBuilder();
			if (Semantics.Type == TokenType.ExternalFunction)
			{
				theBuilder.AppendFormat(" [EF: {0}]", ExternalFunctionKey);
			}
			else if (Variant is not null)
			{
				theBuilder.AppendFormat(" [V: {0}] ", Variant.ToString());
			}
			else
			{
				theBuilder.AppendFormat(" [T: {0}] ", Semantics.Type);
			}

			foreach (var function in Functions)
			{
				theBuilder.AppendFormat(" F: {0}", function);
			}
			return theBuilder.ToString();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj is null)
			{
				return false;
			}

            if (obj is not FunctionNode<TExternalFunctionKey> second)
            {
                return false;
            }

            return Object.Equals(ExternalFunctionKey, second.ExternalFunctionKey)
					&&
					Semantics.Equals(second.Semantics)
					&&
					Object.Equals(Variant, second.Variant);
		}

		public override int GetHashCode()
        {
            return HashCode.Combine(Semantics, Variant, Functions, ExternalFunctionKey);
        }

        internal FunctionNode<TExternalFunctionKey> Clone()
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.Clone());
			}

			newFunctionNode.ExternalFunctionKey = ExternalFunctionKey;
			newFunctionNode.Variant = Variant;
			newFunctionNode.Semantics = Semantics;
			return newFunctionNode;
		}

		internal FunctionNode<TExternalFunctionKey> CloneForDerivative(TExternalFunctionKey functionKey, Variant modifier)
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.CloneFunctionWithVariableModifier(functionKey, modifier));
			}

			newFunctionNode.ExternalFunctionKey = ExternalFunctionKey;
			newFunctionNode.Variant = Variant;
			newFunctionNode.Semantics = Semantics;
			return newFunctionNode;
		}

        public FunctionCommands<TExternalFunctionKey>.FunctionCommand FunctionCommand => _functionCommands[Semantics.Type];
    }
}
