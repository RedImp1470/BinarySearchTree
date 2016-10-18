using System;
using System.Diagnostics;
using System.Linq;


namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            const int min = 0;
            const int max = 100;
            var tree = new BinarySearchTree<int>();
            var rnd = new int[10];

            Node<int> root = null;

            var rndNo = new Random();
            for (var i = 0; i < rnd.Length; i++)
            {
                rnd[i] = rndNo.Next(min, max);
            }

            foreach (var r in rnd)
            {
                root = tree.InsertNode(root, r);
            }

            Console.WriteLine("input array: ");
            Console.WriteLine(string.Join(",", rnd));
            Console.ReadLine();
            
            var values = tree.PreorderTraverseTree(root).ToList();
            Console.WriteLine("the trees preorder traversal: ");
            Console.WriteLine(string.Join(",", values));
            Console.ReadLine();

            var rndItem = rnd[rndNo.Next(0, rnd.Length)];
            Console.WriteLine(rndItem + " will be deleted from tree");
            Console.ReadLine();
            tree.DeleteNode(ref root, rndItem);
           
            var restTree = tree.PreorderTraverseTree(root).ToList();
            Console.WriteLine("tree after deletion: ");
            Console.WriteLine(string.Join(",", restTree));
            Console.ReadLine();
        }
    }
}
