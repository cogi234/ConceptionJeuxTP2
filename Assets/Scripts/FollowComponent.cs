using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowComponent : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] GameGraphs graph;
    List<int> path;
    Vector2Int position = Vector2Int.zero;

    private void Start()
    {
        //If the graph changes, we have to recalculate the path
        graph.OnGraphChange.AddListener(ReloadPath);

        //We start with a random path
        RandomPath();
    }

    void Update()
    {
        //If we have a path and we aren't at the end
        if (path != null && path.Count > 0) 
        {
            transform.position = Vector3.MoveTowards(transform.position, graph.GetPosition(path[0]), speed * Time.deltaTime);
            //If we reached the point, we switch to the next
            if (Vector3.Distance(transform.position, graph.GetPosition(path[0])) < 0.1f)
            {
                position = new Vector2Int(path[0] % graph.width, path[0] / graph.width);
                path.RemoveAt(0);
            }
        }
        //find new path
        else
        {
            RandomPath();
        }

    }

    public void ReloadPath()
    {
        graph.start = position;
        graph.FindPath();
        path = new List<int>(graph.path);
        transform.position = graph.GetPosition(path[0]);
    }

    public void RandomPath()
    {
        System.Random rnd = new System.Random();
        graph.start = position;
        graph.end = new Vector2Int(rnd.Next(graph.width), rnd.Next(graph.height));

        graph.FindPath();
        path = new List<int>(graph.path);
        transform.position = graph.GetPosition(path[0]);
    }
}
