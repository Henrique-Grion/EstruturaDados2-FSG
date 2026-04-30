using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvores.Ed
{
	public class ArvoreBST
	{
		private No _raiz;

		public void Adicionar(int valor)
		{
			No no = new No(valor);
			if (_raiz == null)
			{
				_raiz = no;
				return;
			}
			Adicionar(no, _raiz);
		}
		private void Adicionar(No novoNo, No referencia)
		{
			if (novoNo.Valor > referencia.Valor)
			{
				if (referencia.Maior == null)
				{
					referencia.Maior = novoNo;
				}
				else
				{
					Adicionar(novoNo, referencia.Maior);
				}
			}
			else
			{
				if (referencia.Menor == null)
				{
					referencia.Menor = novoNo;
				}
				else
				{
					Adicionar(novoNo, referencia.Menor);
				}
			}
		}

		public bool Remover(int valor)
		{
			bool removido;
			_raiz = Remover(_raiz, valor, out removido);
			return removido;
		}

		private No Remover(No referencia, int valor, out bool removido)
		{
			if (referencia == null)
			{
				removido = false;
				return null;
			}

			if (valor < referencia.Valor)
			{
				referencia.Menor = Remover(referencia.Menor, valor, out removido);
			}
			else if (valor > referencia.Valor)
			{
				referencia.Maior = Remover(referencia.Maior, valor, out removido);
			}
			else
			{
				removido = true;

				if (referencia.Menor == null)
					return referencia.Maior;
				else if (referencia.Maior == null)
					return referencia.Menor;

				No sucessor = EncontrarSucessor(referencia.Maior);

				No novoNo = new No(sucessor.Valor);

				novoNo.Menor = referencia.Menor;

				novoNo.Maior = Remover(referencia.Maior, sucessor.Valor, out _);

				return novoNo;
			}

			return referencia;
		}
		private No EncontrarSucessor(No referencia)
		{
			while (referencia.Menor != null)
			{
				referencia = referencia.Menor;
			}
			return referencia;
		}

		public bool Contem(int valor)
		{
			if (_raiz == null) return false;
			return Contem(_raiz, valor);
		}

		private bool Contem(No referencia, int valor)
		{
			if (referencia == null) return false;

			if (referencia.Valor == valor) return true;
			else if (valor < referencia.Valor) return Contem(referencia.Menor, valor);
			else return Contem(referencia.Maior, valor);
		}

		public int Altura()
		{
			return Altura(_raiz);
		}

		private int Altura(No referencia)
		{
			if (referencia == null) return 0;

			int alturaMenor = Altura(referencia.Menor);
			int alturaMaior = Altura(referencia.Maior);

			int maxAltura;
			if (alturaMenor > alturaMaior)
				maxAltura = alturaMenor;
			else
				maxAltura = alturaMaior;

			return 1 + maxAltura;
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

		public void ImprimirEmOrdem()
		{
			ImprimirEmOrdem(_raiz);
		}

		private void ImprimirEmOrdem(No referencia)
		{
			if (referencia == null) return;

			ImprimirEmOrdem(referencia.Menor);
			Console.Write(referencia.Valor);
			Console.Write(" ");
			ImprimirEmOrdem(referencia.Maior);
		}

		public void ImprimirEmNivel()
		{
			ImprimirEmNivel(_raiz);
		}
		private void ImprimirEmNivel(No referencia)
		{
			if (referencia == null)
			{
				Console.WriteLine("Árvore vazia.");
				return;
			}

			Queue<No> fila = new Queue<No>();
			fila.Enqueue(referencia);

			while (fila.Count > 0)
			{
				No atual = fila.Dequeue();
				Console.Write(atual.Valor + " ");

				if (atual.Menor != null)
					fila.Enqueue(atual.Menor);

				if (atual.Maior != null)
					fila.Enqueue(atual.Maior);
			}
			Console.WriteLine();
		}

		public void ImprimirDadosArvore()
		{
			Console.WriteLine("\nÁrvore BST:");
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
