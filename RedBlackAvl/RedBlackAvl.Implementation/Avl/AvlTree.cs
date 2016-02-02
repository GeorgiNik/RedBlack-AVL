namespace RedBlackAvl.Implementation.Avl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class AvlTree<TKey, TValue> : IEnumerable<TValue>
    {
        public IEnumerator<TValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class AvlNode
        {
            public AvlNode Parent { get; set; }

            public AvlNode Left { get; set; }

            public AvlNode Right { get; set; }

            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public int Balance { get; set; }
        }
    }
}