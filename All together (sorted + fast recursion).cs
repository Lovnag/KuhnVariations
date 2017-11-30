using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	static List<int>[] g;
	static HashSet<int>[] part; //Вершины из первой доли и из второй (в первой вершин меньше, чем во второй)
	static int[] pair;
	static bool[] was, was1; //was1 - вершины, просмотренные при составлении предваритльного паросочетания
	static bool KuhnFast(int v) //Здесь сначала проверяем всех соседей свободны ли они, а потом запускаем рекурсию
	{
		if (was[v])
			return false;
		was[v] = true;
		foreach (int u in g[v])
			if (pair[u] == -1)
			{
				pair[u] = v;
				return true;
			}
		foreach (int u in g[v])
			if (Kuhn(pair[u]))
			{
				pair[u] = v;
				return true;
			}
		return false;
	}
	static void Main()
	{
		//Чтение графа g, разбиение его на две части part[0] и part[1]
		List<Tuple<int, int>> vertexExponent = new List<Tuple<int, int>>(); //Item1 - количество соседей, Item2 - номер вершины
		for (int i = 0; i < g.Length; i++)
			vertexExponent.Add(new Tuple<int, int>(g[i].Count, i));
		vertexExponent.Sort((a, b) => a.Item1 - b.Item1);
		was1 = new bool[g.Length]; //Использовались в первом паросочетании (учитываются вершины из обеих долей)
		foreach (Tuple<int, int> v in vertexExponent)
			if (!was1[v.Item2])
				foreach (int u in g[v.Item2])
					if (!was1[u])
					{
						if (part[0].Contains(v.Item2))
							pair[u] = v.Item2;
						else
							pair[v.Item2] = u;
						was1[v.Item2] = was1[u] = true;
						break;
					}
		foreach (int v in part[0])
			if (!was1[v])
			{
				was = new bool[g.Length];
				KuhnFast(v);
			}
	}
}