using PathfindingLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class testdll : MonoBehaviour
{
    private AdjacencyListCosts adjacencyListCosts = new AdjacencyListCosts(10);
  
 
    


        adjacencyListCosts.AddEdgeBidirectional(0, 1, 1);
        adjacencyListCosts.AddEdgeBidirectional(0, 2, 1);
        adjacencyListCosts.AddEdgeBidirectional(1, 3, 1);
        adjacencyListCosts.AddEdgeBidirectional(2, 4, 1);
        adjacencyListCosts.AddEdgeBidirectional(2, 6, 1);
        adjacencyListCosts.AddEdgeBidirectional(3, 4, 1);
        adjacencyListCosts.AddEdgeBidirectional(3, 5, 1);
        adjacencyListCosts.AddEdgeBidirectional(4, 7, 1);
        adjacencyListCosts.AddEdgeBidirectional(4, 8, 1);
        adjacencyListCosts.AddEdgeBidirectional(5, 9, 1);
        adjacencyListCosts.AddEdgeBidirectional(6, 7, 1);
        adjacencyListCosts.AddEdgeBidirectional(7, 10, 1);

        adjacencyListCosts.AddEdgeBidirectional(8, 10, 1);
        adjacencyListCosts.AddEdgeBidirectional(9, 10, 1);
    

    

}
