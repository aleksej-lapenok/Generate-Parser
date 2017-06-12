using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


class ExpLexer : BaseLexer
{
    Token[] tokens = new Token[]
    {
        new Token(0, "L_BRACKET", false),
        new Token(1, "R_BRACKET", false),
        new Token(2, "OR", false),
        new Token(3, "XOR", false),
        new Token(4, "AND", false),
        new Token(5, "NEGATE", false),
        new Token(6, "VAR", false),
        new Token(7, "WS", true),
    };

    protected override Token[] Tokens => tokens;

    Regex[] values = new Regex[]
    {
        new Regex(@"\("),
        new Regex(@"\)"),
        new Regex(@"\|"),
        new Regex(@"\^"),
        new Regex(@"&"),
        new Regex(@"!"),
        new Regex(@"[a-z]"),
        new Regex(@"[ \t]+"),
    };

    protected override Regex[] Values => values;

    public ExpLexer(StreamReader fin) : base(fin) 
    {
    }

}
