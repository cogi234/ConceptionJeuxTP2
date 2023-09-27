using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;
using System.Xml;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using System;
using System.Globalization;
using Unity.VisualScripting;

public class GameGraphs : MonoBehaviour
{
    public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };

    public AdjacencyType graphType;
    public List<Nodes> nodes;
    int vertexNumber;

    [SerializeField] Vector3[] vertexPositions;

    AdjacencyList adjacencyList;
    AdjacencyMatrix adjacencyMatrix;
    AdjacencyListCosts adjacencyListCosts;
    AdjacencyMatrixCosts adjacencyMatrixCosts;

    private void Awake()
    {
        //We initialise the graphs
        vertexNumber = vertexPositions.Length;
        adjacencyList = new AdjacencyList(vertexNumber);
        adjacencyMatrix = new AdjacencyMatrix(vertexNumber);
        adjacencyListCosts = new AdjacencyListCosts(vertexNumber);
        adjacencyMatrixCosts = new AdjacencyMatrixCosts(vertexNumber);
    }
    void CreateNodeMap2D(KeyValuePair<int, List<Vector3>> entry, Vector3Int startingPosition, Vector3Int mapSize, List<Vector3> emptyNodes)
    {
        for (int x = startingPosition.x; x < startingPosition.x + mapSize.x; ++x)
        {
            for (int z = startingPosition.z; z < startingPosition.z + mapSize.z; ++z)
            {
                Vector3 actualVector = new Vector3(x, 0, z);
                if (!emptyNodes.Contains(new Vector3(x, 0, z)))
                {
                    Gizmos.DrawSphere(actualVector, 0.2f);

                    entry.Value.Add(actualVector);
                    if (x + 1 < mapSize.x + startingPosition.x && !emptyNodes.Contains(actualVector + new Vector3(1, 0, 0)))
                    {
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(1, 0, 0));
                    }
                    if (z + 1 < mapSize.z + startingPosition.z && !emptyNodes.Contains(actualVector + new Vector3(0, 0, 1)))
                    {
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(0, 0, 1));
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        //We only draw in play mode
        if (EditorApplication.isPlaying)
        {
            //We only show the chosen graph
            IGraphRepresentation graph = adjacencyList;
            switch (graphType)
            {
                case AdjacencyType.Matrix:
                    graph = adjacencyMatrix;
                    break;
                case AdjacencyType.ListCosts:
                    graph = adjacencyListCosts;
                    break;
                case AdjacencyType.MatrixCosts:
                    graph = adjacencyMatrixCosts;
                    break;
            }

            //Show the nodes themselves and the link to their neighbours
            for (int i = 0; i < vertexNumber; i++)
            {
                Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / (float)(vertexNumber - 1));
                Gizmos.DrawSphere(nodes[i].position, 0.5f);

                //Draw links
                Gizmos.color = Color.white;
                IEnumerable<int> neighbours = graph.GetNeighbours(i);
                foreach (Vector3 neighbour in nodes[i].neighbours)
                {
                    //To avoid drawing the same link multiple times
                    Gizmos.DrawLine(nodes[i].position, neighbour);
                }
            }
        }
    }

    void ajouternodListe()
    {

        for(int i = 0;i<vertexNumber;i++)
        {

            if(i - 1 >= 0)
            {
                adjacencyList.AddEdge(i, i-1);
            }
            if (i +1 <= vertexNumber)
            {
                adjacencyList.AddEdge(i, i + 1);
            }
        }
    }
}
