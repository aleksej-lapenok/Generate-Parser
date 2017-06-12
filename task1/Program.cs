using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using GenerateParser.RunTime;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length==0)
            {
                Console.WriteLine("For run use: parsers <input file>");
                return;
            }
            expNode root;
            using (StreamReader fin = new StreamReader(args[0]))
            {
                var lexer = new ExpLexer(fin);
                var parser = new ExpParser(lexer);
                root = parser.exp();
            }
            Application.EnableVisualStyles();
            Application.Run(new Form1(root));

        }
    }
}
