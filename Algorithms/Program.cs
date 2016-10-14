using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class Program
    {
        static List<string> theArray;
        static int arraySize;
        static int itemsInArray = 0;
        static int collisionCount;
        static void Main(string[] args)
        {
            var theFunc = HashFunction(31);
            //List<string> elementsToAdd = new List<string>{ "1", "5", "17", "21", "26" };
            //hashFunction1(elementsToAdd, theArray);
            List<string> elementsToAdd = new List<string> { "100", "510", "170", "214", "268", "398",
                "235", "802", "900", "723", "699", "1", "16", "999", "890",
                "725", "998", "978", "988", "990", "989", "984", "320", "321",
                "400", "415", "450", "50", "660", "624" };
            //hashFunction2(elementsToAdd, theArray);
            doubleHashFunction2(elementsToAdd, theArray);
            //increaseArraySize(60);
            //findKey("660");
            displayTheHashTable(theArray);
        }
        public static List<string> HashFunction(int size)
        {
            arraySize = size;
            theArray = new List<string>(size);
            theArray.AddRange(Enumerable.Repeat("-1", size));
            return theArray;
        }

        public static void hashFunction1(List<string> stringsForArray, List<string> theArray)
        {
            for (int n = 0; n < stringsForArray.Count; n++)
            {
                string newElementVal = stringsForArray[n];
                theArray[int.Parse(newElementVal)] = newElementVal;
            }
        }

        public static void hashFunction2(List<string> stringsForArray, List<string> theArray)
        {
            for (int n = 0; n < stringsForArray.Count; n++)
            {
                string newElementVal = stringsForArray[n];
                int arrayIndex = int.Parse(newElementVal) % (theArray.Count-1);
                Console.WriteLine($"Mod index = {arrayIndex} for value {newElementVal}");
                while(theArray[arrayIndex] != "-1")
                {
                    ++arrayIndex;
                    Console.WriteLine($"Collision! Try {arrayIndex} instead");
                    collisionCount++;
                    arrayIndex %= arraySize;
                }
                if(arrayIndex != int.Parse(newElementVal) % (theArray.Count - 1))
                {
                    Console.WriteLine($"Mod index = {arrayIndex} for value {newElementVal}");
                }
                theArray[arrayIndex] = newElementVal;
            }
        }

        public static void doubleHashFunction2(List<string> stringsForArray, List<string> theArray)
        {
            for (int n = 0; n < stringsForArray.Count; n++)
            {
                string newElementVal = stringsForArray[n];
                int arrayIndex = int.Parse(newElementVal) % (theArray.Count - 1);
                int stepDistance = 7 - (int.Parse(newElementVal) % 7);
                Console.WriteLine($"Mod index = {arrayIndex} for value {newElementVal}");
                while (theArray[arrayIndex] != "-1")
                {
                    arrayIndex += stepDistance;
                    Console.WriteLine($"Collision! Try {arrayIndex} instead");
                    collisionCount++;
                    arrayIndex %= arraySize;
                }
                if (arrayIndex != int.Parse(newElementVal) % (theArray.Count - 1))
                {
                    Console.WriteLine($"Mod index = {arrayIndex} for value {newElementVal}");
                }
                theArray[arrayIndex] = newElementVal;
            }
        }

        public static string findKey(string key)
        {
            int arrayIndexHash = int.Parse(key) % (theArray.Count - 1);
            while(theArray[arrayIndexHash] != "-1")
            {
                if(theArray[arrayIndexHash] == key)
                {
                    Console.WriteLine($"{key} was found in index {arrayIndexHash}");
                    return theArray[arrayIndexHash];
                }
                ++arrayIndexHash;
                arrayIndexHash %= arraySize;
            }
            return null;
        }

        public static string findKeyDblHash(string key)
        {
            int arrayIndexHash = int.Parse(key) % (theArray.Count - 1);
            int stepDistance = 7 - (int.Parse(key) % 7);
            while (theArray[arrayIndexHash] != "-1")
            {
                if (theArray[arrayIndexHash] == key)
                {
                    Console.WriteLine($"{key} was found in index {arrayIndexHash}");
                    return theArray[arrayIndexHash];
                }
                arrayIndexHash += stepDistance;
                arrayIndexHash %= arraySize;
            }
            return null;
        }

        public static void displayTheHashTable(List<string> theArray)
        {
            foreach (var s in theArray)
            {
                Console.Write(s+", ");
            }
            Console.WriteLine($"\nCollision Count: {collisionCount}");
        }

        public static bool isPrime(int number)
        {
            if(number % 2 == 0)
            {
                return false;
            }
            for (int i = 3; i*i <= number; i+=2)
            {
                if(number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int getNextPrime(int minNumberToCheck)
        {
            for (int i = minNumberToCheck; true; i++)
            {
                if (isPrime(i))
                {
                    return i;
                }
            }
        }

        public static void increaseArraySize(int minArraySize)
        {
            int newArraySize = getNextPrime(minArraySize);
            moveOldArray(newArraySize);
        }

        public static void moveOldArray(int newArraySize)
        {
            List<string> cleanArray = removeEmptySpacesInArray(theArray);
            theArray = new List<string>(newArraySize);
            arraySize = newArraySize;
            theArray.AddRange(Enumerable.Repeat("-1", newArraySize));
            hashFunction2(cleanArray, theArray);
        }

        public static List<string> removeEmptySpacesInArray(List<string> arrayToClean)
        {
            List<string> stringList = new List<string>();
            foreach(string theString in arrayToClean)
            {
                if(!String.Equals(theString,"-1"))
                {
                    stringList.Add(theString);
                }
            }

            return arrayToClean;
        }
    }
}
