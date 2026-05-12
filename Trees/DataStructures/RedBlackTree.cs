
namespace Trees.DataStructures;

public enum NodeColor
{
    Red,
    Black
}

public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node(TKey key, TValue value, NodeColor color)
    {
        public TKey Key { get; set; } = key;
        public TValue Value { get; set; } = value;
        public NodeColor Color { get; set; } = color;
        public Node? Left = null;
        public Node? Right = null;
        public Node? Parent = null;

        public bool IsRed => Color == NodeColor.Red;
        public bool IsBlack => Color == NodeColor.Black;
    }

    private Node? _root = null;
    private readonly IComparer<TKey> _comparer;

    public RedBlackTree() : this(Comparer<TKey>.Default) { }

    public RedBlackTree(IComparer<TKey> comparer)
    {
        _comparer = comparer ?? Comparer<TKey>.Default;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var node = FindNode(key);
        if (node != null)
        {
            value = node.Value;
            return true;
        }

        value = default!;
        return false;
    }

    public void Insert(TKey key, TValue value)
    {
        var newNode = new Node(key, value, NodeColor.Red);
        InsertBST(newNode);
        FixInsert(newNode);
    }

    public bool ContainsKey(TKey key) => FindNode(key) != null;

    private Node? FindNode(TKey key)
    {
        var current = _root;
        while (current != null)
        {
            int cmp = _comparer.Compare(key, current.Key);
            if (cmp == 0)
                return current;
            current = cmp < 0 ? current.Left : current.Right;
        }
        return null;
    }

    private void InsertBST(Node node)
    {
        if (_root == null)
        {
            _root = node;
            return;
        }

        Node? current = _root;
        Node? parent = null;

        while (current != null)
        {
            parent = current;
            int cmp = _comparer.Compare(node.Key, current.Key);
            current = cmp < 0 ? current.Left : current.Right;
        }

        if (parent != null)
        {
            node.Parent = parent;
            if (_comparer.Compare(node.Key, parent.Key) < 0)
                parent.Left = node;
            else
                parent.Right = node;
        }
    }

    private void RotateLeft(Node x)
    {
        Node? y = x.Right;
        x.Right = y?.Left;
        y?.Left?.Parent = x;

        y?.Parent = x.Parent;
        if (x.Parent == null)
            _root = y;
        else if (x == x.Parent.Left)
            x.Parent.Left = y;
        else
            x.Parent.Right = y;

        y?.Left = x;
        x.Parent = y;
    }

    private void RotateRight(Node y)
    {
        Node? x = y.Left;
        y.Left = x?.Right;
        if (x?.Right != null)
            x.Right.Parent = y;

        x?.Parent = y.Parent;
        if (y.Parent == null)
            _root = x;
        else if (y == y.Parent.Left)
            y.Parent.Left = x;
        else
            y.Parent.Right = x;

        x?.Right = y;
        y.Parent = x;
    }

    private void FixInsert(Node z)
    {
        while (z?.Parent != null && z.Parent.IsRed)
        {
            if (z.Parent == z.Parent.Parent?.Left)
            {
                Node? y = z.Parent.Parent.Right;
                if (y != null && y.IsRed)
                {
                    z.Parent.Color = NodeColor.Black;
                    y.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Right)
                    {
                        z = z.Parent;
                        RotateLeft(z);
                    }
                    z?.Parent?.Color = NodeColor.Black;
                    z?.Parent?.Parent?.Color = NodeColor.Red;
                    if (z?.Parent?.Parent != null)
                        RotateRight(z.Parent.Parent);
                }
            }
            else
            {
                // simétrico
                Node y = z.Parent.Parent.Left; // tio
                if (y != null && y.IsRed)
                {
                    z.Parent.Color = NodeColor.Black;
                    y.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Left)
                    {
                        z = z.Parent;
                        RotateRight(z);
                    }
                    z.Parent.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    RotateLeft(z.Parent.Parent);
                }
            }
        }

        _root.Color = NodeColor.Black;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> InOrder()
    {
        if (_root == null) yield break;

        var stack = new Stack<Node>();
        var current = _root;

        while (stack.Count > 0 || current != null)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.Left;
            }

            current = stack.Pop();
            yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
            current = current.Right;
        }
    }

    public bool Remove(TKey key)
    {
        var node = FindNode(key);
        if (node == null)
            return false;

        DeleteNode(node);
        return true;
    }

    private void Transplant(Node u, Node v)
    {
        if (u.Parent == null)
            _root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        if (v != null)
            v.Parent = u.Parent;
    }

    private Node Minimum(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }
    private void DeleteNode(Node z)
    {
        if (z.Left == null)
        {
            Transplant(z, z.Right);
        }
        else if (z.Right == null)
        {
            Transplant(z, z.Left);
        }
        else
        {
            Node y = Minimum(z.Right);
            if (y.Parent != z)
            {
                Transplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }
            Transplant(z, y);
            y.Left = z.Left;
            y.Left.Parent = y;
        }
    }

    public void Print()
    {
        Console.WriteLine("\n=== Árvore Red-Black ===");
        if (_root == null)
        {
            Console.WriteLine("Árvore vazia.");
            return;
        }
        PrintLevel(_root);
        Console.WriteLine();
    }

    private void PrintLevel(Node? root)
    {
        if (root == null) return;

        Queue<(Node? node, int nivel)> fila = new Queue<(Node?, int)>();
        fila.Enqueue((root, 0));
        int nivelAtual = -1;

        while (fila.Count > 0)
        {
            var (atual, nivel) = fila.Dequeue();

            if (nivel > nivelAtual)
            {
                if (nivelAtual != -1) Console.WriteLine();
                Console.Write($"Nível {nivel}: ");
                nivelAtual = nivel;
            }

            if (atual != null)
            {
                string cor = atual.IsRed ? "R" : "B";
                Console.Write($"[{atual.Key}:{cor}] ");

                fila.Enqueue((atual.Left, nivel + 1));
                fila.Enqueue((atual.Right, nivel + 1));
            }
            else
            {
                Console.Write("[null] ");
            }
        }
        Console.WriteLine();
    }
}

