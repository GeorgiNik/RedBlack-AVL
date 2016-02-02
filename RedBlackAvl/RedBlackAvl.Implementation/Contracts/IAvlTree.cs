namespace RedBlackAvl.Implementation.Contracts
{
    using System.Collections.Generic;

    public interface IAvlTree<TKey, TValue> : IEnumerable<TValue>
    {
        IAvlNode<TKey, TValue> Root { get; set; }

        bool Search(TKey key, out TValue value);

        void Clear();

        void Insert(TKey key, TValue value);

        void InsertBalance(IAvlNode<TKey, TValue> node, int balance);

        bool Delete(TKey key);

        void DeleteBalance(IAvlNode<TKey, TValue> node, int balance);

        IAvlNode<TKey, TValue> RotateRight(IAvlNode<TKey, TValue> node);

        IAvlNode<TKey, TValue> RotateLeftRight(IAvlNode<TKey, TValue> node);

        IAvlNode<TKey, TValue> RotateRightLeft(IAvlNode<TKey, TValue> node);

        IAvlNode<TKey, TValue> RotateLeft(IAvlNode<TKey, TValue> node);
    }
}