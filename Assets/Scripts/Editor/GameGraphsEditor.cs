using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGraphs))]
public class GameGraphsEditor : Editor
{

    int selectedFirstVertex = 0;
    int selectedSecondVertex = 0;

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        //On peut juste editer les graphs en jeu
        if (EditorApplication.isPlaying)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Vertex 1:", GUILayout.Width(100f));
            selectedFirstVertex = EditorGUILayout.IntField(selectedFirstVertex);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Vertex 2:", GUILayout.Width(100f));
            selectedSecondVertex = EditorGUILayout.IntField(selectedSecondVertex);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Add Edge"))
            {
                (target as GameGraphs).AddEdge(selectedFirstVertex, selectedSecondVertex);
            }

            if (GUILayout.Button("Remove Edge"))
            {
                (target as GameGraphs).RemoveEdge(selectedFirstVertex, selectedSecondVertex);
            }
        }

    }
}
