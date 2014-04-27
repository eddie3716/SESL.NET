using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SESL.NET.Compilation;
using SESL.NET.Syntax;
using SESL.NET.Exception;

namespace SESL.NET.InfixNotation
{
    public class InfixNotationLexer : ILexer
    {
        private IGrammar _grammar;
        private IScanner<char, string> _scanner;
        private Token _currentToken = new Token();
        private Token _lastToken = new Token();

        public InfixNotationLexer(IGrammar lexerData, IScanner<char, string> scanner)
        {
            _grammar = lexerData;
            _scanner = scanner;
        }

        public bool Next()
        {
            bool foundToken = false;
            
            while (!foundToken && _scanner.Next())
            {
                var character = _scanner.Get();
                    
                if (!_grammar.WhiteSpaceCharacters.Any(c => c == character))
                {
                    _lastToken = _currentToken;
                    if (_grammar.IdentifierStartCharacters.Any( c => c == character))
                    {
                        ProcessIdentifier(character);
                    }
                    else if (_grammar.NumberStartCharacters.Any( c => c == character))
                    {
                        ProcessNumber(character);
                    }
                    else if (_grammar.StringChars.Any(c => c == character))
                    {
                        ProcessString(character);
                    }
                    else
                    {
                        ProcessSymbol(character);
                    }
                    foundToken = true;
                }
            }
            
            return foundToken;
        }

        private void ProcessSymbol(char symbolStartCharacter)
        {
 	        var tokenNameBuilder = new TokenNameBuilder();
            tokenNameBuilder.Append(symbolStartCharacter);

            // check to see if this is a two-character symbol
            if (_scanner.Next())
            {
                tokenNameBuilder.Append(_scanner.Get());

                var twoCharSymbolTokenName = tokenNameBuilder.ToString();

                if (_grammar.ContainsKey(twoCharSymbolTokenName))
                {
                    //found a two-character symbol
                    var semantics = _grammar[twoCharSymbolTokenName];
                    _currentToken = new Token(twoCharSymbolTokenName, semantics);
                    return;
                }
                
                // This is not a recognized two-character symbol.
                // Re-treat the scanner and tokenNameBuilder.
                _scanner.Previous();
                tokenNameBuilder.Remove(tokenNameBuilder.Length - 1, 1);
            }

            //Treat potential symbol as one-character symbol
            var oneCharSymbolTokenName = tokenNameBuilder.GetTokenName();
            if (oneCharSymbolTokenName == _grammar.NegativeSymbol)
            {
                var lastTokenType = _lastToken.Semantics.Type;
                if (lastTokenType == TokenType.LeftParenthesis || 
                    lastTokenType == TokenType.Unknown ||
                    _lastToken.Semantics.IsOperator ||
                    _lastToken.Semantics.NestedFunctions > 0)
                {
                    oneCharSymbolTokenName = _grammar.UnaryMinusSymbol;
                }
            }

            if (_grammar.ContainsKey(oneCharSymbolTokenName))
            {
                var tokenMetaData = _grammar[oneCharSymbolTokenName];

                _currentToken = new Token(oneCharSymbolTokenName, tokenMetaData);
            }
            else
            {
                throw new InvalidOperationException(String.Format("Unrecognized character: {0}", symbolStartCharacter));
            }
        }

        private void ProcessNumber(char numberStartCharacter)
        {
            var tokenNameBuilder = new TokenNameBuilder();
            tokenNameBuilder.Append(numberStartCharacter);

            bool foundNumber = false;
            //begin to process numbers
            while (!foundNumber && _scanner.Next())
            {
                var numberCharacter = _scanner.Get();
                if (_grammar.NumberCharacters.Any( c => c == numberCharacter))
                {
                    tokenNameBuilder.Append(numberCharacter);
                }
                else
                {
                    _scanner.Previous();
                    foundNumber = true;
                }
            }
            _currentToken = new Token(tokenNameBuilder.GetTokenName(), new TokenSemantics(TokenType.Value, 0));
        }

        private void ProcessString(char character)
        {
            var tokenNameBuilder = new TokenNameBuilder();

            char nextChar;
            while (_scanner.Next() && (nextChar = _scanner.Get()) != character)
            {
                tokenNameBuilder.Append(nextChar);
            }
            _currentToken = new Token(tokenNameBuilder.GetTokenName(keepCase: true), new TokenSemantics(TokenType.Value, 0));
        }

        private void ProcessIdentifier(char identifierStartCharacter)
        {
            var tokenNameBuilder = new TokenNameBuilder();
            tokenNameBuilder.Append(identifierStartCharacter);

            bool foundIdentifier = false;
            //begin to process identifiers (keywords)
            while (!foundIdentifier && _scanner.Next())
            {
                var character = _scanner.Get();
                if (_grammar.IdentifierCharacters.Any( c => c == character))
                {
                    tokenNameBuilder.Append(character);
                }
                else
                {
                    _scanner.Previous();
                    foundIdentifier = true;
                }
            }

            var identifierName = tokenNameBuilder.GetTokenName();
            if (_grammar.ContainsKey(identifierName))
            {
                var tokenMetaData = _grammar[identifierName];

                _currentToken = new Token(identifierName, tokenMetaData);
            }
            else
            {
                _currentToken = new Token(identifierName, new TokenSemantics(TokenType.ExternalFunction, 0));
            }
        }

        public Token GetToken()
        {
            if (_currentToken.Semantics.Type == TokenType.Unknown)
            {
                throw new CompilerException("Unable to retrieve the next token.");
            }

            return _currentToken;
        }
    }
}
