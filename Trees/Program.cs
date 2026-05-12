using System.Diagnostics;
using Trees.DataStructures;

// ══════════════════════════════════════════════════════════════════════════
// PROGRAMA DE DEMONSTRAÇÃO DE ESTRUTURAS DE DADOS - ÁRVORES
// ══════════════════════════════════════════════════════════════════════════

Console.Clear();
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Seção 1: Testes de Árvores Binárias
TesteBinariasTrees();

// Seção 2: Teste de Trie Tree
TesteTrieTree();

// Seção 3: Teste de Heap Tree
TesteHeapTree();

// Seção 4: Benchmarks de Performance
TesteBenchmarks();

// ══════════════════════════════════════════════════════════════════════════
// MÉTODOS DE TESTE
// ══════════════════════════════════════════════════════════════════════════

void TesteBinariasTrees()
{
    PrintHeader("TESTE: ÁRVORES BINÁRIAS");

    // Inicializar árvores
    BSTTree bstTree = new BSTTree();
    AVLTree avlTree = new AVLTree();
    RedBlackTree<int, string> rbTree = new();
    var bTree = new BTree<int, string>(t: 3);
    var bpTree = new BPlusTree<int, string>(t: 3);

    // Dados de teste
    int[] valores = { 10, 5, 12, 3, 14, 13, 9, 8, 11, 6, 15 };
    
    // Fase 1: Inserção
    PrintSection("Fase 1: Inserção de Valores");
    Console.WriteLine($"Inserindo: {string.Join(", ", valores)}\n");
    
    foreach (int v in valores)
    {
        bstTree.Insert(v);
        avlTree.Insert(v);
        rbTree.Insert(v, $"val_{v}");
        bTree.Insert(v, $"val_{v}");
        bpTree.Insert(v, $"val_{v}");
    }

    // Fase 2: Visualização
    PrintSection("Fase 2: Visualização das Árvores");
    bstTree.Print();
    Console.WriteLine();
    avlTree.Print();
    Console.WriteLine();
    rbTree.Print();
    Console.WriteLine();
    bTree.Print();
    Console.WriteLine();
    bpTree.Print();
    Console.WriteLine();

    // Fase 3: Busca
    PrintSection("Fase 3: Teste de Busca");
    TesteBuscasArvores(bstTree, avlTree, rbTree, bTree, 12);

    // Fase 4: Remoção
    PrintSection("Fase 4: Remoção de Elemento (5)");
    Console.WriteLine("Removendo valor 5...\n");
    bstTree.Remove(5);
    avlTree.Remove(5);
    rbTree.Remove(5);
    bTree.Remove(5);
    Console.WriteLine("Visualizando BST após remoção:");
    bstTree.Print();
    Console.WriteLine();
}

void TesteBuscasArvores(BSTTree bst, AVLTree avl, RedBlackTree<int, string> rb, BTree<int, string> btree, int valor)
{
    Console.WriteLine($"Buscando valor: {valor}\n");
    Console.WriteLine($"  ✓ BST encontrou:       {bst.Contem(valor)}");
    Console.WriteLine($"  ✓ AVL encontrou:       {avl.Contem(valor)}");
    if (rb.TryGetValue(valor, out var rbValue))
        Console.WriteLine($"  ✓ Red-Black encontrou: {rbValue}");
    if (btree.TryGetValue(valor, out var btreeValue))
        Console.WriteLine($"  ✓ B-Tree encontrou:    {btreeValue}");
    Console.WriteLine();
}

void TesteTrieTree()
{
    PrintHeader("TESTE: TRIE TREE");

    TrieTree trieTree = new TrieTree();
    string[] palavras = { "cachorro", "carro", "casa", "gato", "bateria", "bola", "cabeça" };

    // Inserção
    PrintSection("Fase 1: Inserção de Palavras");
    Console.WriteLine($"Inserindo: {string.Join(", ", palavras)}\n");
    foreach (string p in palavras)
        trieTree.Insert(p);

    // Informações
    PrintSection("Fase 2: Análise");
    Console.WriteLine($"Total de palavras:       {trieTree.CountWords()}");
    Console.WriteLine($"Palavras com prefixo 'ca':\n");
    var wordsCa = trieTree.GetWordsByPrefix("ca");
    foreach (var word in wordsCa)
        Console.WriteLine($"  • {word}");
    Console.WriteLine();

    // Visualização
    PrintSection("Fase 3: Estrutura da Trie");
    trieTree.Print();
    Console.WriteLine();
}

void TesteHeapTree()
{
    PrintHeader("TESTE: HEAP TREE");

    int[] values = { 15, 10, 20, 8, 21, 1, 12 };

    // Min Heap
    PrintSection("Min Heap");
    MinHeap<int> minHeap = new MinHeap<int>(values);
    Console.WriteLine($"Valores: {string.Join(", ", values)}\n");
    Console.WriteLine($"Estrutura: {minHeap}\n");
    minHeap.Print();
    Console.WriteLine();

    // Max Heap
    PrintSection("Max Heap");
    MaxHeap<int> maxHeap = new MaxHeap<int>(values);
    Console.WriteLine($"Valores: {string.Join(", ", values)}\n");
    Console.WriteLine($"Estrutura: {maxHeap}\n");
    maxHeap.Print();
    Console.WriteLine();
}

