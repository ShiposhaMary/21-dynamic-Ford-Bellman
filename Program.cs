using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_Ford_Bellman
{
    class Program
    {/* Поиск кратчайшего пути во взвешенном графе 
        с отрицательными весами (алгоритм Форда-Беллмана).*/
        public class Edge
        {
            public int From, To;
            public int Cost;
        }
        public static int GetMinPathCost(List<Edge> edges, int startNode, int finalNode)
        {
            var maxNodeIndex =
                edges.SelectMany(e => new[] { e.From, e.To })
                .Concat(new[] { startNode, finalNode })
                .Max();
            int[] opt = Enumerable.Repeat(int.MaxValue, maxNodeIndex + 1).ToArray();
            opt[startNode] = 0;

            for (var pathSize = 1; pathSize <= maxNodeIndex; pathSize++)
                foreach (var edge in edges)
                    if (opt[edge.From] != int.MaxValue)
                        opt[edge.To] = Math.Min(opt[edge.To], opt[edge.From] + edge.Cost);
            return opt[finalNode];
        }
        static void Main(string[] args)
        {

            var graph = new List<Edge> {
        new Edge { From=0, To=1, Cost=2 },
        new Edge { From=0, To=2, Cost=5 },
        new Edge { From=2, To=1, Cost=-4 },
        new Edge { From=1, To=3, Cost=3 },
        new Edge { From=1, To=4, Cost=2 },
        new Edge { From=3, To=4, Cost=1 }
    };
            Assert.AreEqual(0, GetMinPathCost(graph, 0, 0), "0 → 0");
            Assert.AreEqual(1, GetMinPathCost(graph, 3, 4), "3 → 4");
            Assert.AreEqual(3, GetMinPathCost(graph, 1, 3), "1 → 3");
            Assert.AreEqual(4, GetMinPathCost(graph, 0, 3), "0 → 3");
            Assert.AreEqual(3, GetMinPathCost(graph, 0, 4), "0 → 4");
            Assert.AreEqual(5, GetMinPathCost(graph, 0, 2), "0 → 2");
            Assert.AreEqual(int.MaxValue, GetMinPathCost(graph, 0, 42), "0 → 42");
            SecretTests();
        }
    }
}
