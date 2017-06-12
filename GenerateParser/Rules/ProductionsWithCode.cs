using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.Rules
{
    public class ProductionsWithCode:Productions
    {
        public ProductionsWithCode() : base()
        {

        }

        public ProductionsWithCode(ProductionWithCode production) : base(production)
        {

        }

        public ProductionsWithCode(ProductionWithCode production, ProductionsWithCode tail): base(production,tail)
        {

        }
    }
}
