using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Function.Commands;
using SESL.NET.Syntax;

namespace SESL.NET.Function
{
	public class FunctionNode<TExternalFunctionKey>
	{
		private static FunctionCommands<TExternalFunctionKey> _functionCommands = new FunctionCommands<TExternalFunctionKey>();

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
			else if (!Object.ReferenceEquals(this.Value, null))
			{
				theBuilder.AppendFormat(" [V: {0}] ", Value.ToString());
			}
			else
			{
				theBuilder.AppendFormat(" [T: {0}] ", Semantics.Type);
			}

			foreach (var function in this.Functions)
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

			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			var second = obj as FunctionNode<TExternalFunctionKey>;
			if (second == null)
			{
				return false;
			}

			return Object.Equals(this.ExternalFunctionKey, second.ExternalFunctionKey)
					&&
					this.Semantics.Equals(second.Semantics)
					&&
					Object.Equals(this.Value, second.Value);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + this.Semantics.GetHashCode();
				hash = hash * 23 + this.Value.GetHashCode();
				hash = hash * 23 + this.Functions.GetHashCode();
				hash = hash * 23 + this.ExternalFunctionKey.GetHashCode();

				return hash;
			}
		}

		internal FunctionNode<TExternalFunctionKey> Clone()
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.Clone());
			}

			newFunctionNode.ExternalFunctionKey = this.ExternalFunctionKey;
			newFunctionNode.Value = this.Value;
			newFunctionNode.Semantics = this.Semantics;
			return newFunctionNode;
		}

		internal FunctionNode<TExternalFunctionKey> CloneForDerivative(TExternalFunctionKey functionKey, Value modifier)
		{
			var newFunctionNode = new FunctionNode<TExternalFunctionKey>();

			foreach (var function in Functions)
			{
				newFunctionNode.Functions.Add(function.CloneFunctionWithVariableModifier(functionKey, modifier));
			}

			newFunctionNode.ExternalFunctionKey = this.ExternalFunctionKey;
			newFunctionNode.Value = this.Value;
			newFunctionNode.Semantics = this.Semantics;
			return newFunctionNode;
		}

		public FunctionCommands<TExternalFunctionKey>.FunctionCommand FunctionCommand
		{
			get
			{
				return _functionCommands[this.Semantics.Type];
			}
		}
	}
}
