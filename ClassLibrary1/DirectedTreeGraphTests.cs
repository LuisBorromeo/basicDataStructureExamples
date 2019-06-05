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
    /// typically used to explore the entire graph. 
    /// BSF is often used to see if a search value is reachable from a node.
    /// </summary>
    [TestFixture]
    public class DirectedTreeGraphTests
    {
        [Test]
        public void ShouldBuildTreeFromGraph()
        {
//            var testTreeGraph = CreateTestDirectedTreeGraph();
//            foreach (var item in testTreeGraph)
//            {
//                new No
//            }
        }

        /*
            (a)  ->  (b)      (c)
            |     /   |     /  |
            V    /    V   V    V
           (d)   <-  (e)      (f)
        */
        private static Dictionary<char, List<char>> CreateTestDirectedTreeGraph()
        {
//            var testGraph = new List<Node<char?>>();
//            testGraph.Set(new Node<char?>(null, 'a', new List<char?>() { 'b', 'd' }));
//            testGraph.Set(new Node<char?>('a', 'b', new List<char?>() { 'a', 'd', 'e' }));
//            testGraph.Set(new Node<char?>('a', 'd', new List<char?>() { 'a', 'b', 'e' }));

            var testGraph = new Dictionary<char, List<char>>();
            testGraph.Add('a', new List<char>() {'b', 'd'});
            testGraph.Add('b', new List<char>() {'a', 'd', 'e'});
            testGraph.Add('d', new List<char>() {'a', 'b', 'e'});
            testGraph.Add('e', new List<char>() {'a', 'b', 'd', 'c'});
            testGraph.Add('c', new List<char>() {'e', 'f'});

            return testGraph;
        }
    }
}
