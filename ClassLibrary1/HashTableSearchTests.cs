using System;
using System.Collections;
using HashTableLib;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class HashTableSearch
    {
        private int allocatedSizeOfTable = 1000;
        //Better way:
        /*
            function h(hashCode) = ((positiveIntA * hashCode + positiveIntB) % LargePrimeNumber) % UpperBoundSize 
        */
        [Test]
        public void ShouldGenerateHashhSuccessfully()
        {
            string key = "a";
            int hash = Hash.GenerateHash(key, allocatedSizeOfTable);

            Assert.IsTrue(hash > 0);
            Console.WriteLine(hash);
        }
        
        [Test]
        public void ShouldSearchHashTableSucessfully()
        {
            var myHashTable = new MyHashTable(allocatedSizeOfTable);

            Assert.IsNotNull(myHashTable);
        }

        [Test]
        public void ShouldAddItemToHashTable()
        {
            var myHashTable = new MyHashTable(allocatedSizeOfTable);
            int hashCode = Hash.GenerateHash("a", allocatedSizeOfTable);

            myHashTable.Set("a","a");
            
            Assert.IsNotNull(myHashTable);
            Assert.IsTrue(myHashTable.LookpUp.Count == 1);
            Assert.IsNotNull(myHashTable.LookpUp.ContainsKey(hashCode));
        }

        [Test]
        public void ShouldGetFirstItemByKey()
        {
            var myHashTable = new MyHashTable(allocatedSizeOfTable);
            int hashCode = Hash.GenerateHash("a", allocatedSizeOfTable);
            myHashTable.Set("a","a");
            myHashTable.Set("a","a2");

            string result = myHashTable.GetFirst("a");

            Assert.AreEqual(result, "a");
        }

        [Test]
        public void ShouldGetAllItemByKey()
        {
            var myHashTable = new MyHashTable(allocatedSizeOfTable);
            int hashCode = Hash.GenerateHash("a", allocatedSizeOfTable);
            myHashTable.Set("a", "a");
            myHashTable.Set("a", "a2");

            List<string> result = myHashTable.GetAll("a");

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0] == "a");
            Assert.IsTrue(result[1] == "a2");
        }
    }
}