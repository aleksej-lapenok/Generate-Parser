using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class ProductionWithCode : Production
    {

        private string code;

        public ProductionWithCode():base()
        {

        }

        public ProductionWithCode(string name, string nameVar) : base(name, nameVar)
        {

        }

        public ProductionWithCode(string name, string nameVar, string code) : this(name, nameVar)
        {
            code = code.Substring(1, code.Length - 2);
            Code = code;
        }

        public ProductionWithCode(string code) : this()
        {
            code = code.Substring(1, code.Length - 2);
            Code = code;
        }

        public ProductionWithCode(string name, string nameVar, Production tall) : base(name, nameVar, tall)
        {
        }

        public ProductionWithCode(string name, string nameVar, Production tail, string code) : this(name,nameVar,tail)
        {
            code = code.Substring(1, code.Length - 2);
            Code = code;
        }

        public ProductionWithCode(string name, Production tail) : base(name, name, tail)
        { }

        public string Code
        {
            get => code;
            protected set => code = value;
        }
    }
}
