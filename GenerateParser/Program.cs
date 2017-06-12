using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GenerateParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length!=1)
            {
                Console.WriteLine("For run use arguments: <input file>");
                return;
            }
            if (true)
                Generate(args[0]);
            else
                Test();
        }

        static void Generate(string name)
        {
            using (StreamReader fin = new StreamReader(name))
            {
                new Generator(fin.ReadToEnd()).Generate();
            }
        }

        static void Test()
        {/*
            LambdaLexer lexer;
            using (StreamReader fin = new StreamReader("test.txt"))
            {
                lexer = new LambdaLexer(fin);
                var parser = new LambdaParser(lexer);
                var node = parser.let_expression();
                Console.WriteLine(node.Text);
                Console.WriteLine(node.ret.val.str);
                Console.ReadLine();
            }*/
        }
    }
}
