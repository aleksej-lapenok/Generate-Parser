using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class GrammarRule : Rule
    {
        public GrammarRule(string name, Productions pr) : base(name, pr)
        {
        }
    }
}
