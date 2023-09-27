using PathfindingLib;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public enum AdjacencyType { List ,Matrix, ListCosts, MatrixCosts}
public class GameGrap2 : MonoBehaviour
{

    [SerializeField] AdjacencyType graphType;
    [SerializeField] List<Nodes> nodes;
    [SerializeField] int distanceBetweenGraph;
    public List<Graph> graphs;
    List<IGraphRepresentation> graphsRepresentation = new List<IGraphRepresentation>();


    private void OnDrawGizmos()
    {
        Vector2Int startPostion = new Vector2Int();
        for (int i = 0; i < graphs.Count; ++i)
        {
            switch (graphs[i].type)
            {
                case AdjacencyType.List:
                    graphsRepresentation.Insert(i, new AdjacencyMatrix(graphs[i].size.x * graphs[i].size.y));
                    break;
                case AdjacencyType.Matrix:
                    graphsRepresentation.Insert(i, new AdjacencyMatrixCosts(graphs[i].size.x * graphs[i].size.y));
                    break;
                case AdjacencyType.ListCosts:
                    graphsRepresentation.Insert(i, new AdjacencyList(graphs[i].size.x * graphs[i].size.y));
                    break;
                case AdjacencyType.MatrixCosts:
                    graphsRepresentation.Insert(i, new AdjacencyListCosts(graphs[i].size.x * graphs[i].size.y));
                    break;
            }
            CreateNodeMap2D(startPostion, new Vector2Int(graphs[i].size.x, graphs[i].size.y), graphs[i].emptyNodes, graphsRepresentation[i],
                graphs[i].start, graphs[i].end);
            startPostion += new Vector2Int(graphs[i].size.x + distanceBetweenGraph, 0);
        }

    }
    public void AddEdge(int a, int b, IGraphRepresentation graph)
    {
        graph.AddEdge(a, b);
    }
    public void AddBidirectionalEdge(int a, int b, IGraphRepresentation graph)
    {
        graph.AddEdge(a, b);
        graph.AddEdge(b, a);
    }

    public void RemoveEdge(int a, int b, IGraphRepresentation graph)
    {
        graph.RemoveEdge(a, b);
    }
    public void RemoveBidirectionalEdge(int a, int b, IGraphRepresentation graph)
    {
        graph.RemoveEdge(a, b);
        graph.RemoveEdge(b, a);
    }
    void CreateNodeMap2D(Vector2Int startingPosition, Vector2Int mapSize, List<Vector2Int> emptyNodes, IGraphRepresentation graph, Vector2Int start, Vector2Int end)
    {

        for (int x = 0; x < mapSize.x; ++x)
        {
            for (int y = 0; y < mapSize.y; ++y)
            {
                Vector3 actualVector = new Vector3(x + startingPosition.x, 0, y + startingPosition.y);
                if (!emptyNodes.Contains(new Vector2Int(x, y)))
                {
                    //draw nodes
                    if (start == new Vector2Int(x, y) || end == new Vector2Int(x, y))
                    {
                        Gizmos.color = UnityEngine.Color.red;
                    } 
                    else
                    {
                        Gizmos.color = UnityEngine.Color.white;
                    }
                    Gizmos.DrawSphere(actualVector, 0.2f);

                    //draw edges
                    Gizmos.color = UnityEngine.Color.white;
                    if (x + 1 < mapSize.x && !emptyNodes.Contains(new Vector2Int(x + 1, y)))
                    {
                        AddEdge(y * (1 + x), y * (1 + x) + 1, graph);
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(1, 0, 0));
                    }
                    if (y + 1 < mapSize.y && !emptyNodes.Contains(new Vector2Int(x, y + 1)))
                    {
                        AddEdge(y * (1 + x), y * (1 + x + 1), graph);
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(0, 0, 1));
                    }
                }
            }
        }
    }
}
