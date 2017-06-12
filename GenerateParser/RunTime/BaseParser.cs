using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    public class BaseParser
    {
        protected BaseLexer lexer;

        public BaseParser(BaseLexer lexer)
        {
            this.lexer = lexer;
        }
    }
}
