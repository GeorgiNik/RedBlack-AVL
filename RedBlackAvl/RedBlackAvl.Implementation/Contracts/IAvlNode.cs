namespace RedBlackAvl.Implementation.Contracts
{
    public interface IAvlNode<TKey, TValue>
    {
        IAvlNode<TKey, TValue> Parent { get; set; }

        IAvlNode<TKey, TValue> Left { get; set; }

        IAvlNode<TKey, TValue> Right { get; set; }

        TKey Key { get; set; }

        TValue Value { get; set; }

        int Balance { get; set; }
    }
}