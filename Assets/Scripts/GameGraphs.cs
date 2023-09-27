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
    [SerializeField] List<Nodes> nodes;
    int vertexNumber;

    AdjacencyList adjacencyList;
    AdjacencyMatrix adjacencyMatrix;
    AdjacencyListCosts adjacencyListCosts;
    AdjacencyMatrixCosts adjacencyMatrixCosts;

    private void Awake()
    {
        //We initialise the graphs
        vertexNumber = nodes.Count;
        adjacencyList = new AdjacencyList(vertexNumber);
        adjacencyMatrix = new AdjacencyMatrix(vertexNumber);
        adjacencyListCosts = new AdjacencyListCosts(vertexNumber);
        adjacencyMatrixCosts = new AdjacencyMatrixCosts(vertexNumber);

        //We shift the positions a bit
        for (int i = 0; i < vertexNumber; i++)
        {
            nodes[i].position += new Vector3(4, 0, 4);
        }

        //We initalise the edges
        for (int i = 0; i < vertexNumber; i++)
        {
            foreach (int neighbour in nodes[i].neighbours)
            {
                AddEdge(i, neighbour);
            }
        }
    }

    void CreateNodeMap2D(KeyValuePair<int, List<Vector3>> entry, Vector2Int startingPosition, Vector2Int mapSize, List<Vector3> emptyNodes)
    {

        for (int x = startingPosition.x; x < startingPosition.x + mapSize.x; ++x)
        {
            for (int y = startingPosition.y; y < startingPosition.y + mapSize.y; ++y)
            {
                Vector3 actualVector = new Vector3(x, 0, y);
                if (!emptyNodes.Contains(actualVector))
                {
                    //draw sphere

                    Gizmos.DrawSphere(actualVector, 0.2f);

                    entry.Value.Add(actualVector);
                    if (x + 1 < mapSize.x + startingPosition.x && !emptyNodes.Contains(actualVector + new Vector3(1, 0, 0)))
                    {
                        AddEdge(y * (1 + x), y * (1 + x) + 1);
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(1, 0, 0));
                    }
                    if (y + 1 < mapSize.y + startingPosition.y && !emptyNodes.Contains(actualVector + new Vector3(0, 0, 1)))
                    {
                        AddEdge(y * (1 + x), y * (1 + x + 1));
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

                //Show the edges first
                for (int i = 0; i < vertexNumber; i++)
            {
                //Draw links
                Gizmos.color = Color.white;
                IEnumerable<int> neighbours = graph.GetNeighbours(i);
                foreach (int neighbour in neighbours)
                {
                    //To avoid drawing the same link multiple times
                    Gizmos.DrawLine(nodes[i].position, nodes[neighbour].position);
                }
            }

            //Show the nodes themselves second
            for (int i = 0; i < vertexNumber; i++)
            {
                Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / (float)(vertexNumber - 1));
                Gizmos.DrawSphere(nodes[i].position, 0.2f);
            }
        }
    }

    public void AddEdge(int a, int b)
    {
        if (!adjacencyList.HasNeighbour(a, b))
        {
            adjacencyList.AddEdgeBidirectional(a, b);
            adjacencyMatrix.AddEdgeBidirectional(a, b);
            adjacencyListCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(nodes[a].position, nodes[b].position)));
            adjacencyMatrixCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(nodes[a].position, nodes[b].position)));
        }
    }

    public void RemoveEdge(int a, int b)
    {
        adjacencyList.RemoveEdge(a, b);
        adjacencyList.RemoveEdge(b, a);
        adjacencyListCosts.RemoveEdge(a, b);
        adjacencyListCosts.RemoveEdge(b, a);
        adjacencyMatrix.RemoveEdge(a, b);
        adjacencyMatrix.RemoveEdge(b, a);
        adjacencyMatrixCosts.RemoveEdge(a, b);
        adjacencyMatrixCosts.RemoveEdge(b, a);
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
