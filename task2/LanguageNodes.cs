using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


	using static task2.Utils.Translator;
    using task2.Utils;


public class programNode : Node
{

    public functionNode function;

    public programNode program;

    public varNode var;

    public string res;

    public programNode(string text, string res, params Node[] children) : base("program", text, children)
    {
        this.res = res;
    }

}

public class functionNode : Node
{

    public LETNode LET;

    public NAMENode NAME;

    public LBNode LB;

    public argsNode args;

    public RBNode RB;

    public COLONNode COLON;

    public typeNode type;

    public OPENNode OPEN;

    public functionBodyNode functionBody;

    public CLOSENode CLOSE;

    public string res;

    public functionNode(string text, string res, params Node[] children) : base("function", text, children)
    {
        this.res = res;
    }

}

public class functionBodyNode : Node
{

    public newVarNode newVar;

    public iffNode iff;

    public exprNode expr;

    public string res;

    public functionBodyNode(string text, string res, params Node[] children) : base("functionBody", text, children)
    {
        this.res = res;
    }

}

public class newVarNode : Node
{

    public varNode var;

    public functionBodyNode functionBody;

    public string res;

    public newVarNode(string text, string res, params Node[] children) : base("newVar", text, children)
    {
        this.res = res;
    }

}

public class varNode : Node
{

    public LETSNode LETS;

    public NAMENode NAME;

    public COLONNode COLON;

    public typeNode type;

    public SETNode SET;

    public exprNode expr;

    public string res;

    public varNode(string text, string res, params Node[] children) : base("var", text, children)
    {
        this.res = res;
    }

}

public class iffNode : Node
{

    public IFNode IF;

    public LSBNode LSB;

    public exprNode expr;

    public RSBNode RSB;

    public OPENNode OPEN;

    public functionBodyNode functionBody;

    public CLOSENode CLOSE;

    public if2Node if2;

    public string res;

    public iffNode(string text, string res, params Node[] children) : base("iff", text, children)
    {
        this.res = res;
    }

}

public class if2Node : Node
{

    public elseeNode elsee;

    public elif2Node elif2;

    public string res;

    public if2Node(string text, string res, params Node[] children) : base("if2", text, children)
    {
        this.res = res;
    }

}

public class elseeNode : Node
{

    public ELSENode ELSE;

    public OPENNode OPEN;

    public functionBodyNode functionBody;

    public CLOSENode CLOSE;

    public string res;

    public elseeNode(string text, string res, params Node[] children) : base("elsee", text, children)
    {
        this.res = res;
    }

}

public class elif2Node : Node
{

    public ELIFNode ELIF;

    public LSBNode LSB;

    public exprNode expr;

    public RSBNode RSB;

    public OPENNode OPEN;

    public functionBodyNode functionBody;

    public CLOSENode CLOSE;

    public if2Node if2;

    public string res;

    public elif2Node(string text, string res, params Node[] children) : base("elif2", text, children)
    {
        this.res = res;
    }

}

public class exprNode : Node
{

    public str2Node str2;

    public string res;

    public exprNode(string text, string res, params Node[] children) : base("expr", text, children)
    {
        this.res = res;
    }

}

public class argumentsNode : Node
{

    public exprNode expr;

    public arguments2Node arguments2;

    public string res;

    public argumentsNode(string text, string res, params Node[] children) : base("arguments", text, children)
    {
        this.res = res;
    }

}

public class arguments2Node : Node
{

    public COMMANode COMMA;

    public exprNode expr;

    public arguments2Node arguments2;

    public string res;

    public arguments2Node(string text, string res, params Node[] children) : base("arguments2", text, children)
    {
        this.res = res;
    }

}

public class argsNode : Node
{

    public argNode arg;

    public argg2Node argg2;

    public string res;

    public argsNode(string text, string res, params Node[] children) : base("args", text, children)
    {
        this.res = res;
    }

}

public class argg2Node : Node
{

    public COMMANode COMMA;

    public argNode arg;

    public argg2Node argg2;

    public string res;

    public argg2Node(string text, string res, params Node[] children) : base("argg2", text, children)
    {
        this.res = res;
    }

}

public class argNode : Node
{

    public NAMENode NAME;

    public COLONNode COLON;

    public typeNode type;

    public string res;

    public argNode(string text, string res, params Node[] children) : base("arg", text, children)
    {
        this.res = res;
    }

}

public class typeNode : Node
{

    public INTNode INT;

    public BOOLNode BOOL;

    public string res;

    public typeNode(string text, string res, params Node[] children) : base("type", text, children)
    {
        this.res = res;
    }

}

public class str2Node : Node
{

    public NUMNode NUM;

    public strNode str;

    public NAMENode NAME;

    public eqNode eq;

    public LESNode LES;

