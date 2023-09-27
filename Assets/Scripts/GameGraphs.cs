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

            //Show the nodes themselves and the link to their neighbours
            for (int i = 0; i < vertexNumber; i++)
            {
                Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / (float)(vertexNumber - 1));
                Gizmos.DrawSphere(vertexPositions[i], 0.5f);

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
        }


        /*
        float Startnode = 0;
        int i = 0;
        int j = 0;
        Vector3 vecteuractuel = new Vector3(i + Startnode, 0, j + Startnode);
        foreach (KeyValuePair<int, List<Vector3>> entry in vertexPositions)
        {

            Vector3 vecteurPrécédent = new Vector3(0,0,0);
            float ligne = Mathf.Round(Mathf.Sqrt(vertexNumber));
            float colone = Mathf.Round(Mathf.Sqrt(vertexNumber));
            int sautDeLigne = (int)Mathf.Round(Mathf.Sqrt(vertexNumber));
            float lesReste = vertexNumber - (colone* colone);
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
        */
       
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
