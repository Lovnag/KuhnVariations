using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	static List<int>[] g;
	static HashSet<int>[] part; //Вершины из первой доли и из второй (в первой вершин меньше, чем во второй)
	static int[] pair;
	static bool[] was, was1; //was1 - вершины, просмотренные при составлении предваритльного паросочетания
	static bool Kuhn(int v) //С плохим DFS
	{
		if (was[v] || was1[v])
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
		was1 = new bool[g.Length];
		for (int v = 0; v < g.Length; v++)
			if (!was1[v])
				foreach (int u in g[v])
					if (!was1[u])
					{
						if (part[0].Contains(v))
							pair[u] = v;
						else
							pair[v] = u;
						was1[v] = was1[u] = true;
						break;
					}
		foreach (int v in part[0])
			if (!was1[v])
			{
				was = new bool[g.Length];
				Kuhn(v);
			}
	}
}