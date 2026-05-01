using Trees.DataStructures;

BSTTree bstTree = new BSTTree();
AVLTree avlTree = new AVLTree();
RedBlackTree<int, string> rbTree = new();
var bTree = new BTree<int, string>(t: 3);
var bpTree = new BPlusTree<int, string>(t: 3);

// Nós iniciais
Console.WriteLine("\nAdicionar (10, 5, 12, 3):");
bstTree.Insert(10);
bstTree.Insert(5);
bstTree.Insert(12);
bstTree.Insert(3);

avlTree.Insert(10);
avlTree.Insert(5);
avlTree.Insert(12);
avlTree.Insert(3);

rbTree.Insert(10, "dez");
rbTree.Insert(5, "cinco");
rbTree.Insert(12, "doze");
rbTree.Insert(3, "tres");

bTree.Insert(10, "dez");
bTree.Insert(5, "cinco");
bTree.Insert(12, "doze");
bTree.Insert(3, "tres");

//Impressão das árvores em nível e em ordem 1
bstTree.ImprimirDadosArvore();
Console.WriteLine();
avlTree.ImprimirDadosArvore();
Console.WriteLine();
foreach (var kv in rbTree.InOrder())
    Console.WriteLine($"{kv.Key} -> {kv.Value}");
Console.WriteLine();
bTree.Print();
Console.WriteLine();

//Contem

if (bstTree.Contem(12))
    Console.WriteLine("bstTree Encontrado 12: " + bstTree.Contem(12));

if (avlTree.Contem(12))
    Console.WriteLine("avlTree Encontrado 12: " + avlTree.Contem(12));

if (rbTree.TryGetValue(12, out var v))
    Console.WriteLine("rbTree Encontrado 12: " + v);

if (bTree.TryGetValue(12, out var v2))
    Console.WriteLine("bTree Encontrado 12: " + v2);

//Inserção de valores 1
Console.WriteLine("\nAdicionar (14, 13, 9, 8):");
bstTree.Insert(14);
bstTree.Insert(13);
bstTree.Insert(9);
bstTree.Insert(8);

avlTree.Insert(14);
avlTree.Insert(13);
avlTree.Insert(9);
avlTree.Insert(8);

rbTree.Insert(14, "quatorze");
rbTree.Insert(13, "treze");
rbTree.Insert(9, "nove");
rbTree.Insert(8, "oito");

bTree.Insert(14, "quatorze");
bTree.Insert(13, "treze");
bTree.Insert(9, "nove");
bTree.Insert(8, "oito");

//Impressão das árvores em nível e em ordem 2
bstTree.ImprimirDadosArvore();
Console.WriteLine();
avlTree.ImprimirDadosArvore();
Console.WriteLine();
foreach (var kv in rbTree.InOrder())
    Console.WriteLine($"{kv.Key} -> {kv.Value}");
Console.WriteLine();
bTree.Print();
Console.WriteLine();
Console.WriteLine("Scan:");
foreach (var item in bpTree.Scan())
    Console.WriteLine($"{item.Key} -> {item.Value}");

//Inserção de valores 2
Console.WriteLine("\nAdicionar (11, 6, 15):");
bstTree.Insert(11);
bstTree.Insert(6);
bstTree.Insert(15);

avlTree.Insert(11);
avlTree.Insert(6);
avlTree.Insert(15);

rbTree.Insert(11, "onze");
rbTree.Insert(6, "seis");
rbTree.Insert(15, "quinze");

bTree.Insert(11, "onze");
bTree.Insert(6, "seis");
bTree.Insert(15, "quinze");

//Impressão das árvores em nível e em ordem 3
bstTree.ImprimirDadosArvore();
Console.WriteLine();
avlTree.ImprimirDadosArvore();
Console.WriteLine();
foreach (var kv in rbTree.InOrder())
    Console.WriteLine($"{kv.Key} -> {kv.Value}");
Console.WriteLine();
bTree.Print();
Console.WriteLine();

//Remoção de valores 1
Console.WriteLine("\nRemover 5");
bstTree.Remove(5);
avlTree.Remove(5);
rbTree.Remove(5);
bTree.Remove(5);
bpTree.Remove(5);

//Impressão das árvores em nível e em ordem 4
bstTree.ImprimirDadosArvore();
Console.WriteLine();
avlTree.ImprimirDadosArvore();
Console.WriteLine();
foreach (var kv in rbTree.InOrder())
    Console.WriteLine($"{kv.Key} -> {kv.Value}");
Console.WriteLine();
bTree.Print();
Console.WriteLine();
