using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GenerateParser.RunTime;


    using test;


public class sNode : Node
{

    public mulNode mul;

    public s2Node s2;

    public sNode(string text, params Node[] children) : base("s", text, children)
    {
    }

}

public class s2Node : Node
{

    public sub2Node sub2;

    public add2Node add2;

    public s2Node(string text, params Node[] children) : base("s2", text, children)
    {
    }

}

public class sub2Node : Node
{

    public MINUSNode MINUS;

    public mulNode mul;

    public sub2Node(string text, params Node[] children) : base("sub2", text, children)
    {
    }

}

public class addNode : Node
{

    public mulNode mul;

    public add2Node add2;

    public MyDouble res;

    public addNode(string text, MyDouble res, params Node[] children) : base("add", text, children)
    {
        this.res = res;
    }

}

public class add2Node : Node
{

    public PLUSNode PLUS;

    public addNode add;

    public MyDouble res;

    public add2Node(string text, MyDouble res, params Node[] children) : base("add2", text, children)
    {
        this.res = res;
    }

}

public class mulNode : Node
{

    public numNode num;

    public mul2Node mul2;

    public MyDouble res;

    public mulNode(string text, MyDouble res, params Node[] children) : base("mul", text, children)
    {
        this.res = res;
    }

}

public class mul2Node : Node
{

    public MULNode MUL;

    public mulNode mul;

    public MyDouble res;

    public mul2Node(string text, MyDouble res, params Node[] children) : base("mul2", text, children)
    {
        this.res = res;
    }

}

public class numNode : Node
{

    public LBNode LB;

    public addNode add;

    public RBNode RB;

    public NUMNode NUM;

    public MyDouble res;

    public numNode(string text, MyDouble res, params Node[] children) : base("num", text, children)
    {
        this.res = res;
    }

}

public class PLUSNode : Node
{

    public PLUSNode(string text, params Node[] children) : base("PLUS", text, children)
    {
    }

}

public class MULNode : Node
{

    public MULNode(string text, params Node[] children) : base("MUL", text, children)
    {
    }

}

public class NUMNode : Node
{

    public NUMNode(string text, params Node[] children) : base("NUM", text, children)
    {
    }

}

public class LBNode : Node
{

    public LBNode(string text, params Node[] children) : base("LB", text, children)
    {
    }

}

public class RBNode : Node
{

    public RBNode(string text, params Node[] children) : base("RB", text, children)
    {
    }

}

public class MINUSNode : Node
{

    public MINUSNode(string text, params Node[] children) : base("MINUS", text, children)
    {
    }

}
