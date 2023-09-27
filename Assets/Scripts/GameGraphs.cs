using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathfindingLib;
using System.Xml;
using static UnityEngine.GraphicsBuffer;

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
    private void OnDrawGizmos()
    {
        float Startnode = 0;
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
        Vector3 vecteuractuel = new Vector3(i + Startnode, 0, j + Startnode);
        foreach (KeyValuePair<int, List<Vector3>> entry in vertexPositions)
        {

            Vector3 vecteurPrécédent = new Vector3(0,0,0);
            float ligne = Mathf.Round(Mathf.Sqrt(nombreDeNode));
            float colone = Mathf.Round(Mathf.Sqrt(nombreDeNode));
            int sautDeLigne = (int)Mathf.Round(Mathf.Sqrt(nombreDeNode));
            float lesReste = nombreDeNode - (colone* colone);
            for ( i = 0; i < ligne; i++)
            {

                for ( j = 0; j < colone; j++)
                {

                     vecteuractuel = new Vector3(i + Startnode, 0, j + Startnode);

                

                    Gizmos.DrawSphere(vecteuractuel, 0.3f);
     
                    entry.Value.Add(vecteuractuel);
                    if (j+1 <= colone && j!=0 )
                    {
                        Gizmos.DrawLine(vecteuractuel, vecteurPrécédent);
                    }
                    vecteurPrécédent = vecteuractuel;
                    

                }
             

            }
            for (i = 0; i < entry.Value.Count; i++)
            {

                
               if(i+ sautDeLigne < entry.Value.Count)
                {
                    Gizmos.DrawLine(entry.Value[i], entry.Value[sautDeLigne + i]);
                }


                  
                    
                  


            }
            //for (int T = 0; T < lesReste; T++)
            //{
            //    Vector3 vecteuractuel = new Vector3(i + Startnode, 0, T + Startnode);
            //    Gizmos.DrawSphere(vecteuractuel, 0.3f);
            //    entry.Value.Add(vecteuractuel);
            //}
            Startnode += colone +2;

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
