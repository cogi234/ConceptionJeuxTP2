using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponent : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] GameGraphs graph;
    List<int> path;
    int pathCounter = 0;

    private void Start()
    {
        //If the graph changes, we have to recalculate the path
        graph.OnGraphChange.AddListener(ChangePath);

        ChangePath();
    }

    void Update()
    {
        //If we have a path and we aren't at the end
        if (path != null && pathCounter < path.Count) 
        {
            transform.position = Vector3.MoveTowards(transform.position, graph.GetPosition(path[pathCounter]), speed * Time.deltaTime);
            //If we reached the point, we switch to the next
            if (Vector3.Distance(transform.position, graph.GetPosition(path[pathCounter])) < 0.1f)
            {
                    pathCounter++;
            }
        }
    }

    public void ChangePath()
    {
        graph.FindPath();
        path = graph.path;

        pathCounter = 0;
        transform.position = graph.GetPosition(path[0]);
    }
}
