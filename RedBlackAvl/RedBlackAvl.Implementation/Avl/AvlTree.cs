namespace RedBlackAvl.Implementation.Avl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using RedBlackAvl.Implementation.Contracts;

    public class AvlTree<TKey, TValue> : IAvlTree<TKey, TValue>
    {
        private readonly IComparer<TKey> comparer;

        public AvlTree(IComparer<TKey> comparer)
        {
            this.comparer = comparer;
        }

        public AvlTree()
            : this(Comparer<TKey>.Default)
        {
        }

        public IAvlNode<TKey, TValue> Root { get; set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return new AvlNodeEnumerator<TKey, TValue>((AvlNode<TKey, TValue>)this.Root);
        }

        public bool Search(TKey key, out TValue value)
        {
            var node = this.Root;

            while (node != null)
            {
                if (this.comparer.Compare(key, node.Key) < 0)
                {
                    node = (AvlNode<TKey, TValue>)node.Left;
                }
                else if (this.comparer.Compare(key, node.Key) > 0)
                {
                    node = (AvlNode<TKey, TValue>)node.Right;
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
            if (this.Root == null)
            {
                this.Root = new AvlNode<TKey, TValue> { Key = key, Value = value };
            }
            else
            {
                var node = this.Root;

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
                        node = (AvlNode<TKey, TValue>)left;
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
                        node = (AvlNode<TKey, TValue>)right;
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

        public void InsertBalance(IAvlNode<TKey, TValue> node, int balance)
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
            var node = this.Root;

            while (node != null)
            {
                if (this.comparer.Compare(key, node.Key) < 0)
                {
                    node = (AvlNode<TKey, TValue>)node.Left;
                }
                else if (this.comparer.Compare(key, node.Key) > 0)
                {
                    node = (AvlNode<TKey, TValue>)node.Right;
                }
                else
                {
                    var left = node.Left;
                    var right = node.Right;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == this.Root)
                            {
                                this.Root = null;
                            }
                            else
                            {
                                var parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    this.DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    this.DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            this.Replace(node, right);

                            this.DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        this.Replace(node, left);

                        this.DeleteBalance(node, 0);
                    }
                    else
                    {
                        var successor = right;

                        if (successor.Left == null)
                        {
                            var parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.Root)
                            {
                                this.Root = (AvlNode<TKey, TValue>)successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            this.DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            var parent = node.Parent;
                            var successorParent = successor.Parent;
                            var successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.Root)
                            {
                                this.Root = (AvlNode<TKey, TValue>)successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            this.DeleteBalance(successorParent, -1);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public void DeleteBalance(IAvlNode<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = this.RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = this.RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                var parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        public IAvlNode<TKey, TValue> RotateRight(IAvlNode<TKey, TValue> node)
        {
            var left = node.Left;
            var leftRight = left.Right;
            var parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if (leftRight != null)
            {
                leftRight.Parent = node;
            }

            if (node == this.Root)
            {
                this.Root = (AvlNode<TKey, TValue>)left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;
        }

        public IAvlNode<TKey, TValue> RotateLeftRight(IAvlNode<TKey, TValue> node)
        {
            var left = node.Left;
            var leftRight = left.Right;
            var parent = node.Parent;
            var leftRightRight = leftRight.Right;
            var leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }

            if (node == this.Root)
            {
                this.Root = (AvlNode<TKey, TValue>)leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;
        }

        public IAvlNode<TKey, TValue> RotateRightLeft(IAvlNode<TKey, TValue> node)
        {
            var right = node.Right;
            var rightLeft = right.Left;
            var parent = node.Parent;
            var rightLeftLeft = rightLeft.Left;
            var rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if (node == this.Root)
            {
                this.Root = (AvlNode<TKey, TValue>)rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        public IAvlNode<TKey, TValue> RotateLeft(IAvlNode<TKey, TValue> node)
        {
            var right = node.Right;
            var rightLeft = right.Left;
            var parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == this.Root)
            {
                this.Root = (AvlNode<TKey, TValue>)right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private void Replace(IAvlNode<TKey, TValue> target, IAvlNode<TKey, TValue> source)
        {
            var left = source.Left;
            var right = source.Right;

            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }
    }
}