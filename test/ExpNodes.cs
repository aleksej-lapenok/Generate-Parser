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

    public MINUSNode MINUS;

    public subNode sub;

    public MyDouble res;

    public sNode(string text, MyDouble res, params Node[] children) : base("s", text, children)
    {
        this.res = res;
    }

}

public class subNode : Node
{

    public mulNode mul;

    public sub2Node sub2;

    public MyDouble res;

    public subNode(string text, MyDouble res, params Node[] children) : base("sub", text, children)
    {
        this.res = res;
    }

}

public class sub2Node : Node
{

    public MINUSNode MINUS;

    public subNode sub;

    public MyDouble res;

    public sub2Node(string text, MyDouble res, params Node[] children) : base("sub2", text, children)
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

    public subNode sub;

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
