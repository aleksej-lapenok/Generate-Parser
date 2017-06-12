using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    public class ParserException : Exception
    {
        public ParserException(string message):base(message)
        {

        }

        public ParserException(string message,int pos):base(message+" at " + pos)
        {

        }
    }
}
