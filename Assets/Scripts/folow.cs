using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class folow : MonoBehaviour
{
    GameGraphs graph;
    Vector3[] Liste;
    List<int> ListePath;
    Vector3 targetactuel;
    bool AChanger = false;
    int compteurPath = 0;
    Vector3 DistanceEntre;
    // Start is called before the first frame update
    void Start()
    {
        graph = GameObject.Find("Graphs").GetComponent<GameGraphs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AChanger)
        {
            compteurPath = 0;
            AChanger = false;
            gameObject.transform.position = Liste[ListePath[compteurPath]];
        }
        else
        {
            Debug.Log(ListePath.Count);
            Debug.Log(compteurPath);
            if (Liste != null)
            {





                DistanceEntre = transform.position - Liste[ListePath[compteurPath]];

                if (Math.Abs(DistanceEntre.x) < 0.5f && Math.Abs(DistanceEntre.y) < 0.5f && Math.Abs(DistanceEntre.z) < 0.5f)
                {

                    //changement de target si il y a lieu sinon ne fait rien et se donne sa propre position

                    if (ListePath.Count > compteurPath + 1)
                    {
                        compteurPath++;
                    }

                }

                transform.position = Vector3.MoveTowards(transform.position, Liste[ListePath[compteurPath]], 0.03f);



            }

        }

    }
    public void metterfolow(Vector3[] listePosition, List<int> listePath)
    {
        Debug.Log("a changer");
        Liste = listePosition;
        ListePath = listePath;
        AChanger = true;
    }
}
