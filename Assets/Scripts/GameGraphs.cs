using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;
using UnityEngine.Events;
using Unity.VisualScripting;

public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };
public enum AlgorithmType { BFS, Dijkstra, Astar };

public class GameGraphs : MonoBehaviour
{
    [SerializeField] AdjacencyType graphType;
    [SerializeField] AlgorithmType algorithmType;

    public Vector2Int start, end;

    public int width, height;
    [SerializeField] float distanceBetweenVertices;

    bool generated = false;
    int vertexNumber;
    Vector3[] vertexPositions;
    IGraphRepresentation[] graphs;
    [HideInInspector] public List<int> path = new List<int>();
    [HideInInspector] public UnityEvent OnGraphChange = new UnityEvent();

    private void Awake()
    {
        if (OnGraphChange == null)
            OnGraphChange = new UnityEvent();

        Generate();
    }

    public void Generate()
    {
        //We initialise the graphs
        vertexNumber = width * height;
        vertexPositions = new Vector3[vertexNumber];
        graphs = new IGraphRepresentation[4];
        graphs[0] = new AdjacencyList(vertexNumber);
        graphs[1] = new AdjacencyMatrix(vertexNumber);
        graphs[2] = new AdjacencyListCosts(vertexNumber);
        graphs[3] = new AdjacencyMatrixCosts(vertexNumber);

        //Then we generate a rectangle of nodes
        for (int i = 0; i < vertexNumber; i++)
        {
            int x = i % width;
            int y = i / width;
            vertexPositions[i] = new Vector3(x * distanceBetweenVertices + 10, 0, y * distanceBetweenVertices + 10);

            //We connect the nodes to the four adjacent nodes
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

        OnGraphChange.Invoke();
    }

    public void FindPath()
    {
        //We use the appropriate algorithm.
        switch (algorithmType)
        {
            case AlgorithmType.BFS:
                {
                    path = Algorithms.BFS(graphs[(int)graphType], start.y * width + start.x, end.y * width + end.x);
                    LogPath();
                    break;
                }
            case AlgorithmType.Dijkstra:
                {
                    if (graphs[(int)graphType] is AdjacencyList || graphs[(int)graphType] is AdjacencyMatrix)
                    {
                        Debug.LogWarning("Wrong graph type for Dijkstra.");
                        return;
                    }
                    path = Algorithms.Dijkstra((IWeightedGraphRepresentation)graphs[(int)graphType], start.y * width + start.x, end.y * width + end.x);
                    LogPath();
                    break;
                }
            case AlgorithmType.Astar:
                {
                    if (graphs[(int)graphType] is AdjacencyList || graphs[(int)graphType] is AdjacencyMatrix)
                    {
                        Debug.LogWarning("Wrong graph type for Dijkstra.");
                        return;
                    }
                    path = Algorithms.AStar((int a, int b) => Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])),
                        (IWeightedGraphRepresentation)graphs[(int)graphType], start.y * width + start.x, end.y * width + end.x);
                    LogPath();
                    break;
                }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return vertexPositions[i];
    }
    public Vector3 GetPosition(int x, int y)
    {
        return vertexPositions[y * width + x];
    }

    private void OnDrawGizmos()
    {
        //We only draw if the graph has been generated
        if (generated)
        {

            //Show the edges first
            for (int i = 0; i < vertexNumber; i++)
            {
                //Draw links
                Gizmos.color = Color.white;
                IEnumerable<int> neighbours = graphs[(int)graphType].GetNeighbours(i);
                foreach (int neighbour in neighbours)
                {
                    //We color the path
                    if (path.Contains(i))
                    {
                        int j1 = path.FindIndex((int a) => a == i) + 1;
                        int j2 = path.FindIndex((int a) => a == i) - 1;
                        if (j1 < path.Count && j1 >= 0 && path[j1] == neighbour)
                            Gizmos.color = Color.green;
                        else if (j2 < path.Count && j2 >= 0 && path[j2] == neighbour)
                            Gizmos.color = Color.green;
                        else
                            Gizmos.color = Color.white;
                    }
                    else
                        Gizmos.color = Color.white;

                    //To avoid drawing the same link multiple times
                    Gizmos.DrawLine(vertexPositions[i], vertexPositions[neighbour]);
                }
            }

            //Show the nodes themselves second
            for (int i = 0; i < vertexNumber; i++)
            {
                if (path.Contains(i))
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / (float)(vertexNumber - 1));
                Gizmos.DrawSphere(vertexPositions[i], 0.5f);
            }

        }
    }

    public void AddEdge(int a, int b)
    {
        if (!graphs[0].HasNeighbour(a, b) && a >= 0 && a < vertexNumber && b >= 0 && b < vertexNumber)
        {
            (graphs[0] as AdjacencyList).AddEdgeBidirectional(a, b);
            (graphs[1] as AdjacencyMatrix).AddEdgeBidirectional(a, b);
            (graphs[2] as AdjacencyListCosts).AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));
            (graphs[3] as AdjacencyMatrixCosts).AddEdgeBidirectional(a, b, Mathf.FloorToInt(Vector3.Distance(vertexPositions[a], vertexPositions[b])));

            OnGraphChange.Invoke();
        }
    }

    public void RemoveEdge(int a, int b)
    {
        if (graphs[0].HasNeighbour(a, b) && a >= 0 && a < vertexNumber && b >= 0 && b < vertexNumber)
        {
            foreach (IGraphRepresentation g in graphs)
            {
                g.RemoveEdge(a, b);
                g.RemoveEdge(b, a);
            }

            OnGraphChange.Invoke();
        }
    }

    private void LogPath()
    {
        string pathString = "Path: ";
        for (int i = 0; i < path.Count; i++)
        {
            pathString += $"{path[i]}" + (i == path.Count - 1 ? "" : ", ");
        }

        Debug.Log(pathString);
    }
}
