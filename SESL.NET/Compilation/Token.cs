using System;
using System.Collections.Generic;
using System.Text;
using SESL.NET.Syntax;

namespace SESL.NET.Compilation
{
	public struct Token 
	{
        private string _name;
		public string Name 
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        private TokenSemantics _semantics;
        public TokenSemantics Semantics 
        {
            get
            {
                return _semantics;
            }
            private set
            {
                _semantics = value;
            }
        }

        public Token(string name, TokenSemantics tokenSemantics)
        {
            _name = name;
            _semantics = tokenSemantics;
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(TokenType), Semantics.Type) + ": " + (String.IsNullOrEmpty(Name) ? String.Empty : Name);
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Token)
            {
                var token = (Token)obj;

                result = this.Name.Equals(token.Name) && this.Semantics.Equals(token.Semantics);
            }
           
            return result;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + this.Semantics.GetHashCode();
                hash = hash * 23 + this.Name.GetHashCode();

                return hash;
            }
        }
    }
}