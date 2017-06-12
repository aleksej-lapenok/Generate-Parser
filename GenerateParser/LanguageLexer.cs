using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


	using static task2.Utils.Translator;
    using task2.Utils;

class LanguageLexer : BaseLexer
{
    Token[] tokens = new Token[]
    {
        new Token(0, "NUM", false),
        new Token(1, "INT", false),
        new Token(2, "BOOL", false),
        new Token(3, "LET", false),
        new Token(4, "LETS", false),
        new Token(5, "IF", false),
        new Token(6, "ELSE", false),
        new Token(7, "TRUE", false),
        new Token(8, "FALSE", false),
        new Token(9, "WS", true),
        new Token(10, "LB", false),
        new Token(11, "RB", false),
        new Token(12, "COLON", false),
        new Token(13, "EQ", false),
        new Token(14, "SET", false),
        new Token(15, "LES", false),
        new Token(16, "LESE", false),
        new Token(17, "M", false),
        new Token(18, "MOREE", false),
        new Token(19, "AND", false),
        new Token(20, "NOT", false),
        new Token(21, "OR", false),
        new Token(22, "PLUS", false),
        new Token(23, "MINUS", false),
        new Token(24, "MUL", false),
        new Token(25, "DIV", false),
        new Token(26, "MOD", false),
        new Token(27, "OPEN", false),
        new Token(28, "CLOSE", false),
        new Token(29, "COMMA", false),
        new Token(30, "ELIF", false),
        new Token(31, "NAME", false),
    };

    protected override Token[] Tokens => tokens;

    Regex[] values = new Regex[]
    {
        new Regex(@"[0-9]"),
        new Regex(@"Int"),
        new Regex(@"Bool"),
        new Regex(@"Let"),
        new Regex(@"let"),
        new Regex(@"If"),
        new Regex(@"Else"),
        new Regex(@"true"),
        new Regex(@"false"),
        new Regex(@"[ \t\r\n]+"),
        new Regex(@"\("),
        new Regex(@"\)"),
        new Regex(@":"),
        new Regex(@"=="),
        new Regex(@"="),
        new Regex(@"<"),
        new Regex(@"<="),
        new Regex(@">"),
        new Regex(@">="),
        new Regex(@"&&"),
        new Regex(@"!"),
        new Regex(@"\|\|"),
        new Regex(@"\+"),
        new Regex(@"-"),
        new Regex(@"\*"),
        new Regex(@"/"),
        new Regex(@"%"),
        new Regex(@"{"),
        new Regex(@"}"),
        new Regex(@","),
        new Regex(@"ElIf"),
        new Regex(@"[a-z][a-zA-Z_0-9]*"),
    };

    protected override Regex[] Values => values;

    public LanguageLexer(StreamReader fin) : base(fin) 
    {
    }

}
