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
    public class BinaryTreeTests
    {
        [Test]
        public void ShouldGenerateInOrderBinaryTreeSucessfully()
        {
            int[] keys = new int[] { 1, 2, 3, 4, 5, 6, 7};

            Node result = BinaryTreeUtil.CreateInOrderBinaryTree(keys);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.NodeValue == 4);
            Assert.IsTrue(result.Left.NodeValue == 2);
            Assert.IsTrue(result.Right.NodeValue == 6);
        }

        [Test]
        public void WhenValidatingAValidBinaryTreeShouldReturnTrue()
        {
            var isBinaryTree = BinaryTreeUtil
                .IsBinaryTree(CreateValidBinaryTree(), int.MinValue, int.MaxValue);

            Assert.IsTrue(isBinaryTree);
        }

        [Test]
        public void WhenValidatingAInValidBinaryTreeShouldReturnFalse()
        {
            var isBinaryTree = BinaryTreeUtil
                .IsBinaryTree(CreateInValidBinaryTree(), int.MinValue, int.MaxValue);

            Assert.IsFalse(isBinaryTree);
        }
        
        [Test]
        public void ShouldFirstBreathSearchSuccessfully()
        {
            var result = BinaryTreeUtil.BreathFirstSearch(CreateInValidBinaryTree(), 7);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.NodeValue == 7);
        }

        [Test]
        public void ShouldDepthSearchSuccessfully()
        {
            var result = BinaryTreeUtil.DepthSearch(CreateValidBinaryTree(), 4);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.NodeValue == 4);
        }
        
        [Test]
        public void ShouldPerformALimitedDepthSearchSuccessfully()
        {
            int maxDepth = 3;
            var binaryTreeUtil = new BinaryTreeUtil();
            var result = binaryTreeUtil.MaxDepthSearch(CreateValidBinaryTree(), 4, maxDepth, 0);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.NodeValue == 4);
        }

        [Test]
        public void ShouldOnlySearch1LevelDeepAndReturnNull()
        {
            int maxDepth = 2;
            int currentDepth = 0;
            var binaryTreeUtil = new BinaryTreeUtil();
            var tree = CreateValidBinaryTree();

            var result = binaryTreeUtil.MaxDepthSearch(tree, 4, maxDepth, currentDepth);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldMapAllNodesByPerformFullTreeTraversal()
        {
            var tree = CreateValidBinaryTree();

            var nodeMap = BinaryTreeUtil.MapAllNodes(tree, n => new Node[] { n.Left, n.Right});
            Node nodeId3 = nodeMap.FirstOrDefault(n => n.NodeValue == 3);

            Assert.IsTrue(nodeMap.Count > 0);
            Assert.IsNotNull(nodeId3);
            Assert.IsTrue(nodeId3.NodeValue == 3);
            Assert.IsTrue(nodeId3.Left.NodeValue == 1);
            Assert.IsTrue(nodeId3.Right.NodeValue == 4);
        }

        [Test]
        public void ShouldGetParentIds()
        {
            var tree = CreateValidBinaryTree();
            var parentLookUp = new Dictionary<int, List<int>>();

            var nodeMap = BinaryTreeUtil.MapAllNodes(
                tree, 
                n => new Node[] { n.Left, n.Right }, 
                BinaryTreeUtil.MapParentNodes(parentLookUp)
            );
            Node nodeId3 = nodeMap.FirstOrDefault(n => n.NodeValue == 3);
            List<int> parentIds = parentLookUp[nodeId3.NodeValue];

            Assert.IsTrue(parentLookUp.Count > 0);
            Assert.IsNotNull(nodeId3);
            Assert.IsTrue(parentIds.Count == 1);
        }


        private static Node CreateValidBinaryTree()
        {
            /*
                         6
                        / \
                      3     8
                     / \   / \
                    1   4 7   9  
            */
            int[] keys = new int[] { 1, 3, 4, 6, 7, 8, 9,10 };

            return BinaryTreeUtil.CreateInOrderBinaryTree(keys);
        }

        private static Node CreateInValidBinaryTree()
        {
            /*
                         6
                        / \
                      3     8
                     / \     \
                    1   7     9  
            */
            var rootNode = new Node(6)
            {
                Left = new Node(3, new Node(1, null, null), new Node(7, null, null)),
                Right = new Node(8, null, new Node(9, null, null))
            };
            return rootNode;
        }
    }
}
