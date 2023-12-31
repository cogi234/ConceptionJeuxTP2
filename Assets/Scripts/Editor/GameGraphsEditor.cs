using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGraphs))]
public class GameGraphsEditor : Editor
{
    //This is a script to allow us to edit the edges of the graph
    int firstX = 0;
    int firstY = 0;
    int secondX = 0;
    int secondY = 0;

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GameGraphs graph = (GameGraphs)target;

        if (GUILayout.Button("Generate Graph"))
        {
            graph.Generate();
        }
        if (GUILayout.Button("Find Path"))
        {
            graph.FindPath();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Vertex 1:");
        GUILayout.Label("X:");
        firstX = EditorGUILayout.IntField(firstX);
        GUILayout.Label("Y:");
        firstY = EditorGUILayout.IntField(firstY);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Vertex 2:");
        GUILayout.Label("X:");
        secondX = EditorGUILayout.IntField(secondX);
        GUILayout.Label("Y:");
        secondY = EditorGUILayout.IntField(secondY);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add Edge"))
        {
            graph.AddEdge(firstY * graph.width + firstX, secondY * graph.width + secondX);
        }

        if (GUILayout.Button("Remove Edge"))
        {
            graph.RemoveEdge(firstY * graph.width + firstX, secondY * graph.width + secondX);
        }

    }
}
