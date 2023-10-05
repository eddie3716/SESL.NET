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
		public Value Value = Value.Void;
		public TokenSemantics Semantics;

		public override string ToString()
		{
			var theBuilder = new StringBuilder();
			if (Semantics.Type == TokenType.ExternalFunction)
			{
				theBuilder.AppendFormat(" [EF: {0}]", ExternalFunctionKey);
			}
			else if (Value is not null)
			{
				theBuilder.AppendFormat(" [V: {0}] ", Value.ToString());
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

			var second = obj as FunctionNode<TExternalFunctionKey>;
			if (second == null)
			{
				return false;
			}

			return Object.Equals(ExternalFunctionKey, second.ExternalFunctionKey)
					&&
					Semantics.Equals(second.Semantics)
					&&
					Object.Equals(Value, second.Value);
		}

		public override int GetHashCode()
        {
            return HashCode.Combine(Semantics, Value, Functions, ExternalFunctionKey);
        }

        internal FunctionNode<TExternalFunctionKey> Clone()
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.Clone());
			}

			newFunctionNode.ExternalFunctionKey = ExternalFunctionKey;
			newFunctionNode.Value = Value;
			newFunctionNode.Semantics = Semantics;
			return newFunctionNode;
		}

		internal FunctionNode<TExternalFunctionKey> CloneForDerivative(TExternalFunctionKey functionKey, Value modifier)
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.CloneFunctionWithVariableModifier(functionKey, modifier));
			}

			newFunctionNode.ExternalFunctionKey = ExternalFunctionKey;
			newFunctionNode.Value = Value;
			newFunctionNode.Semantics = Semantics;
			return newFunctionNode;
		}

        public FunctionCommands<TExternalFunctionKey>.FunctionCommand FunctionCommand => _functionCommands[Semantics.Type];
    }
}
