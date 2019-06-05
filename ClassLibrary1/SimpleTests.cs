using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
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
    public class SimpleTests
    {
        [Test]
        public void ShouldCreate2dTestMatrix()
        {
            //The matrix: length = xSize x ySize
            const int xSize = 10;
            const int ySize = 5;
            
            // create square matrix
            var matrix = TestMatrix(xSize, ySize);
            int xLength = matrix.GetLength(0);
            int yLength = matrix.GetLength(1);

            Assert.True(matrix.Length == (xSize * ySize));
            Assert.True(xSize == xLength);
            Assert.True(ySize == yLength);
        }

        [Test]
        public void ShouldRotateTestMatrix()
        {
            //The matrix: length = xSize x ySize
            const int xSize = 10;
            const int ySize = 5;
            
            // create square matrix
            var matrix = TestMatrix(xSize, ySize);
            int xLength = matrix.GetLength(0);
            int yLength = matrix.GetLength(1);

            var rotatedMatrix = new int[ ySize, xSize ];

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    Console.WriteLine(x + " " + y);
                    rotatedMatrix[y, x] = matrix[x, y];
                }
            }

            Assert.IsTrue(rotatedMatrix.GetLength(0) == ySize);
            Assert.IsTrue(rotatedMatrix.GetLength(1) == xSize);
            Assert.IsTrue(rotatedMatrix[ySize-1, xSize-1] == matrix[xSize-1, ySize-1]);
        }

        private static int[,] TestMatrix(int xSize, int ySize)
        {
            var matrix = new int[xSize, ySize];
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    matrix[x, y] = x + y;
                }
            }
            return matrix;
        }

        [Test]
        public void ShouldReturnTrueDetectingOverlapOf2Rectangles()
        {
            /*
            I have two rectangles characterized by 4 values each :
            Left position X, top position Y, width W and height H

            +--------------------> X axis
            |
            |    (X,Y)      (X+Width, Y)
            |    +--------------+
            |    |              |
            |    |              |
            |    |              |
            |    +--------------+
            v    (X, Y+Height)   (X+Width, Y+Height)

            Y axis

            */
            var rectangle1 = new Rectangle(0, 0, 10, 10);
            var rectangle2 = new Rectangle(5, 5, 10, 10);

            bool result = IsOverlapping(rectangle1, rectangle2);

            Assert.IsTrue(result);
            Assert.IsTrue(rectangle1.IntersectsWith(rectangle2));
        }

        private bool IsOverlapping(Rectangle rectangle1, Rectangle rectangle2)
        {
            /*
            http://stackoverflow.com/questions/13390333/two-rectangles-intersection

            if (X1+W1<X2 or X2+W2<X1 or Y1+H1<Y2 or Y2+H2<Y1):
                Intersection = Empty
            else:
                Intersection = Not Empty
            */

            if (   ((rectangle1.X + rectangle1.Width)  < rectangle2.X) 
                || ((rectangle2.X + rectangle2.Width)  < rectangle1.X)
                || ((rectangle1.Y + rectangle1.Height) < rectangle2.Y)
                || ((rectangle2.Y + rectangle2.Height) < rectangle1.Y) )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        [Test]
        public void ShouldGetDistanceBetween2Coordinates()
        {
            // http://stackoverflow.com/questions/11555355/calculating-the-distance-between-2-points
            //(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) < (d * d);

            var xy1 = new int[] {10, 10};
            var xy2 = new int[] {20, 20};

            var distance = Math.Sqrt( Math.Pow(xy1[0] - xy2[0], 2) + Math.Pow(xy1[1] - xy2[1], 2) );

            var p1 = new System.Windows.Point(xy1[0], xy1[1]);
            var p2 = new System.Windows.Point(xy2[0], xy2[1]);
            var correctDistance = System.Windows.Point.Subtract(p2, p1).Length;

            Assert.AreEqual(distance, correctDistance);
            Console.WriteLine(distance);
            Console.WriteLine(correctDistance);
        }

        [Test]
        public void FindTheLongestPalindronicSubstring()
        {
            //http://codereview.stackexchange.com/questions/43571/return-the-largest-palindrome-from-the-string
            throw new NotImplementedException();
        }

        [Test]
        public void FindAllPossibleCombinationsOfGivenDigits()
        {
            var str = new string[] {"A", "B", "C"};
            var combos = new List<string>();

            for (int outerIndex = 0; outerIndex < str.Length; outerIndex++)
            {
                var firstValue = str[outerIndex];

                for (int innerIndex = 0; innerIndex < str.Length; innerIndex++)
                {
                    int theIndex = innerIndex;
                    if (innerIndex + 1 >= str.Length)
                    {
                        theIndex = -1;
                    }

                    var secondValue = str[outerIndex];

                    string[] newStr = str.ToArray();

                    //swap
                    newStr[outerIndex] = secondValue;
                    newStr[innerIndex] = firstValue;

                    var result = String.Join("", newStr);
                    combos.Add(result);
                }
            }

            var uniqueCombos = combos.Distinct().ToList();
            uniqueCombos.ForEach(c => Console.WriteLine(c));

            Assert.IsTrue(combos.Count == str.Length * str.Length);
            Assert.IsTrue(uniqueCombos.Count == 7);
        }

        [Test]
        public void ShouldReverseAnArrayInplace()
        {
            var arr = new string[] {"one", "two", "three", "four", "five"};

            for (int i = 0; i < arr.Length / 2; i++)
            {
                string tmp = arr[i];

                //swap first and last
                arr[i] = arr[arr.Length - i - 1];
                arr[arr.Length - i - 1] = tmp;
            }
            
            Console.WriteLine(String.Join(",", arr));
        }

        [Test]
        public void ShouldReturnMaxSumOfConsecutiveIntegers()
        {
            // ArraySize = 5 AND Threshold = 25
            string firstLineInput = "5 25";
            var firstInputArray = firstLineInput.Split(' ');

            // Parse string input
            int size = Int32.Parse(firstInputArray[0]);
            int threshold = Int32.Parse(firstInputArray[1]);

            string secondLineInput = "10 7 3 15 11";
            var arr = secondLineInput.Split(' ').Select(int.Parse).ToArray();
            
            int maxSum = 0;
            int[] sequence = null;

            for (var i = 0; i < arr.Length; i++)
            {
                for (var x = arr.Length; x > i; x--)
                {
                    var subArray = arr.Skip(i).Take(x);
                    int sum = subArray.Sum();

                    if (sum > maxSum && sum <= threshold)
                    {
                        maxSum = sum;
                        sequence = subArray.ToArray();
                    }
                }
            }
            
            Assert.IsTrue(threshold >= maxSum);
            Console.WriteLine("Sequence: " + String.Join(", ", sequence));
            Console.WriteLine("MaxSum: " +maxSum);
        }

    }
}
