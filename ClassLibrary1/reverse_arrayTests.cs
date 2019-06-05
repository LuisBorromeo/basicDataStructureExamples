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
    public class reverse_arrayTests
    {
        [Test]
        public void ReverseAnArrayInPlace()

        {
            var array = new[] {1, 2, 3, 4, 5};
            var reversedArray = new[] { 5, 4, 3, 2, 1 };

            int first = 0;
            int last = array.Length - 1;

            while (first < last)
            {
                //hold init value;
                var initialValue = array[first];

                //swap
                array[first] = array[last];
                array[last] = initialValue;

                first++;
                last--;
            }


            Assert.AreEqual(array, reversedArray);
        }
        
    }
}
