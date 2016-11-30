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
                tree.InsertNode(r);
            }

            Console.WriteLine("input array: ");
            Console.WriteLine(string.Join(",", rnd));
            Console.ReadLine();
            
            var values = tree.PreorderTraverseTree().ToList();
            Console.WriteLine("the trees preorder traversal: ");
            Console.WriteLine(string.Join(",", values));
            Console.ReadLine();

            var vals = tree.InOrderTraverseTree().ToList();
            Console.WriteLine("the trees inorder traversal: ");
            Console.WriteLine(string.Join(",", vals));
            Console.ReadLine();

            var rndItem = rnd[rndNo.Next(0, rnd.Length)];
            Console.WriteLine(rndItem + " will be deleted from tree");
            Console.ReadLine();
            tree.DeleteNode(rndItem);
           
            var restTree = tree.PreorderTraverseTree().ToList();
            Console.WriteLine("tree after deletion: ");
            Console.WriteLine(string.Join(",", restTree));
            Console.ReadLine();

            var treeContent = tree.PreorderTraverseTree().ToList();
            var rndItem1 = treeContent[2];
            Console.WriteLine("try to find: " + rndItem1);
            var test = tree.FindNode(rndItem1);
            Console.WriteLine("Found: "+test);
            Console.ReadLine();

            Console.WriteLine("Balancing tree now");
            var balancedTree = tree.BalancedTree();

            var balancedPreOrder = balancedTree.PreorderTraverseTree().ToList();
            Console.WriteLine("preorder traversal of balanced tree: ");
            Console.WriteLine(string.Join(",", balancedPreOrder));
            Console.ReadLine();

        }
    }
}
