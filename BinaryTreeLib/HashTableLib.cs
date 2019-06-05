
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace HashTableLib
{
    public class MyHashTable
    {
        private readonly int _tableSize;
        public Dictionary<int, List<string>> LookpUp { get; set; }

        public MyHashTable(int tableSize = 1000)
        {
            _tableSize = tableSize;
            LookpUp = new Dictionary<int, List<string>>();
        }

        public void Set(string itemKey, string itemValue)
        {
            int hashCode = Hash.GenerateHash(itemKey, _tableSize);

            if (LookpUp.ContainsKey(hashCode))
            {
                LookpUp[hashCode].Add(itemValue);
            }
            else
            {
                LookpUp.Add(hashCode, new List<string>() {itemValue});
            }
        }

        public string GetFirst(string itemKey)
        {
            int hashCode = Hash.GenerateHash(itemKey, _tableSize);

            if (LookpUp.ContainsKey(hashCode))
            {
                return LookpUp[hashCode].FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public List<string> GetAll(string itemKey)
        {
            int hashCode = Hash.GenerateHash(itemKey, _tableSize);

            if (LookpUp.ContainsKey(hashCode))
            {
                return LookpUp[hashCode];
            }
            else
            {
                return null;
            }
        }

    }

    public class Hash
    {
        public static int GenerateHash(string key, int upperBoundSize)
        {
            int positiveIntA = 1;
            int positiveIntB = 2;
            int largePrimeNumber = 1610612741;
            
            //int sumOfKeys = key.Sum(part => Convert.ToInt32(sumOfKeys));
            //((positiveIntA * hashCode + positiveIntB) % LargePrimeNumber) % UpperBoundSize

            int sum = key.Sum(charsInKey => charsInKey);

            return ((positiveIntA * sum + positiveIntB) % largePrimeNumber) % upperBoundSize;
        }
    }
}
