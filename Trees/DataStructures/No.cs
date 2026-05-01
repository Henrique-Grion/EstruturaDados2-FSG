using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees.DataStructures
{
	public class No
	{
		public No Maior { get; set; }
		public No Menor { get; set; }
		public int Valor { get; }

		public No(int valor)
		{
			Valor = valor;
		}

		public override string ToString()
		{
			return Valor.ToString();
		}
	}
}
