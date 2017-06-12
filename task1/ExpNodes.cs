using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;



public class expNode : Node
{

    public orNode or;

    public expNode(string text, params Node[] children) : base("exp", text, children)
    {
    }

}

public class orNode : Node
{

    public xorNode xor;

    public or2Node or2;

    public orNode(string text, params Node[] children) : base("or", text, children)
    {
    }

}

public class or2Node : Node
{

    public ORNode OR;

    public orNode or;

    public or2Node(string text, params Node[] children) : base("or2", text, children)
    {
    }

}

public class xorNode : Node
{

    public andNode and;

    public xor2Node xor2;

    public xorNode(string text, params Node[] children) : base("xor", text, children)
    {
    }

}

public class xor2Node : Node
{

    public XORNode XOR;

    public xorNode xor;

    public xor2Node(string text, params Node[] children) : base("xor2", text, children)
    {
    }

}

public class andNode : Node
{

    public negateNode negate;

    public and2Node and2;

    public andNode(string text, params Node[] children) : base("and", text, children)
    {
    }

}

public class and2Node : Node
{

    public ANDNode AND;

    public andNode and;

    public and2Node(string text, params Node[] children) : base("and2", text, children)
    {
    }

}

public class negateNode : Node
{

    public NEGATENode NEGATE;

    public varNode var;

    public negateNode(string text, params Node[] children) : base("negate", text, children)
    {
    }

}

public class varNode : Node
{

    public L_BRACKETNode L_BRACKET;

    public orNode or;

    public R_BRACKETNode R_BRACKET;

    public VARNode VAR;

    public varNode(string text, params Node[] children) : base("var", text, children)
    {
    }

}

public class L_BRACKETNode : Node
{

    public L_BRACKETNode(string text, params Node[] children) : base("L_BRACKET", text, children)
    {
    }

}

public class R_BRACKETNode : Node
{

    public R_BRACKETNode(string text, params Node[] children) : base("R_BRACKET", text, children)
    {
    }

}

public class ORNode : Node
{

    public ORNode(string text, params Node[] children) : base("OR", text, children)
    {
    }

}

public class XORNode : Node
{

    public XORNode(string text, params Node[] children) : base("XOR", text, children)
    {
    }

}

public class ANDNode : Node
{

    public ANDNode(string text, params Node[] children) : base("AND", text, children)
    {
    }

}

public class NEGATENode : Node
{

    public NEGATENode(string text, params Node[] children) : base("NEGATE", text, children)
    {
    }

}

public class VARNode : Node
{

    public VARNode(string text, params Node[] children) : base("VAR", text, children)
    {
    }

}
