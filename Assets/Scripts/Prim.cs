/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prim
{
    public class PrimCode
    {
        private static int MinKey(int[] key, bool[] set, int verticesCount)
        {
            //set up min and index of min variables
            int min = int.MaxValue;
            int minIndex = 0;

            //if not in set and less than min, update current min variables
            for (int v = 0; v < verticesCount; ++v)
            {
                if (set[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private static void Print(int[] parent, int[,] graph, int verticesCount)
        {
            Console.WriteLine("Edge\tWeight");
            for (int i = 1; i < verticesCount; ++i)
                Console.WriteLine("{0} - {1}\t{2}", parent[i], i, graph[i, parent[i]]);
                //parent of current vertex, vertex, weight
        }

        public static void Prim(int[,] graph, int verticesCount)
        {
            //instantiate arrays
            int[] parent = new int[verticesCount];
            int[] key = new int[verticesCount];
            bool[] mstSet = new bool[verticesCount];

            //fill with max distances, mark as not part of MST
            for (int i = 0; i < verticesCount; ++i)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }
            //set first key, mark its index in parent array
            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                //find vertex with smallest distance, mark it as part of MST
                int u = MinKey(key, mstSet, verticesCount);
                mstSet[u] = true;

                //update parent and key for each neighbor if the weight is lower
                for (int v = 0; v < verticesCount; ++v)
                {
                    if (Convert.ToBoolean(graph[u, v]) && mstSet[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }

            Print(parent, graph, verticesCount);
        }

        static void Main(string[] args)
        {
            int[,] graph = {
                    { 0, 2, 0, 6, 0 },
                    { 2, 0, 3, 8, 5 },
                    { 0, 3, 0, 0, 7 },
                    { 6, 8, 0, 0, 9 },
                    { 0, 5, 7, 9, 0 },
            };

            Prim(graph, 5);
            Console.ReadKey();
        }
    }
}*/