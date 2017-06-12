using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.test
{
    public class Structure<T>
    {
        public T val;
        public Structure(T val)
        {
            this.val = val;
        }
    }

    public class Lambda
    {
        public string str;
        public Lambda(string str)
        {
            this.str = str;
        }
    }

    public class LambdaStructure
    {
        public static Structure<Lambda> let(string var,Structure<Lambda> arg1, Structure<Lambda> arg2)
        {
            return new Structure<Lambda>(new Lambda($"(let {var} = {arg1.val.str} in {arg2.val.str})"));
        }

        public static Structure<Lambda> abstraction(string var, Structure<Lambda> arg1)
        {
            return new Structure<Lambda>(new Lambda($"(\\{var}.{arg1.val.str})"));
        }

        public static Structure<Lambda> variable(string var)
        {
            return new Structure<Lambda>(new Lambda(var));
        }

        public static Structure<Lambda> application(Structure<Lambda> arg1, Structure<Lambda> arg2)
        {
            return new Structure<Lambda>(new Lambda("("+arg1.val.str + " " + arg2.val.str+")"));
        }
    }
}
