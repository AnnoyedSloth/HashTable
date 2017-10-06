using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableNS
{
    #region List Classes
    public class Node
    {
        private string _key;
        private string _value;
        public Node _next;
        public Node(string key, string value)
        {
            _key = key;
            _value = value;
        }
        public string GetValue()
        {
            return _value;
        }
        public string GetKey()
        {
            return _key;
        }
    }

    public class List
    {
        private int _count;
        public Node _head;
        public Node _cur;
        public Node _tail;
        public Node _before;
        public List()
        {
            _count = 0;
            _head = null;
            _cur = null;
            _tail = null;
            _before = null;
        }
        public int Count
        {
            get { return _count; }
            set { ++_count; }
        }
    }
    #endregion

    #region HashTable Classes
    class HashTable
    {
        private List[] _hashList = new List[100];

        public bool HashInsert()
        {
            string key, value;

            Console.WriteLine("You've chose the Insert option.");
            Console.Write("Key : ");
            key = Console.ReadLine();
            Console.Write("Value : ");
            value = Console.ReadLine();

            int hashkey = Hashing(key);

            // if _hashList is null then call constructor
            if (_hashList[hashkey] == null)
            {
                _hashList[hashkey] = new List();

            }
            else
            {
                //Checking amount of connected lists
                if (_hashList[hashkey].Count < 5) hashkey = Hashing(key);
                else
                {
                    hashkey = ReHashing(key);
                    if (_hashList[hashkey] == null) _hashList[hashkey] = new List();
                    else
                    {
                        //Preventing another hashing which already hashed twice
                        if (_hashList[hashkey].Count > 4)
                        {
                            Console.WriteLine("This address is already hashed twice. Therefore more hashing would be not available.");
                            return false;
                        }
                    }
                }
            }
            Program.InsertNode(_hashList[hashkey], key, value);

            return true;
        }

        //Overloaded Method(to make testing procedure more easier)
        public bool Insert(string key, string value)
        {

            int hashkey = Hashing(key);

            // if _hashList is null then call constructor
            if (_hashList[hashkey] == null)
            {
                _hashList[hashkey] = new List();

            }
            else
            {
                //Checking amount of connected lists
                if (_hashList[hashkey].Count < 5) hashkey = Hashing(key);
                else
                {
                    hashkey = ReHashing(key);
                    if (_hashList[hashkey] == null) _hashList[hashkey] = new List();
                    else
                    {
                        //Preventing another hashing which already hashed twice
                        if (_hashList[hashkey].Count > 4)
                        {
                            return false;
                        }
                    }
                }
            }
            Program.InsertNode(_hashList[hashkey], key, value);

            return true;
        }

        public void DeleteHashData()
        {
            Console.Write("You've chose the Deleting option. Please write key to delete : ");
            string delKey = Console.ReadLine();
            for (int a = 0; a < 100; a++)
            {
                if (!(_hashList[a] == null))
                {
                    if (Program.LFirst(_hashList[a]))
                    {
                        if (_hashList[a]._cur.GetKey() == delKey)
                        {
                            Console.WriteLine($"Value '{Program.RemoveNode(_hashList[a])}' has been deleted");
                        }
                        while (Program.LNext(_hashList[a]))
                        {
                            if (_hashList[a]._cur.GetKey() == delKey)
                            {
                                Console.WriteLine($"Value '{Program.RemoveNode(_hashList[a])}' has been deleted");
                            }
                        }
                    }
                }
            }
        }

        public int Hashing(string key)
        {
            int hashKey;
            hashKey = key[0] % 100;
            return hashKey;
        }

        public int ReHashing(string key)
        {
            int hashKey;
            hashKey = key[1] % 100;
            return hashKey;
        }

        public void PrintHashtable()
        {
            for (int a = 0; a < 100; a++)
            {
                if (!(_hashList[a] == null))
                {
                    Console.Write($"Hashcode[{a}] ");
                    Program.ListPrint(_hashList[a]);
                }
            }
        }
        #endregion

        class Program
        {
            #region List Methods
            public static bool InsertNode(List list, string key, string value)
            {
                Node newNode = new Node(key, value);

                newNode._next = null;

                // in case of empty list
                if (list._head == null)
                {
                    list._head = newNode;
                    list._tail = newNode;
                }
                // if there is one or more node exist, then put new node next of the tail.
                else
                {
                    list._tail._next = newNode;
                    list._tail = newNode;
                }
                list.Count++; // adding each time when List inserted
                return true;
            }

            public static string RemoveNode(List list)
            {
                string tempValue = null;
                // in case of empty list
                if (list._head == null)
                {
                    return null;
                }

                // in case of only 1 node exist
                if (list._head._next == null)
                {
                    Console.WriteLine("1 node case called");
                    tempValue = list._head.GetValue();
                    list._head = null;
                    list._cur = null;
                    list._tail = null;
                    list._before = null;
                    list = null;
                    return tempValue;
                }
                // in case of list has 2 or more nodes
                else
                {
                    // in case of deleting node is neither head or tail 
                    if (list._cur != list._head && list._cur != list._tail)
                    {
                        tempValue = list._cur.GetValue();
                        list._before._next = list._cur._next;
                        list._cur = list._before;
                        return tempValue;
                    }
                    // in case of deleting node is head
                    else if (list._cur == list._head && list._cur != list._tail)
                    {
                        tempValue = list._cur.GetValue();
                        list._head = list._cur._next;
                        list._cur = list._head;
                        return tempValue;
                    }
                    // in case of deleting node is tail
                    else if (list._cur != list._head && list._cur == list._tail)
                    {
                        tempValue = list._cur.GetValue();
                        list._tail = list._before;
                        list._cur = list._tail;
                        list._tail._next = null;
                        return tempValue;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public static void ListPrint(List list)
            {
                if (LFirst(list))
                {
                    Console.Write($"[{list.Count}]Lists ");
                    Console.Write($"{list._cur.GetValue()} ");
                    while (LNext(list))
                    {
                        Console.Write($"{list._cur.GetValue()} ");

                    }
                    Console.WriteLine();
                }
            }

            public static bool LFirst(List list)
            {
                if (list._head == null)
                {
                    //Console.WriteLine("Empty list!");
                    return false;
                }
                list._before = null;
                list._cur = list._head;
                return true;
            }

            public static bool LNext(List list)
            {
                if (list._cur._next == null)
                {
                    return false;
                }
                list._before = list._cur;
                list._cur = list._cur._next;
                return true;
            }
            #endregion


            static void Main(string[] args)
            {
                HashTable HT = new HashTable();

                HT.Insert("Hi1", "1");
                HT.Insert("Hi2", "2");
                HT.Insert("Hi3", "3");
                HT.Insert("Hi4", "4");
                HT.Insert("Hi5", "5");
                HT.Insert("Hi6", "6");
                HT.Insert("Hi7", "7");
                HT.Insert("Hi8", "8");
                HT.Insert("Hi9", "9");
                HT.Insert("Hi10", "10");
                HT.Insert("Hi11", "11");

                while (true)
                {
                    int opt = 0;
                    Console.Write("1.PrintAll 2.Add 3.Delete 4.Search 5.Exit : ");

                    try
                    {
                        opt = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Write your command properly please.");
                        continue;
                    }

                    switch (opt)
                    {
                        case 1:
                            HT.PrintHashtable();
                            break;
                        case 2:
                            HT.HashInsert();
                            break;
                        case 3:
                            HT.DeleteHashData();
                            break;
                        case 5:
                            return;
                            break;
                        default:
                            Console.WriteLine("You've got to choose something!");
                            break;
                    }
                }
            }
        }
    }
}