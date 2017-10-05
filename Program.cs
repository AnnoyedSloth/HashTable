using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
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
        public Node _head;
        public Node _cur;
        public Node _tail;
        public List()
        {
            _head = null;
            _cur = null;
            _tail = null;
        }
    }
#endregion

    class Program
    {
#region List Methods
        static bool InsertNode(List list, string key, string value)
        {
            Node newNode = new Node(key, value);

            newNode.next = null;

            // in case of empty list
            if(list._head == null)
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
            return true;
        }

        static bool LFirst(List list)
        {
            if(list._head == null)
            {
                Console.WriteLine("Empty list!");
                return false;
            }
            list._cur = list._head;
            return true;
        }

        static bool LNext(List list)
        {
            if(list._cur.next == null)
            {
                return false;
            }
            list._cur = list._cur.next;
            return true;
        }
#endregion

        static void Main(string[] args)
        {
            List mylist = new List();

            InsertNode(mylist, "Hi", "Hello");
            InsertNode(mylist, "Hi2", "Hello2");
            InsertNode(mylist, "Hi3", "Hello3");
            InsertNode(mylist, "Hi4", "Hello4");

            if (LFirst(mylist))
            {
                Console.WriteLine($"{mylist._cur.GetValue()}");
                while (LNext(mylist))
                {
                    Console.WriteLine($"{mylist._cur.GetValue()}");
                }
            }
        }
    }
}