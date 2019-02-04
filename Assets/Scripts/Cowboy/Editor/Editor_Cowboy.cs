using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Cowboy))]
public class Editor_Cowboy : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Cowboy cowboy = (Cowboy)target;

        if (GUILayout.Button("Generate collider"))
        {
            cowboy.GenerateCollider();
        }
    }
}
