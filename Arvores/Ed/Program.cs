using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvores.Ed
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ArvoreBST arvoreBST = new ArvoreBST();
			ArvoreAVL arvoreAVL = new ArvoreAVL();

			// Nós iniciais
			Console.WriteLine("\nAdicionar (10, 5, 12, 3):");
			arvoreBST.Adicionar(10);
			arvoreBST.Adicionar(5);
			arvoreBST.Adicionar(12);
			arvoreBST.Adicionar(3);

			arvoreAVL.Adicionar(10);
			arvoreAVL.Adicionar(5);
			arvoreAVL.Adicionar(12);
			arvoreAVL.Adicionar(3);

			//Impressão das árvores em nível e em ordem 1
			arvoreBST.ImprimirDadosArvore();
			Console.WriteLine();
			arvoreAVL.ImprimirDadosArvore();

			//Inserção de valores 1
			Console.WriteLine("\nAdicionar (14, 13, 9, 8):");
			arvoreBST.Adicionar(14);
			arvoreBST.Adicionar(13);
			arvoreBST.Adicionar(9);
			arvoreBST.Adicionar(8);

			arvoreAVL.Adicionar(14);
			arvoreAVL.Adicionar(13);
			arvoreAVL.Adicionar(9);
			arvoreAVL.Adicionar(8);

			//Impressão das árvores em nível e em ordem 2
			arvoreBST.ImprimirDadosArvore();
			Console.WriteLine();
			arvoreAVL.ImprimirDadosArvore();

			//Inserção de valores 2
			Console.WriteLine("\nAdicionar (11, 6, 15):");
			arvoreBST.Adicionar(11);
			arvoreBST.Adicionar(6);
			arvoreBST.Adicionar(15);

			arvoreAVL.Adicionar(11);
			arvoreAVL.Adicionar(6);
			arvoreAVL.Adicionar(15);

			//Impressão das árvores em nível e em ordem 3
			arvoreBST.ImprimirDadosArvore();
			Console.WriteLine();
			arvoreAVL.ImprimirDadosArvore();

			Console.WriteLine("\nRemover 5");
			arvoreBST.Remover(5);
			arvoreAVL.Remover(5);

			//Impressão das árvores em nível e em ordem 4
			arvoreBST.ImprimirDadosArvore();
			Console.WriteLine();
			arvoreAVL.ImprimirDadosArvore();
		}
	}
}