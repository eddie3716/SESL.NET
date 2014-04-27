using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET.Compilation
{
    public interface IScanner<T, S>
    {
        bool Next();

        bool Previous();

        T Get();

        int CurrentIndex { get; }

        S Source { get; }
    }
}
