using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GabVariableSetSo))]
public class GabVariableSetSoEditor : Editor
{
    private bool _dictionaryModified = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Number Variables");
        
        // Get the target script
        GabVariableSetSo targetScript = (GabVariableSetSo)target;

        // Create a new list to store the modified key-value pairs
        List<KeyValuePair<string, int>> modifiedDictionary = new List<KeyValuePair<string, int>>();
        
        if (targetScript.numberDictionary == null)
            targetScript.numberDictionary = new NumberDictionary();
        
        // Display the dictionary in the Inspector and allow editing of the keys and values
        foreach (KeyValuePair<string, int> kvp in targetScript.numberDictionary)
        {
            EditorGUILayout.BeginHorizontal();

            // Create a field and store modification in a variable
            string modifiedKey = EditorGUILayout.TextField(kvp.Key);
            int modifiedValue = EditorGUILayout.IntField(kvp.Value);

            modifiedDictionary.Add(new KeyValuePair<string, int>(modifiedKey, modifiedValue));

            //check for changes and remember
            if (modifiedKey != kvp.Key || modifiedValue != kvp.Value)
                _dictionaryModified = true;
            
            if (GUILayout.Button("Remove"))
            {
                Undo.RecordObject(targetScript, "Remove a Variable (Conversate)");
                EditorUtility.SetDirty(targetScript); //ScriptableObjects don't do this automatically
                targetScript.numberDictionary.Remove(kvp.Key);
                break;
            }
            
            EditorGUILayout.EndHorizontal();
        }

        if (_dictionaryModified)
        {
            Undo.RecordObject(targetScript, "Change Variable (Conversate)");
            EditorUtility.SetDirty(targetScript); //ScriptableObjects don't do this automatically

            //rebuild the original dictionary from modified dictionary
            targetScript.numberDictionary = new NumberDictionary();
            foreach (KeyValuePair<string, int> kvp in modifiedDictionary)
            {
                targetScript.numberDictionary.Add(kvp.Key, kvp.Value);
            }
            _dictionaryModified = false; //reset
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Entry"))
        {
            // Add a new key-value pair to the dictionary with default value and numbered variable name
            int endNumber = 1;
            while (!targetScript.numberDictionary.TryAdd("num" + endNumber, 0) && endNumber <= 25)
            {
                endNumber++;
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}