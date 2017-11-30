using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	static List<int>[] g;
	static HashSet<int>[] part; //Вершины из первой доли и из второй (в первой вершин меньше, чем во второй)
	static int[] pair;
	static bool[] was;
	static HashSet<int>[] MakeBipartiteGraph(List<int>[] g) //Разбиение графа на две доли
	{
		HashSet<int>[] part = new HashSet<int>[] { new HashSet<int>(), new HashSet<int>() };
		Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>(); //Item1 - номер вершины, Item2 - какой доле принадлежит
		bool[] was = new bool[g.Length];
		for (int i = 0; i < g.Length; i++)
			if (!was[i])
			{
				q.Enqueue(new Tuple<int, int>(i, 0));
				while (q.Count != 0)
				{
					Tuple<int, int> v = q.Dequeue();
					was[v.Item1] = true;
					part[v.Item2].Add(v.Item1);
					int next = (v.Item2 + 1) % 2;
					foreach (int u in g[v.Item1])
						if (!was[u])
							q.Enqueue(new Tuple<int, int>(u, next));
				}
			}
		return part[0].Count < part[1].Count ? part : new HashSet<int>[] { part[1], part[0] };
	}
	static void Main()
	{
		Random rand = new Random();
		g = new List<int>[n];
		pair = new int[n];
		for (int i = 0; i < g.Length; i++)
		{
			g[i] = new List<int>();
			pair[i] = -1;
		}
		int n1 = rand.Next(n / 4, n * 3 / 4); //Количество вершин в первой доле
		List<int> v2 = new List<int>(); //Список вершин из второй доли
		for (int i = n1; i < n; i++)
			v2.Add(i);
		for (int i = 0; i < n1; i++)
		{
			int e = rand.Next((n - n1) / 3, n - n1); //Кол-во ребер из вершины i первой доли
			List<int> vnow2 = v2.ToList(); //Копия v2
			for (int j = 0; j < e; j++)
			{
				int u = rand.Next(vnow2.Count);
				g[i].Add(vnow2[u]);
				g[vnow2[u]].Add(i);
				vnow2.RemoveAt(u);
			}
		}
		part = MakeBipartiteGraph(g);
		//Работа алгоритма Куна
	}
}