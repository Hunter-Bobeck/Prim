using System;
using System.Collections.Generic;
using UnityEngine;


public class PrimCode
{
    private static int MinKey(float[] key, bool[] set, int verticesCount)
    {
        //set up min and index of min variables
        float min = float.MaxValue;
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

    private static List<GameObject> Listify(int[] parent, GameObject nodes, int verticesCount)
    {
        List<GameObject> returnList = new List<GameObject>();
        for (int i = 1; i < verticesCount; ++i)
        {
            GameObject edge = helperConnectedTo(parent[i], i, nodes);
            if(edge == Null) debug.log("Hunter broke it");
            returnList.add(edge);
        }
        return returnList;
    }

    public static void Prim(GameObject nodes)
    {
        //instantiate arrays
        int verticesCount = nodes.transform.childCount;
        int[] parent = new int[verticesCount];
        float[] key = new float[verticesCount];
        bool[] mstSet = new bool[verticesCount];

        //fill with max distances, mark as not part of MST
        for (int i = 0; i < verticesCount; ++i)
        {
            key[i] = float.MaxValue;
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
                if(helperConnectedTo(u, v, nodes) != Null)
                {
                    GameObject edge = helperConnectedTo(u, v, nodes);
                    float weight = edge.GetComponent<DistanceWeightUpdating>().distance;

                    if (mstSet[v] == false && weight < key[v])
                    {
                        parent[v] = u;
                        key[v] = weight;
                    }
                }
            }
        }

        return Listify(parent, nodes, verticesCount);
    }

    public GameObject helperConnectedTo(int u, int v, GameObject nodes)
    {
        GameObject uNode = nodes.getChild(u);
        GameObject vNode = nodes.getChild(v);
        return uNode.GetComponent<ConnectedEdges>().connectedTo(vNode);
    }
}