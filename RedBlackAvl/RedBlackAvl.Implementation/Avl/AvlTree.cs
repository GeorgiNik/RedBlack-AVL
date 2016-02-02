namespace RedBlackAvl.Implementation.Avl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using RedBlackAvl.Implementation.Contracts;

    public class AvlTree<TKey, TValue> : IAvlTree<TKey, TValue>
    {
        private readonly IComparer<TKey> comparer;

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
            var node = this.root;

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
            if (this.root == null)
            {
                this.root = new AvlNode<TKey, TValue> { Key = key, Value = value };
            }
            else
            {
                var node = this.root;

                while (node != null)
                {
                    var compare = this.comparer.Compare(key, node.Key);

                    if (compare < 0)
                    {
                        var left = node.Left;

                        if (left == null)
                        {
                            node.Left = new AvlNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            this.InsertBalance(node, 1);

                            return;
                        }
                        node = left;
                    }
                    else if (compare > 0)
                    {
                        var right = node.Right;

                        if (right == null)
                        {
                            node.Right = new AvlNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            this.InsertBalance(node, -1);

                            return;
                        }
                        node = right;
                    }
                    else
                    {
                        node.Value = value;

                        return;
                    }
                }
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void InsertBalance(AvlNode<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        this.RotateRight(node);
                    }
                    else
                    {
                        this.RotateLeftRight(node);
                    }

                    return;
                }
                if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        this.RotateLeft(node);
                    }
                    else
                    {
                        this.RotateRightLeft(node);
                    }

                    return;
                }

                var parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        public bool Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public void DeleteBalance(AvlNode<TKey, TValue> node, int balance)
        {
            throw new NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateRight(AvlNode<TKey, TValue> node)
        {
            throw new NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateLeftRight(AvlNode<TKey, TValue> node)
        {
            throw new NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateRightLeft(AvlNode<TKey, TValue> node)
        {
            throw new NotImplementedException();
        }

        public AvlNode<TKey, TValue> RotateLeft(AvlNode<TKey, TValue> node)
        {
            throw new NotImplementedException();
        }

        public void Replace(AvlNode<TKey, TValue> target, AvlNode<TKey, TValue> source)
        {
            throw new NotImplementedException();
        }
    }
}