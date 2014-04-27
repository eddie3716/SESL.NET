using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Compilation
{
    public interface ILexer
    {
        bool Next();

        Token GetToken();
    }
}
