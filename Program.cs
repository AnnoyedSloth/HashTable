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
        public Node next;
        public Node(string key, string value)
        {
            _key = key;
            _value = value;
        }
        public string GetValue()
        {
            return _value;
        }
    }

    public class List
    {
        private int _count;
        public Node _head;
        public Node _cur;
        public Node _tail;
        public List()
        {
            _count = 0;
            _head = null;
            _cur = null;
            _tail = null;
        }
        public int Count
        {
            get { return _count; }
            set { ++_count; }
        }
    }
    #endregion

    class HashTable
    {
        private List[] _hashList = new List[100];

        public bool Insert(string key, string value)
        {
            int hashkey = Hashing(key);
            if (_hashList[hashkey] == null) _hashList[hashkey] = new List(); // if _hashList is null then execute constructor
            Program.InsertNode(_hashList[hashkey], key, value);

            return true;
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

        class Program
        {
            #region List Methods
            public static bool InsertNode(List list, string key, string value)
            {
                Node newNode = new Node(key, value);

                newNode.next = null;

                // in case of empty list
                if (list._head == null)
                {
                    list._head = newNode;
                    list._tail = newNode;
                }
                // if there is one or more node exist, then put new node next of the tail.
                else
                {
                    list._tail.next = newNode;
                    list._tail = newNode;
                }
                list.Count++; // adding each time when List inserted
                return true;
            }

            public static void ListPrint(List list)
            {
                if (LFirst(list))
                {
                    Console.Write($"{list.Count} ");
                    Console.Write($"{list._cur.GetValue()} ");
                    while (LNext(list))
                    {
                        Console.Write($"{list._cur.GetValue()} ");
                    }
                    Console.WriteLine();
                }
            }

            static bool LFirst(List list)
            {
                if (list._head == null)
                {
                    //Console.WriteLine("Empty list!");
                    return false;
                }
                list._cur = list._head;
                return true;
            }

            static bool LNext(List list)
            {
                if (list._cur.next == null)
                {
                    return false;
                }
                list._cur = list._cur.next;
                return true;
            }
            #endregion

            static void Main(string[] args)
            {
                HashTable HT = new HashTable();

                HT.Insert("abc", "1");
                HT.Insert("abc", "2");
                HT.Insert("abc", "3");
                HT.Insert("abc", "4");
                HT.Insert("abc", "5");
                HT.Insert("bbc", "6");
                HT.Insert("cbc", "7");
                HT.Insert("ebc", "8");

                HT.PrintHashtable();
            }
        }
    }
}