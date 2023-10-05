using System;
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
            return Enum.GetName(typeof(TokenType), Semantics.Type) + ": " + (string.IsNullOrEmpty(Name) ? string.Empty : Name);
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Token token)
            {

                result = Name.Equals(token.Name) && Semantics.Equals(token.Semantics);
            }


            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Semantics, Name);
        }

        public static bool operator ==(Token left, Token right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Token left, Token right)
        {
            return !(left == right);
        }
    }
}