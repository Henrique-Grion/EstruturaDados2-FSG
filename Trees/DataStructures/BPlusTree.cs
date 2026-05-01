namespace Trees.DataStructures;

public class BPlusTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private abstract class Node
    {
        public List<TKey> Keys = new List<TKey>();
        public abstract bool IsLeaf { get; }
    }

    private class InternalNode : Node
    {
        public List<Node> Children = new List<Node>();
        public override bool IsLeaf => false;
    }

    private class LeafNode : Node
    {
        public List<TValue> Values = new List<TValue>();
        public LeafNode Next;
        public override bool IsLeaf => true;
    }

    private readonly int _t;
    private Node _root;

    public BPlusTree(int t)
    {
        if (t < 2)
            throw new ArgumentException("t deve ser >= 2");

        _t = t;
        _root = new LeafNode();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var leaf = FindLeaf(_root, key);

        for (int i = 0; i < leaf.Keys.Count; i++)
        {
            if (leaf.Keys[i].CompareTo(key) == 0)
            {
                value = leaf.Values[i];
                return true;
            }
        }

        value = default!;
        return false;
    }

    private LeafNode FindLeaf(Node node, TKey key)
    {
        if (node.IsLeaf)
            return (LeafNode)node;

        var internalNode = (InternalNode)node;

        int i = 0;
        while (i < internalNode.Keys.Count && key.CompareTo(internalNode.Keys[i]) >= 0)
            i++;

        return FindLeaf(internalNode.Children[i], key);
    }

    public void Insert(TKey key, TValue value)
    {
        var root = _root;

        if (root.Keys.Count == 2 * _t - 1)
        {
            var newRoot = new InternalNode();
            newRoot.Children.Add(root);
            SplitChild(newRoot, 0);
            _root = newRoot;
        }

        InsertNonFull(_root, key, value);
    }

    private void InsertNonFull(Node node, TKey key, TValue value)
    {
        if (node.IsLeaf)
        {
            var leaf = (LeafNode)node;

            int i = leaf.Keys.Count - 1;
            leaf.Keys.Add(default!);
            leaf.Values.Add(default!);

            while (i >= 0 && key.CompareTo(leaf.Keys[i]) < 0)
            {
                leaf.Keys[i + 1] = leaf.Keys[i];
                leaf.Values[i + 1] = leaf.Values[i];
                i--;
            }

            leaf.Keys[i + 1] = key;
            leaf.Values[i + 1] = value;
        }
        else
        {
            var internalNode = (InternalNode)node;

            int i = internalNode.Keys.Count - 1;
            while (i >= 0 && key.CompareTo(internalNode.Keys[i]) < 0)
                i--;

            i++;

            if (internalNode.Children[i].Keys.Count == 2 * _t - 1)
            {
                SplitChild(internalNode, i);

                if (key.CompareTo(internalNode.Keys[i]) >= 0)
                    i++;
            }

            InsertNonFull(internalNode.Children[i], key, value);
        }
    }

    public bool Remove(TKey key)
    {
        var items = new List<KeyValuePair<TKey, TValue>>();
        bool removed = false;

        foreach (var item in Scan())
        {
            if (!removed && item.Key.CompareTo(key) == 0)
            {
                removed = true;
                continue;
            }

            items.Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
        }

        if (!removed)
            return false;

        _root = new LeafNode();

        for (int i = 0; i < items.Count; i++)
            Insert(items[i].Key, items[i].Value);

        return true;
    }

    private void SplitChild(InternalNode parent, int index)
    {
        Node full = parent.Children[index];

        if (full.IsLeaf)
            SplitLeaf(parent, index, (LeafNode)full);
        else
            SplitInternal(parent, index, (InternalNode)full);
    }

    private void SplitLeaf(InternalNode parent, int index, LeafNode full)
    {
        var newLeaf = new LeafNode();

        int mid = _t;

        for (int i = mid; i < full.Keys.Count; i++)
        {
            newLeaf.Keys.Add(full.Keys[i]);
            newLeaf.Values.Add(full.Values[i]);
        }

        full.Keys.RemoveRange(mid, full.Keys.Count - mid);
        full.Values.RemoveRange(mid, full.Values.Count - mid);

        newLeaf.Next = full.Next;
        full.Next = newLeaf;

        TKey promoted = newLeaf.Keys[0];

        parent.Keys.Insert(index, promoted);
        parent.Children.Insert(index + 1, newLeaf);
    }

    private void SplitInternal(InternalNode parent, int index, InternalNode full)
    {
        var newNode = new InternalNode();

        int mid = _t - 1;

        TKey promoted = full.Keys[mid];

        for (int i = mid + 1; i < full.Keys.Count; i++)
            newNode.Keys.Add(full.Keys[i]);

        for (int i = mid + 1; i < full.Children.Count; i++)
            newNode.Children.Add(full.Children[i]);

        full.Keys.RemoveRange(mid, full.Keys.Count - mid);
        full.Children.RemoveRange(mid + 1, full.Children.Count - (mid + 1));

        parent.Keys.Insert(index, promoted);
        parent.Children.Insert(index + 1, newNode);
    }

    public IEnumerable<(TKey Key, TValue Value)> Scan()
    {
        var node = _root;

        while (!node.IsLeaf)
            node = ((InternalNode)node).Children[0];

        var leaf = (LeafNode)node;

        while (leaf != null)
        {
            for (int i = 0; i < leaf.Keys.Count; i++)
                yield return (leaf.Keys[i], leaf.Values[i]);

            leaf = leaf.Next;
        }
    }
}

