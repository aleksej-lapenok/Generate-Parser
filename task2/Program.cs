using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

using static task2.Utils.Translator;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;
            using (StreamReader fin = new StreamReader(args[0]))
            {
                var lexer = new LanguageLexer(fin);
                var parser = new LanguageParser(lexer);
                var program = parser.program();
                using (StreamWriter fout = new StreamWriter(args[1]))
                {
                    fout.WriteLine("public class Program2 \r\n {");
                    fout.WriteLine(program.res);
                    fout.WriteLine("}");
                }
            }
            Console.WriteLine(new Program2().isPrime(239));
            Console.ReadLine();
        }
    }
}
