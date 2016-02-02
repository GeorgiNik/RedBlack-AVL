namespace RedBlackAvl.Implementation.Avl
{
    using System.Collections;
    using System.Collections.Generic;

    public class AvlTree<TKey, TValue> : IEnumerable<TValue>
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
    }
}