
using Arvores.Ed;

// Red Black Tree Example
RedBlackTree<int, string> tree = new();

tree.Insert(10, "dez");
tree.Insert(5, "cinco");
tree.Insert(15, "quinze");
tree.Insert(1, "um");
tree.Insert(7, "sete");

foreach (var kv in tree.InOrder())
    Console.WriteLine($"{kv.Key} -> {kv.Value}");

if (tree.TryGetValue(7, out var v))
    Console.WriteLine("Encontrado 7: " + v);

// B-Tree Example
var btree = new BTree<int, string>(t: 3);

btree.Insert(10, "dez");
btree.Insert(20, "vinte");
btree.Insert(5, "cinco");
btree.Insert(6, "seis");
btree.Insert(12, "doze");
btree.Insert(30, "trinta");
btree.Insert(7, "sete");
btree.Insert(17, "dezessete");

btree.Print();

if (btree.TryGetValue(12, out var v2))
    Console.WriteLine("Encontrado: " + v2);

// B+ Tree Example
var bpt = new BPlusTree<int, string>(t: 3);

bpt.Insert(10, "dez");
bpt.Insert(20, "vinte");
bpt.Insert(5, "cinco");
bpt.Insert(6, "seis");
bpt.Insert(12, "doze");
bpt.Insert(30, "trinta");
bpt.Insert(7, "sete");
bpt.Insert(17, "dezessete");

if (bpt.TryGetValue(12, out var v3))
    Console.WriteLine("Encontrado: " + v3);

Console.WriteLine("Scan:");
foreach (var item in bpt.Scan())
    Console.WriteLine($"{item.Key} -> {item.Value}");