namespace RedBlackAvl.Implementation.Avl
{
    public class AvlNode<TKey, TValue>
    {
        public AvlNode<TKey, TValue> Parent { get; set; }

        public AvlNode<TKey, TValue> Left { get; set; }

        public AvlNode<TKey, TValue> Right { get; set; }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public int Balance { get; set; }
    }
}