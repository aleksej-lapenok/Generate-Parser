using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

using Antlr4.Runtime;

using GenerateParser.Rules;

namespace GenerateParser
{
    class Generator
    {
        Dictionary<string, LexerRule> lexers = new Dictionary<string, LexerRule>();
        Dictionary<string, Rule> rules = new Dictionary<string, Rule>();
        string header, name;


        Dictionary<string, HashSet<int>> first = new Dictionary<string, HashSet<int>>();
        Dictionary<string, HashSet<int>> follow = new Dictionary<string, HashSet<int>>();

        List<string> users = new List<string>()
        {
            "System",
            "System.Collections.Generic",
            "System.Linq",
            "System.Text",
            "System.IO",
            "System.Text.RegularExpressions",
            "GenerateParser.RunTime",
        };

        public Generator(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            var lexer = new GrammarLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new GrammarParser(tokenStream);
            var context = parser.file();
            if (context.header() != null)
            {
                header = context.header().CODE().GetText();
                header = header.Substring(1, header.Length - 2);
            }
            else
                header = "";
            name = context.LexerName().GetText();

            Console.WriteLine($"Generate grammar \"{name}\"");
            var rules = context.line();
            foreach (var rule in rules)
            {
                if (rule?.ret is LexerRule lexerRule)
                    lexers.Add(lexerRule?.Name, lexerRule);
                else
                    this.rules.Add(rule?.ret?.Name, rule?.ret);
            }
            foreach (var rule in this.rules.Values)
            {
                foreach (var pr in rule.Productions.Data)
                {
                    foreach (var name in pr.Productions)
                    {
                        if (lexers.ContainsKey(name.name) || this.rules.ContainsKey(name.name))
                            continue;
                        throw new Exception($"Unknown name {name}");
                    }
                }
            }
        }

        public void Generate()
        {
            BuildFirst();
            BuildFollow();
            CheckGrammar();
            GenerateLexer();
            GenerateNodes();
            GenerateParser();
        }

        private void CheckGrammar()
        {
            foreach (var rule in rules.Values)
            {
                for (int i = 0; i < rule.Productions.Data.Count; i++)
                {
                    for (int j = i + 1; j < rule.Productions.Data.Count; j++)
                    {
                        var firstA = First(rule.Productions.Data[i]);
                        firstA.Remove(-1);
                        var firstB = First(rule.Productions.Data[j]);
                        firstB.Remove(-1);
                        var intersect = firstA.Intersect(firstB).ToArray();
                        if (intersect.Length != 0 ||
                            firstA.Contains(-1) && follow[rule.Name].Intersect(firstB).Count() != 0 //||
                            /*firstB.Contains(-1) && follow[rule.Name].Intersect(firstA).Count() != 0*/)
                            throw new Exception($"Not LL1 grammar for rule {rule.Name}.");
                    }
                }
            }
        }

        private void GenerateNodes()
        {
            using (StreamWriter fout = new StreamWriter("../../" + name + "Nodes.cs"))
            {
                WriteUsing(fout);
                var rules = this.rules.Values.ToList();
                rules.AddRange(lexers.Values);
                foreach (var rule in rules)//todo: write method to get child by name of rule
                {
                    if (rule is LexerRule lexerRule && lexerRule.Skip)
                        continue;
                    fout.WriteLine();
                    fout.WriteLine($"public class {rule.Name}Node : Node");
                    WriteOpen(fout, 0);
                    fout.WriteLine();
                    var children = new HashSet<(string name, string nameVar)>();
                    foreach(var production in rule.Productions.Data)
                    {
                        foreach(var child in production.Productions)
                        {
                            children.Add(child);
                        }
                    }
                    foreach(var child in children)
                    {
                        WriteSpaces(fout, 4);
                        fout.WriteLine($"public {child.name}Node {child.nameVar};");
                        fout.WriteLine();
                    }
                    WriteSpaces(fout, 4);
                    if (rule is RuleWithCode ruleCode)
                    {
                        fout.WriteLine($"public {ruleCode.Arg};");
                        fout.WriteLine();
                        WriteSpaces(fout, 4);
                        fout.WriteLine($"public {ruleCode.Name}Node(string text, {ruleCode.Arg}, params Node[] children) : base(\"{rule.Name}\", text, children)");
                    }
                    else
                        fout.WriteLine($"public {rule.Name}Node(string text, params Node[] children) : base(\"{rule.Name}\", text, children)");
                    WriteOpen(fout, 4);
                    if(rule is RuleWithCode rCode)
                    {
                        WriteSpaces(fout, 8);
                        var nameVar = rCode.Arg.Substring(rCode.Arg.LastIndexOf(' ') + 1);
                        fout.WriteLine($"this.{nameVar} = {nameVar};");
                    }
                    WriteClose(fout, 4);
                    fout.WriteLine();
                    WriteClose(fout, 0);
                }
            }
        }

