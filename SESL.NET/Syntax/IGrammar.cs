using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Syntax
{
    public interface IGrammar
    {
        IEnumerable<string> OneCharSymbols { get; }

        IEnumerable<string> TwoCharSymbols { get; }

        IEnumerable<char> IdentifierStartCharacters { get; }

        IEnumerable<char> IdentifierCharacters { get; }

        string NegativeSymbol { get; }

        string UnaryMinusSymbol { get; }

        IEnumerable<char> NumberStartCharacters { get; }

        IEnumerable<char> NumberCharacters { get; }

        IEnumerable<char> WhiteSpaceCharacters { get; }

        IEnumerable<char> StringChars { get; }

        TokenSemantics this[string key] { get; }

        bool ContainsKey(string key);
    }
}
