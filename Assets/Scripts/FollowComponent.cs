using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FollowComponent : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] GameGraphs graph;
    List<int> path;
    int compteurPath = 0;

    private void Start()
    {
        //If the graph changes, we have to recalculate the path
        graph.OnGraphChange.AddListener(ChangePath);

        ChangePath();
    }

    void Update()
    {
        //If we have a path and we aren't at the end
        if (path != null && compteurPath < path.Count) 
        {
            transform.position = Vector3.MoveTowards(transform.position, graph.GetPosition(path[compteurPath]), speed * Time.deltaTime);
            //If we reached the point, we switch to the next
            if (Vector3.Distance(transform.position, graph.GetPosition(path[compteurPath])) < 0.5f)
            {
                    compteurPath++;
            }
        }
    }

    public void ChangePath()
    {
        graph.FindPath();
        path = graph.path;

        compteurPath = 0;
        transform.position = graph.GetPosition(path[0]);
    }
}
