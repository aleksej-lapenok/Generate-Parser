using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GenerateParser.RunTime;

namespace task1
{
    public partial class Form1 : Form
    {
        expNode tree;

        private Form1()
        {
            InitializeComponent();
        }

        public Form1(expNode tree) : this()
        {
            this.tree = tree;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode(tree.Name));
            var rootNode = treeView1.Nodes[0];
            AddNode(tree, rootNode);
            treeView1.ExpandAll();
        }

        private void AddNode(Node tree, TreeNode inView)
        {
            List<Node> nodeList;
            if (tree.Children.Count!=0)
            {
                nodeList = tree.Children;
                foreach (var node in nodeList)
                {
                    inView.Nodes.Add(new TreeNode(node.Name));
                    var tNode = inView.Nodes[inView.Nodes.Count - 1];
                    AddNode(node, tNode);
                }
            }
            else
            {
                inView.Text = tree.Name;
                inView.Nodes.Add(new TreeNode(tree.Text));
            }
        }
    }
}
