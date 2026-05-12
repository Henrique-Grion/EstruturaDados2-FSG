using System;
using System.Collections.Generic;
using System.Linq;

namespace Trees.DataStructures
{
    public class HeapTree<T> where T : IComparable<T>
    {
        private List<T> _items;
        private bool _isMaxHeap;

        public int Size => _items.Count;

        public HeapTree(bool isMaxHeap = false)
        {
            _items = new List<T>();
            _isMaxHeap = isMaxHeap;
        }

        public HeapTree(IEnumerable<T> items, bool isMaxHeap = false)
        {
            _items = new List<T>(items);
            _isMaxHeap = isMaxHeap;
            BuildHeap();
        }

        public void Insert(T item)
        {
            _items.Add(item);
            BubbleUp(_items.Count - 1);
        }

        public T RemoveRoot()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Heap is empty!");

            T root = _items[0];

            if (_items.Count == 1)
            {
                _items.RemoveAt(0);
            }
            else
            {
                _items[0] = _items[_items.Count - 1];
                _items.RemoveAt(_items.Count - 1);
                BubbleDown(0);
            }

            return root;
        }

        public T GetRoot()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Heap is empty!");

            return _items[0];
        }

        public bool IsEmpty() => _items.Count == 0;

        public void Clear()
        {
            _items.Clear();
        }

        public List<T> GetOrderedItems()
        {
            List<T> result = new List<T>();
            var copy = new HeapTree<T>(_items, _isMaxHeap);

            while (!copy.IsEmpty())
            {
                result.Add(copy.RemoveRoot());
            }

            return result;
        }

        public void Print()
        {
            Console.WriteLine($"=== {(_isMaxHeap ? "Max" : "Min")} Heap ===");
            if (_items.Count == 0)
            {
                Console.WriteLine("Heap vazio!");
                return;
            }

            PrintLevel();
            Console.WriteLine();
        }

        private void PrintLevel()
        {
            if (_items.Count == 0)
                return;

            Queue<(int index, int depth)> queue = new Queue<(int, int)>();
            queue.Enqueue((0, 0));
            int currentDepth = -1;

            while (queue.Count > 0)
            {
                var (index, depth) = queue.Dequeue();

                if (depth > currentDepth)
                {
                    currentDepth = depth;
                    Console.WriteLine();
                }

                Console.Write($"{_items[index]} ");

                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;

                if (leftChild < _items.Count)
                    queue.Enqueue((leftChild, depth + 1));

                if (rightChild < _items.Count)
                    queue.Enqueue((rightChild, depth + 1));
            }

            Console.WriteLine();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", _items)}]";
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (ShouldBubbleUp(index, parentIndex))
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int targetIndex = index;

                if (leftChild < _items.Count && ShouldBubbleDown(leftChild, targetIndex))
                    targetIndex = leftChild;

                if (rightChild < _items.Count && ShouldBubbleDown(rightChild, targetIndex))
                    targetIndex = rightChild;

                if (targetIndex == index)
                    break;

                Swap(index, targetIndex);
                index = targetIndex;
            }
        }

        private void BuildHeap()
        {
            for (int i = _items.Count / 2 - 1; i >= 0; i--)
            {
                BubbleDown(i);
            }
        }

        private bool ShouldBubbleUp(int childIndex, int parentIndex)
        {
            if (_isMaxHeap)
                return _items[childIndex].CompareTo(_items[parentIndex]) > 0;
            else
                return _items[childIndex].CompareTo(_items[parentIndex]) < 0;
        }

        private bool ShouldBubbleDown(int childIndex, int parentIndex)
        {
            if (_isMaxHeap)
                return _items[childIndex].CompareTo(_items[parentIndex]) > 0;
            else
                return _items[childIndex].CompareTo(_items[parentIndex]) < 0;
        }

        private void Swap(int i, int j)
        {
            T temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }
    }

    public class MinHeap<T> : HeapTree<T> where T : IComparable<T>
    {
        public MinHeap() : base(false) { }
        public MinHeap(IEnumerable<T> elementos) : base(elementos, false) { }
    }

    public class MaxHeap<T> : HeapTree<T> where T : IComparable<T>
    {
        public MaxHeap() : base(true) { }
        public MaxHeap(IEnumerable<T> elementos) : base(elementos, true) { }
    }
}
