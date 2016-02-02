namespace RedBlackAvl.Implementation.Avl
{
    using System.Collections;
    using System.Collections.Generic;

    using RedBlackAvl.Implementation.Contracts;

    public class AvlTree<TKey, TValue> : IAvlTree<TKey,TValue>
    {
        private IComparer<TKey> comparer;

        private AvlNode<TKey, TValue> root;

        public AvlTree(IComparer<TKey> comparer)
        {
            this.comparer = comparer;
        }

        public AvlTree()
            : this(Comparer<TKey>.Default)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return new AvlNodeEnumerator<TKey, TValue>(this.root);
        }

        public bool Search(TKey key, out TValue value)
        {
            AvlNode<TKey, TValue> node = this.root;

            while (node != null)
            {
                if (this.comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (this.comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;

                    return true;
                }
            }

            value = default(TValue);

            return false;
        }

        public void Insert(TKey key, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void InsertBalance(AvlNode<TKey, TValue> node, int balance)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBalance(AvlNode<TKey, TValue> node, int balance)
        {
            throw new System.NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateRight(AvlNode<TKey, TValue> node)
        {
            throw new System.NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateLeftRight(AvlNode<TKey, TValue> node)
        {
            throw new System.NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateRightLeft(AvlNode<TKey, TValue> node)
        {
            throw new System.NotImplementedException();
        }

        public void Replace(AvlNode<TKey, TValue> target, AvlNode<TKey, TValue> source)
        {
            throw new System.NotImplementedException();
        }
    }
}