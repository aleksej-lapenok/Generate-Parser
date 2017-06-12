using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    public abstract class Node : INode
    {
        private List<Node> children;
        private string name;
        private string text;

        public Node(string name, string text, params Node[] children)
        {
            Children = children.ToList();
            Name = name;
            Text = text;
        }

        public void AddChildren(Node child)
        {
            children.Add(child);
        }

        public List<Node> Children
        {
            get => children;
            protected set => children = value;
        }

        public string Name
        {
            get => name;
            protected set => name = value;
        }

        public string Text
        {
            get => text;
            set => text = value;
        }

    }
}
