# Estruturas de Arvores do Projeto

Documentacao tecnica das estruturas implementadas no projeto, escrita de forma objetiva e organizada.

As implementacoes atuais incluem:
- BST (Binary Search Tree)
- AVL Tree
- Red-Black Tree
- B-Tree
- B+-Tree

---

# BST (Binary Search Tree)

## Visao geral
A BST e uma arvore binaria de busca sem balanceamento automatico.

## Caracteristicas
- Para cada no: valores menores ficam na subarvore da esquerda e maiores na direita.
- Operacoes de busca, insercao e remocao dependem da altura da arvore.
- Em cenarios desfavoraveis pode degradar para comportamento linear.

## Insercao
1. Compara a chave com o no atual.
2. Desce para esquerda ou direita ate encontrar posicao nula.
3. Insere o novo no nessa posicao.

## Remocao
1. Remove diretamente se for folha.
2. Substitui pelo unico filho quando houver apenas um.
3. Usa sucessor em ordem quando houver dois filhos.

## Aplicacoes
- Base conceitual para arvores balanceadas.
- Estruturas simples em memoria com baixo overhead.

---

# AVL Tree

## Visao geral
A AVL e uma arvore binaria de busca auto-balanceada.

## Caracteristicas
- Mantem fator de balanceamento entre -1 e 1 para cada no.
- Garante altura O(log n).
- Usa rotacoes para restaurar balanceamento.

## Insercao
1. Insere como uma BST.
2. Atualiza balanceamento no retorno da recursao.
3. Aplica rotacoes simples ou duplas quando necessario.

## Remocao
1. Remove como em BST.
2. Recalcula fator de balanceamento nos ancestrais.
3. Aplica rotacoes para manter a propriedade AVL.

## Aplicacoes
- Estruturas em memoria com busca previsivel.
- Cenarios com muitas consultas e atualizacoes.

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

| Estrutura      | Balanceamento | Onde ficam os valores | Encadeamento de folhas | Uso ideal |
|----------------|---------------|-----------------------|--------------------------|-----------|
| BST            | Nao           | Em todos os nos       | Nao                      | Estruturas simples em memoria |
| AVL Tree       | Sim (estrito) | Em todos os nos       | Nao                      | Busca em memoria com altura controlada |
| Red‑Black Tree | Sim (relaxado)| Em todos os nos       | Nao                      | Estruturas ordenadas gerais em memoria |
| B‑Tree         | Sim           | Em todos os nos       | Nao                      | Armazenamento em disco |
| B+‑Tree        | Sim           | Apenas nas folhas     | Sim                      | Bancos de dados e range scan |
