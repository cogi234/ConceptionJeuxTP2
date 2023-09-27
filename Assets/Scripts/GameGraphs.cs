using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;
using System.Xml;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public class GameGraphs : MonoBehaviour
{
    public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };

    public AdjacencyType graphType;
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

        //We shift the positions a bit
        for (int i = 0; i < vertexNumber; i++)
        {
            vertexPositions[i] += new Vector3(4, 0, 4);
        }

        //We initalise the edges
        for (int i = 0; i < vertexNumber; i++)
        {
            int edge1 = i % 4 != 3 ? i + 1 : -1;
            int edge2 = i + 4 >= vertexNumber ? -1 : i + 4;

            if (edge1 >= 0)
                AddEdge(i, edge1);

            if (edge2 >= 0)
                AddEdge(i, edge2);
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
                    if (neighbour > i)
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
        adjacencyList.AddEdgeBidirectional(a, b);
        adjacencyMatrix.AddEdgeBidirectional(a, b);
        adjacencyListCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));
        adjacencyMatrixCosts.AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));
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
