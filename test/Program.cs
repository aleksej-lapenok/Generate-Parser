using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var lexer = new ExpLexer(new System.IO.StreamReader("test.txt"));
            var parse = new ExpParser(lexer);
            var result = parse.s().res.Value;
            Console.WriteLine(result);
            Console.ReadKey();
            
        }
    }
}
