using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeLib;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Visit the neighbors you are connected to. This is recurse level by level.
    /// </summary>
    [TestFixture]
    public class ExpressionTree
    {
        [Test]
        public void ShouldGenerateInOrderBinaryTreeSucessfully()
        {
            string expression = "(a+b)-(c+d)";
        }
        
    }
}
