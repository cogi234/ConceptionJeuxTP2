using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class folow : MonoBehaviour
{
    GameGraphs graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = GameObject.Find("Graphs").GetComponent<GameGraphs>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, graph., .03);
    }
}