void TesteBenchmarks()
{
    PrintHeader("BENCHMARKS: OPERAÇÕES INTENSIVAS DE BUSCA");

    const int TAMANHO = 50000;
    const int NUM_BUSCAS = 25000;

    Console.WriteLine($"Configuração: {TAMANHO:N0} inserções | {NUM_BUSCAS:N0} buscas\n");

    Random rand = new Random(42);
    int[] dados = Enumerable.Range(1, TAMANHO).OrderBy(_ => rand.Next()).ToArray();
    int[] buscas = Enumerable.Range(1, NUM_BUSCAS).Select(_ => dados[rand.Next(TAMANHO)]).ToArray();

    // Executar benchmarks
    ExecutarBenchmarkBST(dados, buscas);
    ExecutarBenchmarkAVL(dados, buscas);
    ExecutarBenchmarkRedBlack(dados, buscas);
    ExecutarBenchmarkBTree(dados, buscas);
    ExecutarBenchmarkBPlusTree(dados, buscas);

    PrintFooter();
}

void ExecutarBenchmarkBST(int[] dados, int[] buscas)
{
    PrintSection("Benchmark: BST (Binary Search Tree)");
    BSTTree bst = new BSTTree();
    var sw = Stopwatch.StartNew();
    foreach (int val in dados) bst.Insert(val);
    sw.Stop();
    long tempoInsercao = sw.ElapsedMilliseconds;
    
    sw = Stopwatch.StartNew();
    int acertos = 0;
    foreach (int val in buscas) if (bst.Contem(val)) acertos++;
    sw.Stop();
    
    ImprimirResultadoBenchmark(tempoInsercao, sw.ElapsedMilliseconds, acertos, buscas.Length);
}

void ExecutarBenchmarkAVL(int[] dados, int[] buscas)
{
    PrintSection("Benchmark: AVL Tree");
    AVLTree avl = new AVLTree();
    var sw = Stopwatch.StartNew();
    foreach (int val in dados) avl.Insert(val);
    sw.Stop();
    long tempoInsercao = sw.ElapsedMilliseconds;
    
    sw = Stopwatch.StartNew();
    int acertos = 0;
    foreach (int val in buscas) if (avl.Contem(val)) acertos++;
    sw.Stop();
    
    ImprimirResultadoBenchmark(tempoInsercao, sw.ElapsedMilliseconds, acertos, buscas.Length);
}

void ExecutarBenchmarkRedBlack(int[] dados, int[] buscas)
{
    PrintSection("Benchmark: Red-Black Tree");
    RedBlackTree<int, int> rb = new();
    var sw = Stopwatch.StartNew();
    foreach (int val in dados) rb.Insert(val, val);
    sw.Stop();
    long tempoInsercao = sw.ElapsedMilliseconds;
    
    sw = Stopwatch.StartNew();
    int acertos = 0;
    foreach (int val in buscas) if (rb.TryGetValue(val, out _)) acertos++;
    sw.Stop();
    
    ImprimirResultadoBenchmark(tempoInsercao, sw.ElapsedMilliseconds, acertos, buscas.Length);
}

void ExecutarBenchmarkBTree(int[] dados, int[] buscas)
{
    PrintSection("Benchmark: B-Tree (t=100)");
    var btree = new BTree<int, int>(t: 100);
    var sw = Stopwatch.StartNew();
    foreach (int val in dados) btree.Insert(val, val);
    sw.Stop();
    long tempoInsercao = sw.ElapsedMilliseconds;
    
    sw = Stopwatch.StartNew();
    int acertos = 0;
    foreach (int val in buscas) if (btree.TryGetValue(val, out _)) acertos++;
    sw.Stop();
    
    ImprimirResultadoBenchmark(tempoInsercao, sw.ElapsedMilliseconds, acertos, buscas.Length);
}

void ExecutarBenchmarkBPlusTree(int[] dados, int[] buscas)
{
    PrintSection("Benchmark: B-Plus Tree (t=100)");
    var bplus = new BPlusTree<int, int>(t: 100);
    var sw = Stopwatch.StartNew();
    foreach (int val in dados) bplus.Insert(val, val);
    sw.Stop();
    long tempoInsercao = sw.ElapsedMilliseconds;
    
    sw = Stopwatch.StartNew();
    int acertos = 0;
    foreach (int val in buscas) if (bplus.TryGetValue(val, out _)) acertos++;
    sw.Stop();
    
    ImprimirResultadoBenchmark(tempoInsercao, sw.ElapsedMilliseconds, acertos, buscas.Length);
}

// ══════════════════════════════════════════════════════════════════════════
// UTILITÁRIOS DE FORMATAÇÃO
// ══════════════════════════════════════════════════════════════════════════

void PrintHeader(string titulo)
{
    Console.WriteLine("\n" + new string('=', 70));
    Console.WriteLine($"  {titulo}");
    Console.WriteLine(new string('=', 70) + "\n");
}

void PrintSection(string titulo)
{
    Console.WriteLine($"\n>> {titulo}");
    Console.WriteLine(new string('-', 50));
}

void PrintFooter()
{
    Console.WriteLine("\n" + new string('=', 70));
    Console.WriteLine("  [OK] TODOS OS TESTES E BENCHMARKS CONCLUIDOS!");
    Console.WriteLine(new string('=', 70) + "\n");
}

void ImprimirResultadoBenchmark(long tempoInsercao, long tempoBuscas, int acertos, int totalBuscas)
{
    Console.WriteLine($"  Insercao:     {tempoInsercao:N0} ms");
    Console.WriteLine($"  Busca:        {tempoBuscas:N0} ms ({totalBuscas:N0} ops)");
    Console.WriteLine($"  Taxa sucesso: {acertos:N0}/{totalBuscas:N0} ({100.0 * acertos / totalBuscas:F1}%)");
    Console.WriteLine();
}