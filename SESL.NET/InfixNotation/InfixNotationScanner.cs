using System;
using SESL.NET.Compilation;

namespace SESL.NET.InfixNotation
{
    public class InfixNotationScanner: IScanner<char, string>
    {
        private readonly string _sourceText;
        private readonly int _sourceTextLength;
        private int _index = -1;

        private InfixNotationScanner() { }

        public InfixNotationScanner(string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
            {
                throw new InvalidOperationException("SourceText is invalid.");
            }
            _sourceText = sourceText;
            _sourceTextLength = sourceText.Length;
        }

        public bool Next()
        {
            bool result = false;
            if (_sourceTextLength != _index + 1)
            {
                result = true;
                ++_index;
            }
            return result;
        }

        public bool Previous()
        {
            bool result = false;
            if (-1 != _index)
            {
                result = true;
                --_index;
            }
            return result;
        }

        public char Get()
        {
            if (_index == -1)
            {
                ++_index;
            }
            return _sourceText[_index];
        }

        public int CurrentIndex => _index;


        public string Source => _sourceText;

    }
}
