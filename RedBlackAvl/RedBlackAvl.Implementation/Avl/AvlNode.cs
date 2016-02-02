namespace RedBlackAvl.Implementation.Avl
{
    using RedBlackAvl.Implementation.Contracts;

    public class AvlNode<TKey, TValue> : IAvlNode<TKey, TValue>
    {
        public IAvlNode<TKey, TValue> Parent { get; set; }

        public IAvlNode<TKey, TValue> Left { get; set; }

        public IAvlNode<TKey, TValue> Right { get; set; }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public int Balance { get; set; }
    }
}