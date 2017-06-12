using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    public class Token
    {
        int id;
        string str;
        bool skip;

        public Token(int id, string str, bool skip)
        {
            Id = id;
            Str = str;
            Skip = skip;
        }

        public Token(int id, string str) : this(id, str, false)
        {

        }

        public int Id
        {
            get => id;
            protected set => id = value;
        }

        public string Str
        {
            get => str;
            protected set => str = value;
        }

        public bool Skip
        {
            get => skip;
            protected set => skip = value;
        }

        public static bool operator== (Token t1, Token t2)
        {
            return t1.Id == t2.Id;
        }

        public static bool operator!=(Token t1, Token t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            if(obj is Token token)
            {
                return Id == token.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
