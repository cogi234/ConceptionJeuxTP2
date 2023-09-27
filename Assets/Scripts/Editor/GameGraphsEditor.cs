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
            GUILayout.Label("First vertex:", GUILayout);
            selectedFirstVertex = GUILayout.TextField(playerName);
            GUILayout.EndHorizontal();
        }

    }
}
