using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;

public class GameGraphs : MonoBehaviour
{
    public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };

    [SerializeField] List<Vector3> vertexPositions;
    [SerializeField] AdjacencyType adjacencyType;
    
    AdjacencyList adjacencyList;
    AdjacencyMatrix adjacencyMatrix;
    AdjacencyListCosts adjacencyListCosts;
    AdjacencyMatrixCosts adjacencyMatrixCosts;

    private void Awake()
    {
        adjacencyList = new AdjacencyList(vertexPositions.Count);
        adjacencyMatrix = new AdjacencyMatrix(vertexPositions.Count);
        adjacencyListCosts = new AdjacencyListCosts(vertexPositions.Count);
        adjacencyMatrixCosts = new AdjacencyMatrixCosts(vertexPositions.Count);
    }
    
}
