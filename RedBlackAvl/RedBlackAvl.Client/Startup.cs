namespace RedBlackAvl.Client
{
    using System;
    using System.Collections.Generic;

    using RedBlackAvl.Common;
    using RedBlackAvl.Implementation.Avl;

    public class Startup
    {
        static readonly AvlTree<int, string> avlTree = new AvlTree<int, string>();

        static void Main()
        {
            try
            {
                Console.WriteLine("Adding items to AVL");
                avlTree.Insert(1, "One");
                avlTree.Insert(2, "Two");
                avlTree.Insert(3, "Three");
                avlTree.Insert(4, "Four");
                avlTree.Insert(5, "Five");
                avlTree.Insert(6, "Six");
                avlTree.Insert(7, "Seven");
                avlTree.Insert(8, "Eight");
                avlTree.Insert(9, "Nine");
                avlTree.Insert(10, "Ten");
                avlTree.Insert(11, "Eleven");
                avlTree.Insert(12, "Twelve");
                avlTree.Insert(13, "Thirteen");

                Helpers.Traversing();
                TraverseAvl(avlTree);
                Search(9);

                Helpers.Removing(9);
                avlTree.Delete(9);
                
                TraverseAvl(avlTree);
                Search(9);

                Helpers.Removing(13);
                avlTree.Delete(13);
                Helpers.Traversing();
                TraverseAvl(avlTree);

                Helpers.Removing(12);
                avlTree.Delete(12);
                Helpers.Traversing();
                TraverseAvl(avlTree);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();
            }
        }

        private static void Search(int keyToBeSearched)
        {
            string value;
            if (avlTree.Search(keyToBeSearched, out value))
            {
                Console.WriteLine("{0} was found", value);
            }
            else
            {
                Console.WriteLine("{0} wasn't found", keyToBeSearched);
            }
        }

        private static void TraverseAvl(AvlTree<int, string> avlTree)
        {
            // Depth-first traversal  

            var stack = new Stack<AvlNode<int, string>>();
            stack.Push(avlTree.root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                IComparable parentKey = null;
                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Parent != null)
                {
                    parentKey = node.Parent.Key;
                }

                PrintAvlNode(node, parentKey);
            }
        }

        private static void PrintAvlNode(AvlNode<int, string> node, IComparable parentKey)
        {
            Console.WriteLine(
                "Key:{0}\t" + "Data:{1}\t" + "Parent Key:{2}\t" + "Balance:{3}",
                node.Key,
                node.Value,
                parentKey,
                node.Balance);
        }
    }
}