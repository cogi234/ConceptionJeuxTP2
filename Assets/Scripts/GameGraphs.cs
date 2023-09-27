using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;
using System.Xml;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

public class GameGraphs : MonoBehaviour
{
    public enum AdjacencyType { List, Matrix, ListCosts, MatrixCosts };

    public Dictionary<int,List<Vector3>> vertexPositions;
    [SerializeField] AdjacencyType adjacencyType;
    [SerializeField] int nombreDeNode = 12;
    int VecteurRendu = 1;
    AdjacencyList adjacencyList;
    AdjacencyMatrix adjacencyMatrix;
    AdjacencyListCosts adjacencyListCosts;
    AdjacencyMatrixCosts adjacencyMatrixCosts;

    private void Awake()
    {
        
      
       
    }

    void CreateNodeMap2D(KeyValuePair<int, List<Vector3>> entry, Vector3Int startingPosition, Vector3Int mapSize, List<Vector3> emptyNodes)
    {
        float ligne = Mathf.Round(Mathf.Sqrt(nombreDeNode));
        float colone = Mathf.Round(Mathf.Sqrt(nombreDeNode));
        int sautDeLigne = (int)Mathf.Round(Mathf.Sqrt(nombreDeNode));
        float lesReste = nombreDeNode - (colone * colone);

        for (int x = startingPosition.x; x < startingPosition.x + mapSize.x; ++x)
        {
            for (int z = startingPosition.z; z < startingPosition.z + mapSize.z; ++z)
            {
                Vector3 actualVector = new Vector3(x, 0, z);
                if (!emptyNodes.Contains(new Vector3(x, 0, z)))
                {
                    Gizmos.DrawSphere(actualVector, 0.2f);

                    entry.Value.Add(actualVector);
                    if (x + 1 < colone + startingPosition.x && !emptyNodes.Contains(actualVector + new Vector3(1, 0, 0)))
                    {
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(1, 0, 0));
                    }
                    if (z + 1 < colone + startingPosition.z && !emptyNodes.Contains(actualVector + new Vector3(0, 0, 1)))
                    {
                        Gizmos.DrawLine(actualVector, actualVector + new Vector3(0, 0, 1));
                    }
                }
            }
        }
        //for (int T = 0; T < lesReste; T++)
        //{
        //    Vector3 vecteuractuel = new Vector3(i + Startnode, 0, T + Startnode);
        //    Gizmos.DrawSphere(vecteuractuel, 0.3f);
        //    entry.Value.Add(vecteuractuel);
        //}
    }
    private void OnDrawGizmos()
    {

        List<Vector3> emptyNodes = new List<Vector3>();
        int Startnode = 0;
        Vector3Int size = new Vector3Int(3, 1, 3);
        vertexPositions = new Dictionary<int, List<Vector3>>();
        vertexPositions.Add(1, new List<Vector3>()); //adjacencyList
        vertexPositions.Add(2, new List<Vector3>());// adjency matrix
        vertexPositions.Add(3, new List<Vector3>());// adjencyListCosts
        vertexPositions.Add(4, new List<Vector3>());//AdjencyMatrixcost
        adjacencyList = new AdjacencyList(nombreDeNode);
        adjacencyMatrix = new AdjacencyMatrix(nombreDeNode);
        adjacencyListCosts = new AdjacencyListCosts(nombreDeNode);
        adjacencyMatrixCosts = new AdjacencyMatrixCosts(nombreDeNode);
        int i = 0;
        int j = 0;
        foreach (KeyValuePair<int, List<Vector3>> entry in vertexPositions)
        {
            Vector3Int vecteuractuel = new Vector3Int(i + Startnode, 0, j);
            CreateNodeMap2D(entry, vecteuractuel, size, emptyNodes);
            Startnode += size.x;
        }
       
    }

    void ajouternodListe()
    {

        for(int i = 0;i<nombreDeNode;i++)
        {

            if(i - 1 >= 0)
            {
                adjacencyList.AddEdge(i, i-1);
            }
            if (i +1 <= nombreDeNode)
            {
                adjacencyList.AddEdge(i, i + 1);
            }


        }
       
        
      
    }


}
