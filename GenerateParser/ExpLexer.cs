using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


    using test;

class ExpLexer : BaseLexer
{
    Token[] tokens = new Token[]
    {
        new Token(0, "PLUS", false),
        new Token(1, "MUL", false),
        new Token(2, "NUM", false),
        new Token(3, "LB", false),
        new Token(4, "RB", false),
        new Token(5, "MINUS", false),
    };

    protected override Token[] Tokens => tokens;

    Regex[] values = new Regex[]
    {
        new Regex(@"\+"),
        new Regex(@"\*"),
        new Regex(@"[0-9]+"),
        new Regex(@"\("),
        new Regex(@"\)"),
        new Regex(@"-"),
    };

    protected override Regex[] Values => values;

    public ExpLexer(StreamReader fin) : base(fin) 
    {
    }

}
