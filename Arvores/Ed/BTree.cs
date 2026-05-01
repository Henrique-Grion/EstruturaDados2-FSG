namespace Arvores.Ed;

public class BTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        public bool Leaf;
        public List<TKey> Keys = new List<TKey>();
        public List<TValue> Values = new List<TValue>();
        public List<Node> Children = new List<Node>();

        public Node(bool leaf)
        {
            Leaf = leaf;
        }
    }

    private readonly int _t; // grau mínimo
    private Node _root;

    public BTree(int t)
    {
        if (t < 2)
            throw new ArgumentException("O grau mínimo t deve ser >= 2");

        _t = t;
        _root = new Node(true);
    }

    // -------------------------------
    // BUSCA
    // -------------------------------
    public bool TryGetValue(TKey key, out TValue value)
    {
        return Search(_root, key, out value);
    }

    private bool Search(Node node, TKey key, out TValue value)
    {
        int i = 0;

        while (i < node.Keys.Count && key.CompareTo(node.Keys[i]) > 0)
            i++;

        if (i < node.Keys.Count && key.CompareTo(node.Keys[i]) == 0)
        {
            value = node.Values[i];
            return true;
        }

        if (node.Leaf)
        {
            value = default!;
            return false;
        }

        return Search(node.Children[i], key, out value);
    }

    // -------------------------------
    // INSERÇÃO
    // -------------------------------
    public void Insert(TKey key, TValue value)
    {
        Node r = _root;

        if (r.Keys.Count == 2 * _t - 1)
        {
            Node s = new Node(false);
            _root = s;
            s.Children.Add(r);
            SplitChild(s, 0);
            InsertNonFull(s, key, value);
        }
        else
        {
            InsertNonFull(r, key, value);
        }
    }

    private void InsertNonFull(Node node, TKey key, TValue value)
    {
        int i = node.Keys.Count - 1;

        if (node.Leaf)
        {
            node.Keys.Add(default!);
            node.Values.Add(default!);

            while (i >= 0 && key.CompareTo(node.Keys[i]) < 0)
            {
                node.Keys[i + 1] = node.Keys[i];
                node.Values[i + 1] = node.Values[i];
                i--;
            }

            node.Keys[i + 1] = key;
            node.Values[i + 1] = value;
        }
        else
        {
            while (i >= 0 && key.CompareTo(node.Keys[i]) < 0)
                i--;

            i++;

            if (node.Children[i].Keys.Count == 2 * _t - 1)
            {
                SplitChild(node, i);

                if (key.CompareTo(node.Keys[i]) > 0)
                    i++;
            }

            InsertNonFull(node.Children[i], key, value);
        }
    }

    private void SplitChild(Node parent, int index)
    {
        Node full = parent.Children[index];
        Node newNode = new Node(full.Leaf);

        // 1) Salvar a chave do meio ANTES de mexer no nó
        TKey middleKey = full.Keys[_t - 1];
        TValue middleValue = full.Values[_t - 1];

        // 2) Copiar as chaves da metade direita para o novo nó
        for (int j = 0; j < _t - 1; j++)
        {
            newNode.Keys.Add(full.Keys[_t + j]);
            newNode.Values.Add(full.Values[_t + j]);
        }

        // 3) Copiar os filhos da metade direita (se não for folha)
        if (!full.Leaf)
        {
            for (int j = 0; j < _t; j++)
                newNode.Children.Add(full.Children[_t + j]);
        }

        // 4) Remover a metade direita do nó cheio
        full.Keys.RemoveRange(_t, _t - 1);
        full.Values.RemoveRange(_t, _t - 1);

        if (!full.Leaf)
            full.Children.RemoveRange(_t, _t);

        // 5) Remover a chave do meio do nó cheio
        full.Keys.RemoveAt(_t - 1);
        full.Values.RemoveAt(_t - 1);

        // 6) Inserir o novo nó no pai
        parent.Children.Insert(index + 1, newNode);

        // 7) Inserir a chave promovida no pai
        parent.Keys.Insert(index, middleKey);
        parent.Values.Insert(index, middleValue);
    }

    // -------------------------------
    // PERCURSO (para debug)
    // -------------------------------
    public void Print()
    {
        PrintNode(_root, 0);
    }

    private void PrintNode(Node node, int level)
    {
        Console.WriteLine(new string(' ', level * 2) + string.Join(", ", node.Keys));

        if (!node.Leaf)
        {
            for (int i = 0; i < node.Children.Count; i++)
                PrintNode(node.Children[i], level + 1);
        }
    }
}
