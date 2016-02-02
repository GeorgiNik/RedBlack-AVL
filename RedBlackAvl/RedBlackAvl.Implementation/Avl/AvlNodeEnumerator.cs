namespace RedBlackAvl.Implementation.Avl
{
    using System.Collections;
    using System.Collections.Generic;

    using RedBlackAvl.Common;

    public class AvlNodeEnumerator<TKey, TValue> : IEnumerator<TValue>
    {
        private readonly AvlNode<TKey, TValue> root;

        private Action action;

        private AvlNode<TKey, TValue> current;

        private AvlNode<TKey, TValue> right;

        public AvlNodeEnumerator(AvlNode<TKey, TValue> root)
        {
            this.right = this.root = root;

            this.action = root == null ? Action.End : Action.Right;
        }

        public bool MoveNext()
        {
            switch (this.action)
            {
                case Action.Right:
                    this.current = this.right;

                    while (this.current.Left != null)
                    {
                        this.current = this.current.Left;
                    }

                    this.right = this.current.Right;

                    this.action = this.right != null ? Action.Right : Action.Parent;

                    return true;
                case Action.Parent:
                    while (this.current.Parent != null)
                    {
                        var previous = this.current;

                        this.current = this.current.Parent;

                        if (this.current.Left == previous)
                        {
                            this.right = this.current.Right;

                            this.action = this.right != null ? Action.Right : Action.Parent;

                            return true;
                        }
                    }

                    this.action = Action.End;

                    return false;
                default:
                    return false;
            }
        }

        public void Reset()
        {
            this.right = this.root;

            this.action = this.root == null ? Action.End : Action.Right;
        }

        public TValue Current
        {
            get
            {
                return this.current.Value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public void Dispose()
        {
        }
    }
}