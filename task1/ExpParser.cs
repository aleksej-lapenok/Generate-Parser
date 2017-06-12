using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;



class ExpParser : BaseParser
{
    public ExpParser(ExpLexer lexer) : base(lexer)
    {
    }

    public expNode exp()
    {
        var token = lexer.GetToken();
        if (token.Id == 5 || token.Id == 0 || token.Id == 6)
        {
            var text = "";
            var arg0 = or();
            text += arg0.Text;
            var result = new expNode(text, arg0);
            result.or = arg0;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public orNode or()
    {
        var token = lexer.GetToken();
        if (token.Id == 5 || token.Id == 0 || token.Id == 6)
        {
            var text = "";
            var arg0 = xor();
            text += arg0.Text;
            var arg1 = or2();
            text += arg1.Text;
            var result = new orNode(text, arg0, arg1);
            result.xor = arg0;
            result.or2 = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public or2Node or2()
    {
        var token = lexer.GetToken();
        if (token.Id == 2)
        {
            var text = "";
            var arg0 = OR();
            text += arg0.Text;
            var arg1 = or();
            text += arg1.Text;
            var result = new or2Node(text, arg0, arg1);
            result.OR = arg0;
            result.or = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new or2Node(text);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public xorNode xor()
    {
        var token = lexer.GetToken();
        if (token.Id == 5 || token.Id == 0 || token.Id == 6)
        {
            var text = "";
            var arg0 = and();
            text += arg0.Text;
            var arg1 = xor2();
            text += arg1.Text;
            var result = new xorNode(text, arg0, arg1);
            result.and = arg0;
            result.xor2 = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public xor2Node xor2()
    {
        var token = lexer.GetToken();
        if (token.Id == 3)
        {
            var text = "";
            var arg0 = XOR();
            text += arg0.Text;
            var arg1 = xor();
            text += arg1.Text;
            var result = new xor2Node(text, arg0, arg1);
            result.XOR = arg0;
            result.xor = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new xor2Node(text);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public andNode and()
    {
        var token = lexer.GetToken();
        if (token.Id == 5 || token.Id == 0 || token.Id == 6)
        {
            var text = "";
            var arg0 = negate();
            text += arg0.Text;
            var arg1 = and2();
            text += arg1.Text;
            var result = new andNode(text, arg0, arg1);
            result.negate = arg0;
            result.and2 = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public and2Node and2()
    {
        var token = lexer.GetToken();
        if (token.Id == 4)
        {
            var text = "";
            var arg0 = AND();
            text += arg0.Text;
            var arg1 = and();
            text += arg1.Text;
            var result = new and2Node(text, arg0, arg1);
            result.AND = arg0;
            result.and = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new and2Node(text);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public negateNode negate()
    {
        var token = lexer.GetToken();
        if (token.Id == 5)
        {
            var text = "";
            var arg0 = NEGATE();
            text += arg0.Text;
            var arg1 = var();
            text += arg1.Text;
            var result = new negateNode(text, arg0, arg1);
            result.NEGATE = arg0;
            result.var = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 0 || token.Id == 6)
        {
            var text = "";
            var arg0 = var();
            text += arg0.Text;
            var result = new negateNode(text, arg0);
            result.var = arg0;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public varNode var()
    {
        var token = lexer.GetToken();
        if (token.Id == 0)
        {
            var text = "";
            var arg0 = L_BRACKET();
            text += arg0.Text;
            var arg1 = or();
            text += arg1.Text;
            var arg2 = R_BRACKET();
            text += arg2.Text;
            var result = new varNode(text, arg0, arg1, arg2);
            result.L_BRACKET = arg0;
            result.or = arg1;
            result.R_BRACKET = arg2;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 6)
        {
            var text = "";
            var arg0 = VAR();
            text += arg0.Text;
            var result = new varNode(text, arg0);
            result.VAR = arg0;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 4 && token.Id != 3 && token.Id != 2 && token.Id != 1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public L_BRACKETNode L_BRACKET()
    {
        if (lexer.GetToken().Id == 0)
        {
            var result = new L_BRACKETNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public R_BRACKETNode R_BRACKET()
    {
        if (lexer.GetToken().Id == 1)
        {
            var result = new R_BRACKETNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ORNode OR()
    {
        if (lexer.GetToken().Id == 2)
        {
            var result = new ORNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public XORNode XOR()
    {
        if (lexer.GetToken().Id == 3)
        {
            var result = new XORNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ANDNode AND()
    {
        if (lexer.GetToken().Id == 4)
        {
            var result = new ANDNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public NEGATENode NEGATE()
    {
        if (lexer.GetToken().Id == 5)
        {
            var result = new NEGATENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public VARNode VAR()
    {
        if (lexer.GetToken().Id == 6)
        {
            var result = new VARNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }
}
