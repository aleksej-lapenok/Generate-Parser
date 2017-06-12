using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class LexerRule : Rule
    {
        bool skip;
        string exp;
        public LexerRule(string name, string val,bool skip) : base(name, new Productions())
        {
            this.skip = skip;
            exp = val.Substring(1,val.Length-2);
        }

        public bool Skip
        {
            get => skip;
            protected set => skip = value;
        }

        public string Exp
        {
            get => exp;
            protected set => exp = value;
        }
    }
}
