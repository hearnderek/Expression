using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokens;

namespace TreesVisitor.Parsing
{
    public static class EquationParser
    {
        public static AbstractToken[] Tokenize(string s) => Tokenizer.Tokenize(s);

    }
}
