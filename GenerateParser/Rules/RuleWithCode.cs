using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    class RuleWithCode : Rule
    {
        private string arg;
        public RuleWithCode(string name, string arg, ProductionsWithCode pr) : base(name, pr)
        {
            arg = arg.Substring(1);
            Arg = arg.Substring(0, arg.Length - 1);
        }

        public string Arg
        {
            get => arg;
            private set => arg = value;
        }
    }
}
