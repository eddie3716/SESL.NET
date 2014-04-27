using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Compilation
{
    public class TokenNameBuilder
    {
        private StringBuilder _innerStringBuilder = new StringBuilder();

        public int Length { get { return _innerStringBuilder.Length; } }

        public string GetTokenName(bool keepCase = false)
        {
            return keepCase ? this.ToString() : this.ToString().ToLower();
        }

        public override string ToString()
        {
            return _innerStringBuilder.ToString();
        }

        public void Append(char c)
        {
            _innerStringBuilder.Append(c);
        }

        public void Remove(int startIndex, int length)
        {
            _innerStringBuilder.Remove(startIndex, length);
        }
    }
}
