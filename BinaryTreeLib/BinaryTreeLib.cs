using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BinaryTreeLib
{
    public class BinaryTreeUtil
    {
        public static bool IsBinaryTree(Node node, int min, int max)
        {
            //Last node tree or tree empty
            if (node == null)
            {
                return true;
            }

            if (node.NodeValue > min
                 && node.NodeValue < max
                 && IsBinaryTree(node.Left, min, node.NodeValue)
                 && IsBinaryTree(node.Right, node.NodeValue, max))
                return true;
            else
                return false;
        }

        public static Node BreathFirstSearch(Node node, int searchValue)
        {
            var q = new Queue<Node>();
            if (node != null)
            {
                q.Enqueue(node);
            }

            while (q.Count > 0)
            {
                var currentNode = q.Dequeue();
                //Console.WriteLine(currentNode.NodeValue);

                if (currentNode.NodeValue == searchValue)
                {
                    return currentNode;
                }

                if (currentNode.Left != null)
                {
                    q.Enqueue(currentNode.Left);
                }

                if (currentNode.Right != null)
                {
                    q.Enqueue(currentNode.Right);
                }
            }

            return null;
        }

        public static Node DepthSearch(Node node, int searchValue)
        {
            if (node != null)
            {
                Console.WriteLine(node.NodeValue);

                if (node.NodeValue == searchValue)
                {
                    return node;
                }

                if (node.NodeValue > searchValue)
                {
                    return DepthSearch(node.Left, searchValue);
                }
                else if (node.NodeValue < searchValue)
                {
                    return DepthSearch(node.Right, searchValue);
                }
            }

            return null;
        }

        public Node MaxDepthSearch(
            Node node,
            int searchValue,
            int maxDepth,
            int currentDepth)
        {
                return DepthSearchLimited(node, searchValue, maxDepth, currentDepth);
        }

        private Node DepthSearchLimited(
                                    Node node, 
                                    int searchValue, 
                                    int maxDepth, 
                                    int currentDepth)
        {
            //int i = currentDepth;
            if (node != null && (currentDepth < maxDepth))
            {
                Console.WriteLine(node.NodeValue + " Depth:" + currentDepth);

                if (node.NodeValue == searchValue)
                {
                    return node;
                }

                if (node.NodeValue > searchValue)
                {
                    int newDepth = currentDepth + 1;
                    return DepthSearchLimited(node.Left, searchValue, maxDepth, newDepth);
                }
                else if (node.NodeValue < searchValue)
                {
                    int newDepth = currentDepth + 1;
                    return DepthSearchLimited(node.Right, searchValue, maxDepth, newDepth);
                }
            }

            return null;
        }

        public static Node CreateInOrderBinaryTree(int[] keys)
        {
            if (keys != null && keys.Length > 0)
            {
                var node = ToBST(keys, 0, keys.Length - 1);
                return node;
            }

            return null;
        }

        private static Node ToBST(int[] keys, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            // Get the middle index of the array
            int mid = start + (end - start) / 2;

            var childNode = new Node(keys[mid]);

            // get the middle of the left and recurse
            childNode.Left = ToBST(keys, start, mid - 1);

            // get the middle of the right and recurse
            childNode.Right = ToBST(keys, mid + 1, end);

            return childNode;
        }

        public static IEnumerable<T> DepthFirstTraversal<T>(T start, Func<T, IEnumerable<T>> children)
        {
            var visited = new HashSet<T>();
            var stack = new Stack<T>();
            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                visited.Add(current);
                yield return current;

                var neighbours = children(current).Where(node => !visited.Contains(node));

                // If you don't care about the left-to-right order, remove the Reverse
                foreach (var neighbour in neighbours.Reverse())
                {
                    stack.Push(neighbour);
                }
            }
        }

        public static HashSet<Node> MapAllNodes(
            Node start, 
            Func<Node, IEnumerable<Node>> children, 
            Func<int, int, bool> visitAction = null)
        {
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                bool alreadyAdded = !visited.Add(current);
                if (alreadyAdded)
                {
                    continue;
                }
                
                var remainingChildrenToVisit = children(current).Where(node => !visited.Contains(node));

                // If you don't care about the left-to-right order, remove the Reverse
                foreach (var child in remainingChildrenToVisit.Reverse())
                {
                    if (child != null)
                    {
                        if (visitAction != null)
                        {
                            visitAction(child.NodeValue, current.NodeValue);
                        }

                        stack.Push(child);
                    }
                }
            }

            return visited;
        }

        //Lambda logic to perform on visit of each node
        public static Func<int, int, bool> MapParentNodes(Dictionary<int, List<int>> parentLookup)
        {
            return (childId, parentId) =>
            {
                List<int> parents;

                if (parentLookup.TryGetValue(childId, out parents) == false)
                {
                    // Insert if new
                    parentLookup.Add(childId, new List<int>() { parentId });
                    return true;
                }
                else if (!parents.Contains(parentId))
                {
                    // Append if does not exist
                    parents.Add(parentId);
                    parentLookup[childId] = parents;
                    return true;
                }

                return false;
            };
        }

    }


    public class NodeInfo<T>
    {
        public int[] ParentIds { get; set; }
        public int Depth { get; set; }
        public T NodeObject { get; set; }
    }

    public class Node
    {
        public int NodeValue { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        
        public Node(int nodeValue, Node left = null, Node right = null)
        {
            NodeValue = nodeValue;
            Left = left;
            Right = right;
        }
    }
}