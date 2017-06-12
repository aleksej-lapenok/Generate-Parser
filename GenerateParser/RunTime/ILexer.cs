using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    interface ILexer
    {
        Token NextToken();
        Token GetToken();
    }
}
