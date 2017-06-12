using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


	using static task2.Utils.Translator;
    using task2.Utils;


class LanguageParser : BaseParser
{
    public LanguageParser(LanguageLexer lexer) : base(lexer)
    {
    }

    public programNode program()
    {
        var token = lexer.GetToken();
        if (token.Id == 3)
        {
            var text = "";
            var arg0 = function();
            text += arg0.Text;
            var arg1 = program();
            text += arg1.Text;
            var result = new programNode(text, null, arg0, arg1);
            result.function = arg0;
            result.program = arg1;
            result.res=result.function.res+result.program.res;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 4)
        {
            var text = "";
            var arg0 = var();
            text += arg0.Text;
            var arg1 = program();
            text += arg1.Text;
            var result = new programNode(text, null, arg0, arg1);
            result.var = arg0;
            result.program = arg1;
            result.res=result.var.res+result.program.res;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new programNode(text, null);
            result.res=EmptyStr;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public functionNode function()
    {
        var token = lexer.GetToken();
        if (token.Id == 3)
        {
            var text = "";
            var arg0 = LET();
            text += arg0.Text;
            var arg1 = NAME();
            text += arg1.Text;
            var arg2 = LB();
            text += arg2.Text;
            var arg3 = args();
            text += arg3.Text;
            var arg4 = RB();
            text += arg4.Text;
            var arg5 = COLON();
            text += arg5.Text;
            var arg6 = type();
            text += arg6.Text;
            var arg7 = OPEN();
            text += arg7.Text;
            var arg8 = functionBody();
            text += arg8.Text;
            var arg9 = CLOSE();
            text += arg9.Text;
            var result = new functionNode(text, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            result.LET = arg0;
            result.NAME = arg1;
            result.LB = arg2;
            result.args = arg3;
            result.RB = arg4;
            result.COLON = arg5;
            result.type = arg6;
            result.OPEN = arg7;
            result.functionBody = arg8;
            result.CLOSE = arg9;
            result.res=Public+result.type.res+Space + result.NAME.Text+Translator.LB + result.args.res+Translator.RB+NewLine+Open+NewLine+result.functionBody.res+Close+NewLine;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 3 && token.Id != 4)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public functionBodyNode functionBody()
    {
        var token = lexer.GetToken();
        if (token.Id == 4)
        {
            var text = "";
            var arg0 = newVar();
            text += arg0.Text;
            var result = new functionBodyNode(text, null, arg0);
            result.newVar = arg0;
            result.res= result.newVar.res+NewLine;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 5)
        {
            var text = "";
            var arg0 = iff();
            text += arg0.Text;
            var result = new functionBodyNode(text, null, arg0);
            result.iff = arg0;
            result.res=result.iff.res+NewLine;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 0 || token.Id == 31 || token.Id == 17 || token.Id == 18 || token.Id == 19 || token.Id == 20 || token.Id == 21 || token.Id == 22 || token.Id == 23 || token.Id == 24 || token.Id == 25 || token.Id == 26 || token.Id == 7 || token.Id == 8 || token.Id == 10 || token.Id == 13 || token.Id == 29 || token.Id == 15)
        {
            var text = "";
            var arg0 = expr();
            text += arg0.Text;
            var result = new functionBodyNode(text, null, arg0);
            result.expr = arg0;
            result.res=Return+result.expr.res+End+NewLine;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public newVarNode newVar()
    {
        var token = lexer.GetToken();
        if (token.Id == 4)
        {
            var text = "";
            var arg0 = var();
            text += arg0.Text;
            var arg1 = functionBody();
            text += arg1.Text;
            var result = new newVarNode(text, null, arg0, arg1);
            result.var = arg0;
            result.functionBody = arg1;
            result.res =result.var.res+NewLine+result.functionBody.res; 
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public varNode var()
    {
        var token = lexer.GetToken();
        if (token.Id == 4)
        {
            var text = "";
            var arg0 = LETS();
            text += arg0.Text;
            var arg1 = NAME();
            text += arg1.Text;
            var arg2 = COLON();
            text += arg2.Text;
            var arg3 = type();
            text += arg3.Text;
            var arg4 = SET();
            text += arg4.Text;
            var arg5 = expr();
            text += arg5.Text;
            var result = new varNode(text, null, arg0, arg1, arg2, arg3, arg4, arg5);
            result.LETS = arg0;
            result.NAME = arg1;
            result.COLON = arg2;
            result.type = arg3;
            result.SET = arg4;
            result.expr = arg5;
            result.res=result.type.res+Space+result.NAME.Text+Is+result.expr.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public iffNode iff()
    {
        var token = lexer.GetToken();
        if (token.Id == 5)
        {
            var text = "";
            var arg0 = IF();
            text += arg0.Text;
            var arg1 = LSB();
            text += arg1.Text;
            var arg2 = expr();
            text += arg2.Text;
            var arg3 = RSB();
            text += arg3.Text;
            var arg4 = OPEN();
            text += arg4.Text;
            var arg5 = functionBody();
            text += arg5.Text;
            var arg6 = CLOSE();
            text += arg6.Text;
            var arg7 = if2();
            text += arg7.Text;
            var result = new iffNode(text, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            result.IF = arg0;
            result.LSB = arg1;
            result.expr = arg2;
            result.RSB = arg3;
            result.OPEN = arg4;
            result.functionBody = arg5;
            result.CLOSE = arg6;
            result.if2 = arg7;
            result.res=If+result.expr.res+Translator.RB+NewLine+Open+NewLine+result.functionBody.res+Close+NewLine+ result.if2.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public if2Node if2()
    {
        var token = lexer.GetToken();
        if (token.Id == 6)
        {
            var text = "";
            var arg0 = elsee();
            text += arg0.Text;
            var result = new if2Node(text, null, arg0);
            result.elsee = arg0;
            result.res = result.elsee.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 30)
        {
            var text = "";
            var arg0 = elif2();
            text += arg0.Text;
            var result = new if2Node(text, null, arg0);
            result.elif2 = arg0;
            result.res=result.elif2.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public elseeNode elsee()
    {
        var token = lexer.GetToken();
        if (token.Id == 6)
        {
            var text = "";
            var arg0 = ELSE();
            text += arg0.Text;
            var arg1 = OPEN();
            text += arg1.Text;
            var arg2 = functionBody();
            text += arg2.Text;
            var arg3 = CLOSE();
            text += arg3.Text;
            var result = new elseeNode(text, null, arg0, arg1, arg2, arg3);
            result.ELSE = arg0;
            result.OPEN = arg1;
            result.functionBody = arg2;
            result.CLOSE = arg3;
            result.res=Else+NewLine+Open+NewLine+result.functionBody.res+Close+NewLine;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public elif2Node elif2()
    {
        var token = lexer.GetToken();
        if (token.Id == 30)
        {
            var text = "";
            var arg0 = ELIF();
            text += arg0.Text;
            var arg1 = LSB();
            text += arg1.Text;
            var arg2 = expr();
            text += arg2.Text;
            var arg3 = RSB();
            text += arg3.Text;
            var arg4 = OPEN();
            text += arg4.Text;
            var arg5 = functionBody();
            text += arg5.Text;
            var arg6 = CLOSE();
            text += arg6.Text;
            var arg7 = if2();
            text += arg7.Text;
            var result = new elif2Node(text, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            result.ELIF = arg0;
            result.LSB = arg1;
            result.expr = arg2;
            result.RSB = arg3;
            result.OPEN = arg4;
            result.functionBody = arg5;
            result.CLOSE = arg6;
            result.if2 = arg7;
            result.res = Else + Space + If+result.expr.res+Translator.RB+NewLine+Open+NewLine + result.functionBody.res+Close+NewLine+result.if2.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public exprNode expr()
    {
        var token = lexer.GetToken();
        if (token.Id == 0 || token.Id == 31 || token.Id == 17 || token.Id == 18 || token.Id == 19 || token.Id == 20 || token.Id == 21 || token.Id == 22 || token.Id == 23 || token.Id == 24 || token.Id == 25 || token.Id == 26 || token.Id == 7 || token.Id == 8 || token.Id == 10 || token.Id == 13 || token.Id == 29 || token.Id == 15)
        {
            var text = "";
            var arg0 = str2();
            text += arg0.Text;
            var result = new exprNode(text, null, arg0);
            result.str2 = arg0;
            result.res=result.str2.Text;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public argumentsNode arguments()
    {
        var token = lexer.GetToken();
        if (token.Id == 0 || token.Id == 31 || token.Id == 17 || token.Id == 18 || token.Id == 19 || token.Id == 20 || token.Id == 21 || token.Id == 22 || token.Id == 23 || token.Id == 24 || token.Id == 25 || token.Id == 26 || token.Id == 7 || token.Id == 8 || token.Id == 10 || token.Id == 13 || token.Id == 29 || token.Id == 15)
        {
            var text = "";
            var arg0 = expr();
            text += arg0.Text;
            var arg1 = arguments2();
            text += arg1.Text;
            var result = new argumentsNode(text, null, arg0, arg1);
            result.expr = arg0;
            result.arguments2 = arg1;
            result.res=result.expr.res+result.arguments2.res;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public arguments2Node arguments2()
    {
        var token = lexer.GetToken();
        if (token.Id == 29)
        {
            var text = "";
            var arg0 = COMMA();
            text += arg0.Text;
            var arg1 = expr();
            text += arg1.Text;
            var arg2 = arguments2();
            text += arg2.Text;
            var result = new arguments2Node(text, null, arg0, arg1, arg2);
            result.COMMA = arg0;
            result.expr = arg1;
            result.arguments2 = arg2;
            result.res=Comma+result.expr.res+result.arguments2.res;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new arguments2Node(text, null);
            result.res=EmptyStr;
            token = lexer.GetToken();
            if (token.Id != -1)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public argsNode args()
    {
        var token = lexer.GetToken();
        if (token.Id == 31)
        {
            var text = "";
            var arg0 = arg();
            text += arg0.Text;
            var arg1 = argg2();
            text += arg1.Text;
            var result = new argsNode(text, null, arg0, arg1);
            result.arg = arg0;
            result.argg2 = arg1;
            result.res=result.arg.res+result.argg2.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new argsNode(text, null);
            result.res=EmptyStr;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public argg2Node argg2()
    {
        var token = lexer.GetToken();
        if (token.Id == 29)
        {
            var text = "";
            var arg0 = COMMA();
            text += arg0.Text;
            var arg1 = arg();
            text += arg1.Text;
            var arg2 = argg2();
            text += arg2.Text;
            var result = new argg2Node(text, null, arg0, arg1, arg2);
            result.COMMA = arg0;
            result.arg = arg1;
            result.argg2 = arg2;
            result.res=Comma+result.arg.res+result.argg2.res;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new argg2Node(text, null);
            result.res=EmptyStr;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public argNode arg()
    {
        var token = lexer.GetToken();
        if (token.Id == 31)
        {
            var text = "";
            var arg0 = NAME();
            text += arg0.Text;
            var arg1 = COLON();
            text += arg1.Text;
            var arg2 = type();
            text += arg2.Text;
            var result = new argNode(text, null, arg0, arg1, arg2);
            result.NAME = arg0;
            result.COLON = arg1;
            result.type = arg2;
            result.res=result.type.res+Space+result.NAME.Text;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 29 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public typeNode type()
    {
        var token = lexer.GetToken();
        if (token.Id == 1)
        {
            var text = "";
            var arg0 = INT();
            text += arg0.Text;
            var result = new typeNode(text, null, arg0);
            result.INT = arg0;
            result.res=Int;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 27 && token.Id != 16 && token.Id != 29 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 2)
        {
            var text = "";
            var arg0 = BOOL();
            text += arg0.Text;
            var result = new typeNode(text, null, arg0);
            result.BOOL = arg0;
            result.res=Bool;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 27 && token.Id != 16 && token.Id != 29 && token.Id != 13)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public str2Node str2()
    {
        var token = lexer.GetToken();
        if (token.Id == 0)
        {
            var text = "";
            var arg0 = NUM();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.NUM = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 31)
        {
            var text = "";
            var arg0 = NAME();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.NAME = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 15)
        {
            var text = "";
            var arg0 = eq();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.eq = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 17)
        {
            var text = "";
            var arg0 = LES();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.LES = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 18)
        {
            var text = "";
            var arg0 = M();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.M = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 19)
        {
            var text = "";
            var arg0 = AND();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.AND = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 20)
        {
            var text = "";
            var arg0 = NOT();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.NOT = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 21)
        {
            var text = "";
            var arg0 = OR();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.OR = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 22)
        {
            var text = "";
            var arg0 = PLUS();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.PLUS = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 23)
        {
            var text = "";
            var arg0 = MINUS();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.MINUS = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 24)
        {
            var text = "";
            var arg0 = MUL();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.MUL = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 25)
        {
            var text = "";
            var arg0 = DIV();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.DIV = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 26)
        {
            var text = "";
            var arg0 = MOD();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.MOD = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 7)
        {
            var text = "";
            var arg0 = TRUE();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.TRUE = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 8)
        {
            var text = "";
            var arg0 = FALSE();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.FALSE = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 10)
        {
            var text = "";
            var arg0 = LB();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.LB = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 13)
        {
            var text = "";
            var arg0 = RB();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.RB = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (token.Id == 29)
        {
            var text = "";
            var arg0 = COMMA();
            text += arg0.Text;
            var arg1 = str();
            text += arg1.Text;
            var result = new str2Node(text, arg0, arg1);
            result.COMMA = arg0;
            result.str = arg1;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public strNode str()
    {
        var token = lexer.GetToken();
        if (token.Id == 0 || token.Id == 31 || token.Id == 17 || token.Id == 18 || token.Id == 19 || token.Id == 20 || token.Id == 21 || token.Id == 22 || token.Id == 23 || token.Id == 24 || token.Id == 25 || token.Id == 26 || token.Id == 7 || token.Id == 8 || token.Id == 10 || token.Id == 13 || token.Id == 29 || token.Id == 15)
        {
            var text = "";
            var arg0 = str2();
            text += arg0.Text;
            var result = new strNode(text, arg0);
            result.str2 = arg0;
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        if (true)
        {
            var text = "";
            var result = new strNode(text);
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public eqNode eq()
    {
        var token = lexer.GetToken();
        if (token.Id == 15)
        {
            var text = "";
            var arg0 = EQ();
            text += arg0.Text;
            var result = new eqNode(text, null, arg0);
            result.EQ = arg0;
            result.Text=" == ";
            token = lexer.GetToken();
            if (token.Id != -1 && token.Id != 0 && token.Id != 31 && token.Id != 17 && token.Id != 18 && token.Id != 19 && token.Id != 20 && token.Id != 21 && token.Id != 22 && token.Id != 23 && token.Id != 24 && token.Id != 25 && token.Id != 26 && token.Id != 7 && token.Id != 8 && token.Id != 10 && token.Id != 13 && token.Id != 29 && token.Id != 15 && token.Id != 28 && token.Id != 3 && token.Id != 4 && token.Id != 5 && token.Id != 12)
                throw new ParserException("Got unxpected token from lexer");
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
        //return null
    }

    public NUMNode NUM()
    {
        if (lexer.GetToken().Id == 0)
        {
            var result = new NUMNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public INTNode INT()
    {
        if (lexer.GetToken().Id == 1)
        {
            var result = new INTNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public BOOLNode BOOL()
    {
        if (lexer.GetToken().Id == 2)
        {
            var result = new BOOLNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LETNode LET()
    {
        if (lexer.GetToken().Id == 3)
        {
            var result = new LETNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LETSNode LETS()
    {
        if (lexer.GetToken().Id == 4)
        {
            var result = new LETSNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public IFNode IF()
    {
        if (lexer.GetToken().Id == 5)
        {
            var result = new IFNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ELSENode ELSE()
    {
        if (lexer.GetToken().Id == 6)
        {
            var result = new ELSENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public TRUENode TRUE()
    {
        if (lexer.GetToken().Id == 7)
        {
            var result = new TRUENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public FALSENode FALSE()
    {
        if (lexer.GetToken().Id == 8)
        {
            var result = new FALSENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LBNode LB()
    {
        if (lexer.GetToken().Id == 10)
        {
            var result = new LBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LSBNode LSB()
    {
        if (lexer.GetToken().Id == 11)
        {
            var result = new LSBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public RSBNode RSB()
    {
        if (lexer.GetToken().Id == 12)
        {
            var result = new RSBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public RBNode RB()
    {
        if (lexer.GetToken().Id == 13)
        {
            var result = new RBNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public COLONNode COLON()
    {
        if (lexer.GetToken().Id == 14)
        {
            var result = new COLONNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public EQNode EQ()
    {
        if (lexer.GetToken().Id == 15)
        {
            var result = new EQNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public SETNode SET()
    {
        if (lexer.GetToken().Id == 16)
        {
            var result = new SETNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public LESNode LES()
    {
        if (lexer.GetToken().Id == 17)
        {
            var result = new LESNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MNode M()
    {
        if (lexer.GetToken().Id == 18)
        {
            var result = new MNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ANDNode AND()
    {
        if (lexer.GetToken().Id == 19)
        {
            var result = new ANDNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public NOTNode NOT()
    {
        if (lexer.GetToken().Id == 20)
        {
            var result = new NOTNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ORNode OR()
    {
        if (lexer.GetToken().Id == 21)
        {
            var result = new ORNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public PLUSNode PLUS()
    {
        if (lexer.GetToken().Id == 22)
        {
            var result = new PLUSNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MINUSNode MINUS()
    {
        if (lexer.GetToken().Id == 23)
        {
            var result = new MINUSNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MULNode MUL()
    {
        if (lexer.GetToken().Id == 24)
        {
            var result = new MULNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public DIVNode DIV()
    {
        if (lexer.GetToken().Id == 25)
        {
            var result = new DIVNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public MODNode MOD()
    {
        if (lexer.GetToken().Id == 26)
        {
            var result = new MODNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public OPENNode OPEN()
    {
        if (lexer.GetToken().Id == 27)
        {
            var result = new OPENNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public CLOSENode CLOSE()
    {
        if (lexer.GetToken().Id == 28)
        {
            var result = new CLOSENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public COMMANode COMMA()
    {
        if (lexer.GetToken().Id == 29)
        {
            var result = new COMMANode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public ELIFNode ELIF()
    {
        if (lexer.GetToken().Id == 30)
        {
            var result = new ELIFNode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }

    public NAMENode NAME()
    {
        if (lexer.GetToken().Id == 31)
        {
            var result = new NAMENode(lexer.GetText());
            lexer.NextToken();
            return result;
        }
        throw new ParserException("Got unxpected token from lexer");
    }
}
