using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GenerateParser.RunTime
{
    public abstract class BaseLexer : ILexer
    {
        StreamReader input;
        private char curChar;
        private int curPos;

        private List<Token> tokens = new List<Token>();
        private List<string> texts = new List<string>();
        private int pos = 0;
        private string buffer;

        public BaseLexer(StreamReader input)
        {
            Input = input;
            while (!input.EndOfStream)
            {
                NextChar();
                MakeToken(curChar);
            }
            //Buffer = input.ReadToEnd();
            MakeToken(-1);
            if (buffer.Length > 0)
                throw new ParserException("Unexpected simbol");
        }

        protected List<Token> ListTokens
        {
            get => tokens;
        }

        protected string Buffer
        {
            get => buffer;
            set => buffer = value;
        }

        private StreamReader Input
        {
            set => input = value;
            get => input;
        }

        protected int CurPos
        {
            get => curPos;
            private set => curPos = value;
        }

        protected int CurChar
        {
            get => curChar;
        }

        protected void NextChar()
        {
            CurPos++;
            curChar = (char)input.Read();
        }

        public Token NextToken()
        {
            pos++;
            return GetToken();
        }

        public Token PrevioseToken()
        {
            pos--;
            return GetToken();
        }

        protected abstract Token[] Tokens
        {
            get;
        }
        protected abstract Regex[] Values
        {
            get;
        }

        protected void MakeToken(int c)
        {
            if (c != -1)
                Buffer += (char)c;
            for (int i = 0; i < Tokens.Length; i++)
            {
                Match res = Values[i].Match(Buffer);
                if (res.Success && res.Index == 0 && (res.Length != Buffer.Length || c == -1) && res.Length != 0)
                {
                    AddToken(Tokens[i], res.Value);
                    Buffer = Buffer.Substring(res.Length);
                    i = -1;
                }
            }
        }

        protected void AddToken(Token token, string text)
        {
            if (!token.Skip)
            {
                tokens.Add(token);
                texts.Add(text);
            }
        }

        public Token GetToken()
        {
            if (pos < tokens.Count && pos >= 0)
                return tokens[pos];
            if (pos >= tokens.Count)
                return new Token(-1, "");
            return null;
        }

        protected List<string> Texts
        {
            get => texts;
        }

        public string GetText()
        {
            if (pos < tokens.Count && pos >= 0)
                return texts[pos];
            if (pos >= tokens.Count)
                return "";
            return null;
        }
    }
    
}
