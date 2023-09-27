using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    public AdjacencyType type;
    public Vector2Int size = new Vector2Int();
    public List<Vector2Int> emptyNodes = new List<Vector2Int>();
    public Vector2Int start = new Vector2Int();
    public Vector2Int end = new Vector2Int();
}
