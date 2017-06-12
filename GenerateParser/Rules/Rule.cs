using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class Rule
    {
        string name;
        Productions productions;

        public Rule(string name, Productions pr)
        {
            this.name = name;
            productions = pr;
        }

        public string Name
        {
            get => name;
            protected set => name = value;
        }

        public Productions Productions
        {
            get => productions;
            protected set => productions = value;
        }
    }
}
