using System;
using System.Collections.Generic;
using System.Linq;

namespace Trees.DataStructures
{
    public class TrieTree
    {
        private TrieNode _root;

        public TrieTree()
        {
            _root = new TrieNode();
        }

        public void Insert(string word)
        {
            if (string.IsNullOrEmpty(word))
                return;

            TrieNode node = _root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                {
                    node.Children[c] = new TrieNode();
                }
                node = node.Children[c];
            }
            node.IsWordEnd = true;
        }

        public bool Search(string word)
        {
            TrieNode node = _root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c))
                    return false;
                node = node.Children[c];
            }
            return node.IsWordEnd;
        }

        public bool SearchByPrefix(string prefix)
        {
            TrieNode node = _root;
            foreach (char c in prefix)
            {
                if (!node.Children.ContainsKey(c))
                    return false;
                node = node.Children[c];
            }
            return true;
        }

        public List<string> GetWordsByPrefix(string prefix)
        {
            List<string> result = new List<string>();
            TrieNode node = _root;

            foreach (char c in prefix)
            {
                if (!node.Children.ContainsKey(c))
                    return result;
                node = node.Children[c];
            }

            SearchDFS(node, prefix, result);
            return result;
        }

        public bool Remove(string word)
        {
            return RemoveRecursive(_root, word, 0);
        }

        private bool RemoveRecursive(TrieNode node, string word, int index)
        {
            if (index == word.Length)
            {
                if (!node.IsWordEnd)
                    return false;

                node.IsWordEnd = false;
                return node.Children.Count == 0;
            }

            char c = word[index];
            if (!node.Children.ContainsKey(c))
                return false;

            TrieNode next = node.Children[c];
            bool shouldRemove = RemoveRecursive(next, word, index + 1);

            if (shouldRemove)
            {
                node.Children.Remove(c);
                return node.Children.Count == 0 && !node.IsWordEnd;
            }

            return false;
        }

        public List<string> GetAllWords()
        {
            List<string> result = new List<string>();
            SearchDFS(_root, "", result);
            return result;
        }

        private void SearchDFS(TrieNode node, string word, List<string> result)
        {
            if (node.IsWordEnd)
            {
                result.Add(word);
            }

            foreach (var pair in node.Children)
            {
                SearchDFS(pair.Value, word + pair.Key, result);
            }
        }

        public void Print()
        {
            Console.WriteLine("=== Trie Tree ===");
            PrintRecursive(_root, "", 0);
        }

        private void PrintRecursive(TrieNode node, string prefix, int depth)
        {
            if (node.IsWordEnd)
            {
                Console.WriteLine(new string(' ', depth * 2) + "└─ " + prefix + " (FIM)");
            }

            foreach (var pair in node.Children.OrderBy(x => x.Key))
            {
                Console.WriteLine(new string(' ', depth * 2) + "├─ " + pair.Key);
                PrintRecursive(pair.Value, prefix + pair.Key, depth + 1);
            }
        }

        public int CountWords()
        {
            return CountWordsRecursive(_root);
        }

        private int CountWordsRecursive(TrieNode node)
        {
            int count = node.IsWordEnd ? 1 : 0;
            foreach (var child in node.Children.Values)
            {
                count += CountWordsRecursive(child);
            }
            return count;
        }
    }

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; set; } = new Dictionary<char, TrieNode>();
        public bool IsWordEnd { get; set; } = false;
    }
}
