using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class Productions
    {
        List<Production> productions = new List<Production>();

        public Productions()
        {

        }

        public Productions(Production production)
        {
            productions.Add(production);
        }

        public Productions(Production pr, Productions tail):this(pr)
        {
            productions.AddRange(tail.productions);
        }

        public List<Production> Data
        {
            get => productions;
            protected set => productions = value;
        }
    }
}
