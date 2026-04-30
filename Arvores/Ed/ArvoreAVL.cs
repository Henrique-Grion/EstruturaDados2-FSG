using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvores.Ed
{
	public class ArvoreAVL
	{
		private No _raiz;

		public void Adicionar(int valor) => _raiz = Adicionar(_raiz, valor);

		public void Remover(int valor) => _raiz = Remover(_raiz, valor);

		public bool Contem(int valor) => Contem(_raiz, valor);

		public void ImprimirEmOrdem() { InOrder(_raiz); Console.WriteLine(); }

		public void ImprimirEmNivel() { LevelOrder(_raiz); }

		private int GetAltura(No no) => no == null ? 0 : Altura(no);

		private int GetFatorEquilibrio(No no) => no == null ? 0 : GetAltura(no.Menor) - GetAltura(no.Maior);

		private No RotacaoDireita(No y)
		{
			No x = y.Menor;
			No T2 = x.Maior;
			x.Maior = y;
			y.Menor = T2;
			return x;
		}
		private No RotacaoEsquerda(No x)
		{
			No y = x.Maior;
			No T2 = y.Menor;
			y.Menor = x;
			x.Maior = T2;
			return y;
		}

		private No Adicionar(No no, int valor)
		{
			if (no == null) return new No(valor);

			if (valor < no.Valor) no.Menor = Adicionar(no.Menor, valor);
			else if (valor > no.Valor) no.Maior = Adicionar(no.Maior, valor);
			else return no; // Duplicatas não permitidas

			return Balancear(no);
		}

		private No Remover(No no, int valor)
		{
			if (no == null) return null;

			if (valor < no.Valor) no.Menor = Remover(no.Menor, valor);
			else if (valor > no.Valor) no.Maior = Remover(no.Maior, valor);
			else
			{
				if (no.Menor == null || no.Maior == null)
					no = (no.Menor ?? no.Maior);
				else
				{
					No sucessor = ObterMenor(no.Maior);
					No novoNo = new No(sucessor.Valor);
					novoNo.Menor = no.Menor;
					novoNo.Maior = Remover(no.Maior, sucessor.Valor);
					no = novoNo;
				}
			}

			if (no == null) return null;
			return Balancear(no);
		}

		private No Balancear(No no)
		{
			int fe = GetFatorEquilibrio(no);

			if (fe > 1 && GetFatorEquilibrio(no.Menor) >= 0)
				return RotacaoDireita(no);

			if (fe > 1 && GetFatorEquilibrio(no.Menor) < 0)
			{
				no.Menor = RotacaoEsquerda(no.Menor);
				return RotacaoDireita(no);
			}

			if (fe < -1 && GetFatorEquilibrio(no.Maior) <= 0)
				return RotacaoEsquerda(no);

			if (fe < -1 && GetFatorEquilibrio(no.Maior) > 0)
			{
				no.Maior = RotacaoDireita(no.Maior);
				return RotacaoEsquerda(no);
			}

			return no;
		}

		private bool Contem(No no, int valor)
		{
			if (no == null) return false;
			if (valor == no.Valor) return true;
			return valor < no.Valor ? Contem(no.Menor, valor) : Contem(no.Maior, valor);
		}

		private No ObterMenor(No no)
		{
			while (no.Menor != null) no = no.Menor;
			return no;
		}

		public int Altura()
		{
			return Altura(_raiz);
		}
		private int Altura(No no)
		{
			if (no == null) return 0;
			return 1 + Math.Max(Altura(no.Menor), Altura(no.Maior));
		}

		public int ContarNos()
		{
			return ContarNos(_raiz);
		}

		private int ContarNos(No referencia)
		{
			if (referencia == null) return 0;

			int totalMenor = ContarNos(referencia.Menor);
			int totalMaior = ContarNos(referencia.Maior);

			return 1 + totalMenor + totalMaior;
		}

		public int ContarFolhas()
		{
			return ContarFolhas(_raiz);
		}

		private int ContarFolhas(No referencia)
		{
			if (referencia == null)
				return 0;

			if (referencia.Menor == null && referencia.Maior == null)
				return 1;

			int folhasMenor = ContarFolhas(referencia.Menor);
			int folhasMaior = ContarFolhas(referencia.Maior);

			return folhasMenor + folhasMaior;
		}

		private void InOrder(No no)
		{
			if (no == null) return;
			InOrder(no.Menor);
			Console.Write(no.Valor + " ");
			InOrder(no.Maior);
		}

		private void LevelOrder(No no)
		{
			if (no == null) return;
			var fila = new Queue<No>();
			fila.Enqueue(no);
			while (fila.Count > 0)
			{
				var atual = fila.Dequeue();
				Console.Write(atual.Valor + " ");
				if (atual.Menor != null) fila.Enqueue(atual.Menor);
				if (atual.Maior != null) fila.Enqueue(atual.Maior);
			}
			Console.WriteLine();
		}

		public void ImprimirDadosArvore()
		{
			Console.WriteLine("\nÁrvore AVL:");
			Console.WriteLine("Altura: " + Altura());
			Console.WriteLine("Total de nós: " + ContarNos());
			Console.WriteLine("Total de folhas: " + ContarFolhas());
			Console.WriteLine("Impressão em nível da árvore:");
			ImprimirEmNivel();
			Console.WriteLine("Impressão em ordem da árvore:");
			ImprimirEmOrdem();
		}
	}
}
