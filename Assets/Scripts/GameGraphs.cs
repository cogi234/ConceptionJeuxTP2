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
using static UnityEngine.EventSystems.EventTrigger;

public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };

public class GameGraphs : MonoBehaviour
{
    public AdjacencyType graphType;

    public int width, height;
    [SerializeField] float distanceBetweenVertices;

    bool generated = false;
    int vertexNumber;
    Vector3[] vertexPositions;
    AdjacencyList adjacencyList;
    AdjacencyMatrix adjacencyMatrix;
    AdjacencyListCosts adjacencyListCosts;
    AdjacencyMatrixCosts adjacencyMatrixCosts;

    public void Generate()
    {
        //We initialise the graphs
        vertexNumber = width * height;
        vertexPositions = new Vector3[vertexNumber];
        adjacencyList = new AdjacencyList(vertexNumber);
        adjacencyMatrix = new AdjacencyMatrix(vertexNumber);
        adjacencyListCosts = new AdjacencyListCosts(vertexNumber);
        adjacencyMatrixCosts = new AdjacencyMatrixCosts(vertexNumber);


        for (int i = 0; i < vertexNumber; i++)
        {
            int x = i % width;
            int y = i / width;
            vertexPositions[i] = new Vector3(x * distanceBetweenVertices + 10, 0, y * distanceBetweenVertices + 10);

            if (x != 0)
                AddEdge(i, i - 1);
            if (x != width - 1)
                AddEdge(i, i + 1);
            if (y != 0)
                AddEdge(i, i - width);
            if (y != height - 1)
                AddEdge(i, i + width);
        }

        generated = true;
    }

    private void OnDrawGizmos()
    {
        //We only draw if the graph has been generated
        if (generated)
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
                    Gizmos.DrawLine(vertexPositions[i], vertexPositions[neighbour]);
                }
            }

            //Show the nodes themselves second
            for (int i = 0; i < vertexNumber; i++)
            {
                Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / (float)(vertexNumber - 1));
                Gizmos.DrawSphere(vertexPositions[i], 0.5f);
            }
        }
    }

    public void AddEdge(int a, int b)
    {
        if (!adjacencyList.HasNeighbour(a, b) && a >= 0 && a < vertexNumber && b >= 0 && b < vertexNumber)
        {
            adjacencyList.AddEdgeBidirectional(a, b);
            adjacencyMatrix.AddEdgeBidirectional(a, b);
            adjacencyListCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));
            adjacencyMatrixCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));
        }
    }

    public void RemoveEdge(int a, int b)
    {
        if (adjacencyList.HasNeighbour(a, b) && a >= 0 && a < vertexNumber && b >= 0 && b < vertexNumber)
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
    }
}
