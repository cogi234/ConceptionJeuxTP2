using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;

public class monotest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(adj.VertexCount);
    }

    AdjacencyList adj = new AdjacencyList(10);

    // Update is called once per frame
    void Update()
    {
        
    }
}
