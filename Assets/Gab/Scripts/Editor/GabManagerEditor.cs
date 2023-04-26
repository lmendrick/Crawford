using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GabManager))]
public class GabManagerEditor : Editor
{
    private bool _showVariables = true;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        _showVariables = EditorGUILayout.BeginFoldoutHeaderGroup(_showVariables,"Variables");

        if (_showVariables)
        {
            // Get the target script
            GabManager targetScript = (GabManager) target;
            
            if (targetScript.NumberVariables == null)
                targetScript.NumberVariables = new NumberDictionary();

            foreach (KeyValuePair<string, int> kvp in targetScript.NumberVariables)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(kvp.Key);
                EditorGUILayout.LabelField("" + kvp.Value);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}