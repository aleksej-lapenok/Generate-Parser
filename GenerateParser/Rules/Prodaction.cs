using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class Production
    {
        List<(string name,string nameVar)> productions = new List<(string name, string nameVar)>();

        public Production()
        {
            ;
        }

        public Production(string name, string nameVar)
        {
            productions.Add((name: name, nameVar: nameVar != null && nameVar != "" ? nameVar : name));
        }

        public Production(string name): this(name,name)
        {

        }

        public Production(string name, string nameVar, Production tall):this(name, nameVar)
        {
            productions.AddRange(tall.productions);
        }

        public Production(string name, Production tail) : this(name, name, tail)
        {

        }

        public List<(string name,string nameVar)> Productions
        {
            get => productions;
            protected set => productions = value;
        }
    }
}
