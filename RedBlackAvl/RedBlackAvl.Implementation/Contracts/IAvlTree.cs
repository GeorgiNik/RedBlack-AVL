namespace RedBlackAvl.Implementation.Contracts
{
    using System.Collections.Generic;

    using RedBlackAvl.Implementation.Avl;

    public interface IAvlTree<TKey,TValue>:IEnumerable<TValue>
    {
        bool Search(TKey key, out TValue value);
        void Clear();

        void Insert(TKey key, TValue value);

        void InsertBalance(AvlNode<TKey, TValue> node, int balance);

        bool Delete(TKey key);

        void DeleteBalance(AvlNode<TKey,TValue> node, int balance);

        AvlNode<TKey,TValue> RotateRight(AvlNode<TKey,TValue> node);

        AvlNode<TKey,TValue> RotateLeftRight(AvlNode<TKey,TValue> node);

        AvlNode<TKey,TValue> RotateRightLeft(AvlNode<TKey,TValue> node);

        AvlNode<TKey, TValue> RotateLeft(AvlNode<TKey, TValue> node);
        
    }
}