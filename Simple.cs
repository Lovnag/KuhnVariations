using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	static List<int>[] g;
	static HashSet<int>[] part; //Вершины из первой доли и из второй (в первой вершин меньше, чем во второй)
	static int[] pair;
	static bool[] was;
	static bool Kuhn(int v) //С плохим DFS
	{
		if (was[v])
			return false;
		was[v] = true;
		foreach (int u in g[v])
			if (pair[u] == -1 || Kuhn(pair[u]))
			{
				pair[u] = v;
				return true;
			}
		return false;
	}
	static void Main()
	{
		//Чтение графа g, разбиение его на две части part[0] и part[1]
		foreach (int v in part[0])
		{
			was = new bool[g.Length];
			Kuhn(v);
		}
	}
}