    public MNode M;

    public ANDNode AND;

    public NOTNode NOT;

    public ORNode OR;

    public PLUSNode PLUS;

    public MINUSNode MINUS;

    public MULNode MUL;

    public DIVNode DIV;

    public MODNode MOD;

    public TRUENode TRUE;

    public FALSENode FALSE;

    public LBNode LB;

    public RBNode RB;

    public COMMANode COMMA;

    public str2Node(string text, params Node[] children) : base("str2", text, children)
    {
    }

}

public class strNode : Node
{

    public str2Node str2;

    public strNode(string text, params Node[] children) : base("str", text, children)
    {
    }

}

public class eqNode : Node
{

    public EQNode EQ;

    public string res;

    public eqNode(string text, string res, params Node[] children) : base("eq", text, children)
    {
        this.res = res;
    }

}

public class NUMNode : Node
{

    public NUMNode(string text, params Node[] children) : base("NUM", text, children)
    {
    }

}

public class INTNode : Node
{

    public INTNode(string text, params Node[] children) : base("INT", text, children)
    {
    }

}

public class BOOLNode : Node
{

    public BOOLNode(string text, params Node[] children) : base("BOOL", text, children)
    {
    }

}

public class LETNode : Node
{

    public LETNode(string text, params Node[] children) : base("LET", text, children)
    {
    }

}

public class LETSNode : Node
{

    public LETSNode(string text, params Node[] children) : base("LETS", text, children)
    {
    }

}

public class IFNode : Node
{

    public IFNode(string text, params Node[] children) : base("IF", text, children)
    {
    }

}

public class ELSENode : Node
{

    public ELSENode(string text, params Node[] children) : base("ELSE", text, children)
    {
    }

}

public class TRUENode : Node
{

    public TRUENode(string text, params Node[] children) : base("TRUE", text, children)
    {
    }

}

public class FALSENode : Node
{

    public FALSENode(string text, params Node[] children) : base("FALSE", text, children)
    {
    }

}

public class LBNode : Node
{

    public LBNode(string text, params Node[] children) : base("LB", text, children)
    {
    }

}

public class LSBNode : Node
{

    public LSBNode(string text, params Node[] children) : base("LSB", text, children)
    {
    }

}

public class RSBNode : Node
{

    public RSBNode(string text, params Node[] children) : base("RSB", text, children)
    {
    }

}

public class RBNode : Node
{

    public RBNode(string text, params Node[] children) : base("RB", text, children)
    {
    }

}

public class COLONNode : Node
{

    public COLONNode(string text, params Node[] children) : base("COLON", text, children)
    {
    }

}

public class EQNode : Node
{

    public EQNode(string text, params Node[] children) : base("EQ", text, children)
    {
    }

}

public class SETNode : Node
{

    public SETNode(string text, params Node[] children) : base("SET", text, children)
    {
    }

}

public class LESNode : Node
{

    public LESNode(string text, params Node[] children) : base("LES", text, children)
    {
    }

}

public class MNode : Node
{

    public MNode(string text, params Node[] children) : base("M", text, children)
    {
    }

}

public class ANDNode : Node
{

    public ANDNode(string text, params Node[] children) : base("AND", text, children)
    {
    }

}

public class NOTNode : Node
{

    public NOTNode(string text, params Node[] children) : base("NOT", text, children)
    {
    }

}

public class ORNode : Node
{

    public ORNode(string text, params Node[] children) : base("OR", text, children)
    {
    }

}

public class PLUSNode : Node
{

    public PLUSNode(string text, params Node[] children) : base("PLUS", text, children)
    {
    }

}

public class MINUSNode : Node
{

    public MINUSNode(string text, params Node[] children) : base("MINUS", text, children)
    {
    }

}

public class MULNode : Node
{

    public MULNode(string text, params Node[] children) : base("MUL", text, children)
    {
    }

}

public class DIVNode : Node
{

    public DIVNode(string text, params Node[] children) : base("DIV", text, children)
    {
    }

}

public class MODNode : Node
{

    public MODNode(string text, params Node[] children) : base("MOD", text, children)
    {
    }

}

public class OPENNode : Node
{

    public OPENNode(string text, params Node[] children) : base("OPEN", text, children)
    {
    }

}

public class CLOSENode : Node
{

    public CLOSENode(string text, params Node[] children) : base("CLOSE", text, children)
    {
    }

}

public class COMMANode : Node
{

    public COMMANode(string text, params Node[] children) : base("COMMA", text, children)
    {
    }

}

public class ELIFNode : Node
{

    public ELIFNode(string text, params Node[] children) : base("ELIF", text, children)
    {
    }

}

public class NAMENode : Node
{

    public NAMENode(string text, params Node[] children) : base("NAME", text, children)
    {
    }

}
