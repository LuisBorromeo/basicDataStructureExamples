using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AmazonTest
    {
        [Test]
        public void ShouldReturnAllGroups()
        {
            var validWords = new List<String>() { "cat", "tac", "pot", "top", "meow", "act" };
            var program = new Program(validWords.ToArray());

            var allGroups = program.GetAllGroups();
            
            Assert.IsNotNull(allGroups);
            Assert.IsTrue(allGroups.Length == 3);
            Assert.IsTrue(allGroups[0].Contains("cat"));
            Assert.IsTrue(allGroups[0].Contains("tac"));
            Assert.IsTrue(allGroups[0].Contains("act"));
            Assert.IsTrue(allGroups[1].Contains("pot"));
            Assert.IsTrue(allGroups[1].Contains("top"));
            Assert.IsTrue(allGroups[2].Contains("meow"));
        }

        [Test]
        public void LinqGroupByImplementation()
        {
            var validWords = new List<String>() { "cat", "tac", "pot", "top", "meow", "act" };

            var dictionary = new Dictionary<string, List<string>>();
            IEnumerable<string>[] allGroups =
                validWords.GroupBy(word => String.Concat(word.ToCharArray().OrderBy(c => c)))
                    .Select(grp => grp.Select(w => w).ToArray())
                    .ToArray();

            Assert.IsTrue(allGroups[0].Contains("cat"));
            Assert.IsTrue(allGroups[0].Contains("tac"));
            Assert.IsTrue(allGroups[0].Contains("act"));
            Assert.IsTrue(allGroups[1].Contains("pot"));
            Assert.IsTrue(allGroups[1].Contains("top"));
            Assert.IsTrue(allGroups[2].Contains("meow"));
        }
    }

    public class Program
    {
        private readonly Dictionary<String, HashSet<String>> _dictionary;
        public Program(string[] validWords)
        {
            _dictionary = new Dictionary<String, HashSet<String>>();

            if (validWords != null)
            {
                validWords.ToList().ForEach(validWord =>Insert(validWord));
            }
        }
        
        public void Insert(string theWord)
        {
            var theKey = GenerateHash(theWord);

            //first insert
            if (!_dictionary.ContainsKey(theKey))
            {
                var hashSet = new HashSet<string> {theWord};
                _dictionary.Add(theKey, hashSet);
            }
            else {
                //Key exists; Add to the list of words
                var listOfWords = _dictionary[theKey];

                if (!listOfWords.Contains(theWord)){
                    listOfWords.Add(theWord);
                    _dictionary[theKey] = listOfWords;
                }
            }
        }

        public string[][] GetAllGroups()
        {
            return _dictionary.Select(item => item.Value.ToArray()).ToArray();
        }

        private string GenerateHash(string validWord)
        {
            if (validWord != null)
            {
                return String.Concat(validWord.OrderBy(c => c));
            }

            throw new ArgumentNullException();
        }
    }
}