        private void GenerateLexer()
        {
            using (StreamWriter fout = new StreamWriter("../../" + name + "Lexer.cs"))
            {
                WriteUsing(fout);
                fout.WriteLine($"class {name}Lexer : BaseLexer");
                fout.WriteLine("{");
                WriteSpaces(fout, 4);
                fout.WriteLine("Token[] tokens = new Token[]");
                WriteOpen(fout, 4);
                var l = lexers.Keys.ToArray();
                for (int i = 0; i < l.Length; i++)
                {
                    WriteSpaces(fout, 8);
                    fout.WriteLine($"new Token({i}, \"{l[i]}\", {lexers[l[i]].Skip.ToString().ToLower()}),");
                }
                WriteSpaces(fout, 4);
                fout.WriteLine("};");
                fout.WriteLine();
                WriteSpaces(fout, 4);
                fout.WriteLine("protected override Token[] Tokens => tokens;");
                fout.WriteLine();
                WriteSpaces(fout, 4);
                fout.WriteLine("Regex[] values = new Regex[]");
                WriteOpen(fout, 4);
                for (int i = 0; i < l.Length; i++)
                {
                    WriteSpaces(fout, 8);
                    fout.WriteLine($"new Regex(@\"{lexers[l[i]].Exp}\"),");
                }
                WriteSpaces(fout, 4);
                fout.WriteLine("};");
                fout.WriteLine();
                WriteSpaces(fout, 4);
                fout.WriteLine("protected override Regex[] Values => values;");
                fout.WriteLine();
                WriteSpaces(fout, 4);
                fout.WriteLine("public " + name + "Lexer(StreamReader fin) : base(fin) ");
                WriteOpen(fout, 4);
                WriteClose(fout, 4);
                fout.WriteLine();
                fout.WriteLine("}");
            }
        }

        private void GenerateParser()
        {
            using (StreamWriter fout = new StreamWriter("../../" + name + "Parser.cs"))
            {
                WriteUsing(fout);
                fout.WriteLine();
                fout.WriteLine($"class {name}Parser : BaseParser");
                WriteOpen(fout, 0);
                WriteSpaces(fout, 4);
                fout.WriteLine($"public {name}Parser({name}Lexer lexer) : base(lexer)");
                WriteOpen(fout, 4);
                WriteClose(fout, 4);
                foreach (var rule in rules.Values)
                {
                    fout.WriteLine();
                    WriteSpaces(fout, 4);
                    fout.WriteLine($"public {rule.Name}Node {rule.Name}()");
                    WriteOpen(fout, 4);
                    WriteSpaces(fout, 8);
                    fout.WriteLine("var token = lexer.GetToken();");
                    foreach (var production in rule.Productions.Data)
                    {
                        WriteSpaces(fout, 8);
                        fout.Write("if (");
                        bool isFirst = true;
                        var ids = First(production);
                        if (ids.Contains(-1))
                            fout.Write("true");
                        else
                            foreach (var id in ids)
                            {
                                if (!isFirst)
                                    fout.Write(" || ");
                                fout.Write($"token.Id == {id}");
                                isFirst = false;
                            }
                        fout.WriteLine($")");
                        WriteOpen(fout, 8);
                        WriteSpaces(fout, 12);
                        fout.WriteLine("var text = \"\";");
                        var map = new Dictionary<string, int>();//map from vars to id
                        for (int i = 0; i < production.Productions.Count; i++)
                        {
                            WriteSpaces(fout, 12);
                            fout.WriteLine($"var arg{i} = {production.Productions[i].name}();");
                            WriteSpaces(fout, 12);
                            fout.WriteLine($"text += arg{i}.Text;");
                            map.Add(production.Productions[i].nameVar, i);
                        }
                        WriteSpaces(fout, 12);
                        fout.Write($"var result = new {rule.Name}Node(text");
                        if (rule is RuleWithCode)
                        {
                            fout.Write(", null");
                        }
                        for (int i = 0; i < production.Productions.Count; i++)
                        {
                            fout.Write($", arg{i}");
                        }

                        fout.WriteLine(");");
                        foreach (var name in production.Productions)
                        {
                            WriteSpaces(fout, 12);
                            fout.WriteLine($"result.{name.nameVar} = arg{map[name.nameVar]};");
                        }
                        if (rule is RuleWithCode)
                        {
                            WriteSpaces(fout, 12);
                            var code = ((ProductionWithCode)production).Code;
                            code=code.Replace("$", "result.");
                            fout.WriteLine(code);
                        }
                        WriteSpaces(fout, 12);
                        fout.WriteLine("token = lexer.GetToken();");
                        WriteSpaces(fout, 12);
                        fout.Write("if (");
                        isFirst = true;
                        if(follow[rule.Name].Count==0)
                        {
                            fout.Write("token.Id != -1");
                        }
                        foreach(var id in follow[rule.Name])
                        {
                            if (!isFirst)
                                fout.Write(" && ");
                            if (id == -2)
                                fout.Write($"token.Id != {-1}");
                            else
                                fout.Write($"token.Id != {id}");
                            isFirst = false;
                        }
                        fout.WriteLine(")");
                        WriteSpaces(fout, 16);
                        fout.WriteLine("throw new ParserException(\"Got unxpected token from lexer\");");
                        WriteSpaces(fout, 12);
                        fout.WriteLine("return result;");
                        
                        WriteClose(fout, 8);
                    }
                    WriteSpaces(fout, 8);
                    fout.WriteLine("throw new ParserException(\"Got unxpected token from lexer\");");
                    WriteSpaces(fout, 8);
                    fout.WriteLine("//return null");
                    WriteClose(fout, 4);
                    fout.Flush();
                }
                var lexerRules = lexers.Values.ToArray();
                for (int i = 0; i < lexers.Values.Count; i++)
                {
                    if (lexerRules[i].Skip)
                        continue;
                    fout.WriteLine();
                    var rule = lexerRules[i];
                    WriteSpaces(fout, 4);
                    fout.WriteLine($"public {rule.Name}Node {rule.Name}()");
                    WriteOpen(fout, 4);
                    WriteSpaces(fout, 8);
                    fout.WriteLine($"if (lexer.GetToken().Id == {i})");
                    WriteOpen(fout, 8);
                    WriteSpaces(fout, 12);
                    fout.WriteLine($"var result = new {rule.Name}Node(lexer.GetText());");
                    WriteSpaces(fout, 12);
                    fout.WriteLine("lexer.NextToken();");
                    WriteSpaces(fout, 12);
                    fout.WriteLine($"return result;");
                    WriteClose(fout, 8);
                    WriteSpaces(fout, 8);
                    fout.WriteLine("throw new ParserException(\"Got unxpected token from lexer\");");
                    WriteClose(fout, 4);
                }
                WriteClose(fout, 0);
            }
        }

