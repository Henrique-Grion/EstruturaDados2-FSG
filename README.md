# Red‑Black Tree, B‑Tree e B+‑Tree

Documentação técnica das três estruturas, escrita de forma objetiva e organizada.

---

# Red‑Black Tree

## Visão geral
A Red‑Black Tree é uma árvore binária de busca balanceada que mantém altura O(log n) através de regras de coloração e rotações.

## Propriedades
- Cada nó é vermelho ou preto.
- A raiz é sempre preta.
- Nós vermelhos não podem ter filhos vermelhos.
- Todo caminho da raiz até uma folha nula contém o mesmo número de nós pretos.
- Folhas nulas são pretas.

## Inserção
1. Inserção segue a lógica de uma árvore binária de busca.
2. O novo nó é vermelho.
3. Se houver violações, são aplicadas correções:
   - Recolorir
   - Rotação à esquerda
   - Rotação à direita
   - Combinações de rotação e recoloração

## Remoção
A remoção segue a lógica da árvore binária de busca, mas exige correções adicionais quando um nó preto é removido.  
O algoritmo garante que o número de nós pretos em cada caminho permaneça consistente.

## Aplicações
- Estruturas internas de linguagens (TreeMap, TreeSet, OrderedDictionary)
- Índices em memória
- Estruturas ordenadas com acesso rápido

---

# B‑Tree

## Visão geral
A B‑Tree é uma árvore balanceada projetada para armazenamento em disco.  
Minimiza acessos a disco ao agrupar várias chaves em um único nó.

## Estrutura
- Cada nó contém entre t−1 e 2t−1 chaves.
- Cada nó interno possui entre t e 2t filhos.
- A raiz pode ter menos chaves.
- Todas as folhas estão no mesmo nível.
- Valores podem estar em nós internos e folhas.

## Inserção
1. A busca é feita até a folha correta.
2. Se o nó estiver cheio, ocorre split:
   - O nó é dividido em dois
   - A chave do meio é promovida ao pai
3. O processo pode se propagar até a raiz.

## Remoção
A remoção envolve:
- Redistribuição de chaves entre nós irmãos
- Fusão de nós
- Garantia de que nenhum nó fique com menos que t−1 chaves

## Aplicações
- Bancos de dados
- Sistemas de arquivos
- Índices armazenados em disco
- Estruturas que precisam minimizar leituras de páginas

---

# B+‑Tree

## Visão geral
A B+‑Tree é uma variação da B‑Tree otimizada para leitura sequencial e acesso em disco.  
É a estrutura padrão em bancos de dados e sistemas de arquivos.

## Estrutura
- Nós internos armazenam apenas chaves.
- Valores são armazenados exclusivamente nas folhas.
- Folhas são encadeadas por ponteiros.
- Todas as folhas estão no mesmo nível.
- Cada nó segue as mesmas regras de capacidade da B‑Tree.

## Inserção
1. A busca é feita até a folha.
2. A chave e o valor são inseridos na folha.
3. Se a folha estiver cheia, ocorre split:
   - A metade direita vai para uma nova folha
   - A primeira chave da nova folha é promovida ao pai
4. O processo pode subir até a raiz.

## Busca
A busca sempre termina em uma folha, mesmo quando a chave não existe.  
Isso simplifica o algoritmo e melhora a previsibilidade do acesso.

## Vantagens sobre a B‑Tree
- Todas as chaves estão nas folhas.
- Nós internos são menores, aumentando a densidade de chaves por página.
- Melhor desempenho em leituras sequenciais e range queries.

## Aplicações
- Bancos de dados relacionais
- Sistemas de arquivos
- Motores de busca
- Índices de alto desempenho

---

# Comparação

| Estrutura      | Onde ficam os valores | Encadeamento de folhas | Uso ideal |
|----------------|------------------------|--------------------------|-----------|
| Red‑Black Tree | Em todos os nós        | Não                      | Estruturas em memória |
| B‑Tree         | Em todos os nós        | Não                      | Armazenamento em disco |
| B+‑Tree        | Apenas nas folhas      | Sim                      | Bancos de dados e sistemas de arquivos |
