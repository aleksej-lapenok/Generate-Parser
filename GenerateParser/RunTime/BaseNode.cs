using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateParser.RunTime
{
    class BaseNode:INode
    {
        public List<BaseNode> children;

        public BaseNode(params BaseNode[] children)
        {
            this.children = children.ToList();
        }
    }
}