        private void BuildFirst()
        {
            foreach (var rule in rules.Values)
            {
                first.Add(rule.Name, new HashSet<int>());
            }
            var lexerRules = lexers.Values.ToArray();
            for (int i = 0; i < lexerRules.Length; i++)
            {
                first.Add(lexerRules[i].Name, new HashSet<int>());
                first[lexerRules[i].Name].Add(i);
                if (new Regex(lexerRules[i].Exp).Match("").Success)
                    first[lexerRules[i].Name].Add(-1);
            }
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (var rule in rules.Values)
                {
                    foreach (var production in rule.Productions.Data)
                    {
                        foreach (var id in First(production))
                        {
                            changed |= first[rule.Name].Add(id);
                        }
                    }
                }
            }
        }

        private void BuildFollow()
        {
            foreach (var rule in rules.Values)
            {
                follow.Add(rule.Name, new HashSet<int>());
                follow[rule.Name].Add(-2);//end of str
            }
            foreach (var rule in lexers.Values)
            {
                follow.Add(rule.Name, new HashSet<int>());
            }
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (var rule in rules.Values)
                {
                    foreach (var production in rule.Productions.Data)//по каждому правилу
                    {
                        for (int i = 0; i < production.Productions.Count - 1; i++)//по каждому кусочку
                        {
                            foreach (var id in first[production.Productions[i + 1].name])//follow[i].add(first[i+1])
                            {
                                if (id != -1)
                                    changed |= follow[production.Productions[i].name].Add(id);
                            }
                            if (first[production.Productions[i + 1].name].Contains(-1))//if eps in first[i+1]
                            {
                                for (int j = i + 1; j < production.Productions.Count; j++)//if first[j] contains eps
                                {
                                    if (j < production.Productions.Count - 1)//if j+1 exist, follow[i].add(first[j+1])
                                    {
                                        foreach (var id in first[production.Productions[j + 1].name])
                                        {
                                            if (id != -1)
                                                changed |= follow[production.Productions[i].name].Add(id);
                                        }
                                        if (!first[production.Productions[j + 1].name].Contains(-1))//if j+1 not contains eps, then finish
                                            break;
                                    }
                                    else
                                        foreach (var id in follow[rule.Name])//if j is last then follow[i].add(follow[A])
                                        {
                                            changed |= follow[production.Productions[i].name].Add(id);
                                        }

                                }
                            }
                        }
                        if (production.Productions.Count != 0)
                        {
                            foreach (var id in follow[rule.Name])
                            {
                                changed |= follow[production.Productions.Last().name].Add(id);
                            }
                        }
                    }
                }
            }
        }

        private HashSet<int> First(Production production)
        {
            var back = new HashSet<int>();
            if (production.Productions.Count == 0)
            {
                back.Add(-1);
            }
            foreach (var name in production.Productions)
            {
                foreach (var val in first[name.name])
                {
                    back.Add(val);
                }
                if (!first[name.name].Contains(-1))//-1 - empty string
                    break;
            }
            return back;
        }

        private void WriteOpen(StreamWriter fout, int col)
        {
            WriteSpaces(fout, col);
            fout.WriteLine("{");
        }

        private void WriteClose(StreamWriter fout, int col)
        {
            WriteSpaces(fout, col);
            fout.WriteLine("}");
        }

        private void WriteSpaces(StreamWriter fout, int col)
        {
            for (int i = 0; i < col; i++)
                fout.Write(" ");
        }

        private void WriteUsing(StreamWriter fout)
        {
            foreach (var u in users)
            {
                fout.WriteLine($"using {u};");
            }
            fout.WriteLine();
            fout.WriteLine(header);
        }
    }
}
