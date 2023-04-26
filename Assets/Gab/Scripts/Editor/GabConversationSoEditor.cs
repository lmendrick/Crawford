/// <summary>
/// Convetsate.ConversationSOEditor
/// A custom inspector for ConversationSO
/// Has foldout instructions on usage above properties
/// Allows creation of conversation one passage at a time (with named passages)
/// [Add "More" Passage] button to generate easy continuous passages using [[more->passage #]]
/// [Smart Add Passage] button to scan all passages for links without passages and adds the passages
/// </summary>
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GabConversationSo))]
public class GabConversationSoEditor : Editor
{   
    private bool _showInstructions = false;
    private bool _dictionaryModified = false;
    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();
        
        _showInstructions = EditorGUILayout.BeginFoldoutHeaderGroup(_showInstructions, "Instructions");
        if (_showInstructions)
        {
            EditorGUILayout.LabelField("Like Twine/Harlowe Link Format:");
            EditorGUILayout.LabelField("- Use Start as the name of starting passage");
            EditorGUILayout.LabelField("- Each option is enclosed in [[ ]]");
            EditorGUILayout.LabelField("- Either use link text as name, or use ->");
            EditorGUILayout.LabelField("  [[visible link text->passage name]]");
            EditorGUILayout.LabelField("- To just advance with no options:");
            EditorGUILayout.LabelField("  [[more->passage name]]");
            EditorGUILayout.LabelField("- Give no options to end conversation");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        //DrawDefaultInspector();
        
        GabConversationSo targetScript = (GabConversationSo)target;
        
        
        // Create a new list to store the modified key-value pairs
        List<KeyValuePair<string, string>> modifiedDictionary = new List<KeyValuePair<string, string>>();

        if (targetScript.passageDictionary == null)
            targetScript.passageDictionary = new PassageDictionary();
        
        //EditorGUILayout.LabelField("Passages");
        
        // Display the dictionary in the Inspector and allow editing of the keys and values
        foreach (KeyValuePair<string, string> kvp in targetScript.passageDictionary)
        {
            // Create a horizontal layout group for the key-value pair
            EditorGUILayout.BeginHorizontal();
            
            GUILayoutOption[] passageNameOptions = { GUILayout.ExpandWidth(true),
                                                     GUILayout.Height(18) };
            // Create a field for the key and store the modified key in a variable
            string modifiedKey = EditorGUILayout.TextField(kvp.Key, passageNameOptions);
            
            EditorGUILayout.Separator();
            
            if (GUILayout.Button("Remove"))
            {
                Undo.RecordObject(targetScript, "Remove a Passage (Conversation)");
                EditorUtility.SetDirty(targetScript); //ScriptableObjects don't do this automatically
                targetScript.passageDictionary.Remove(kvp.Key);
                break;
            }
            
            EditorGUILayout.EndHorizontal();

            GUILayoutOption[] passageOptions = { GUILayout.ExpandWidth(true), 
                                                 GUILayout.ExpandHeight(true) };
            // Create a field for the value and store the modified value in a variable
            string modifiedValue = EditorGUILayout.TextArea(kvp.Value, passageOptions);

            // Add the modified key-value pair to the list
            modifiedDictionary.Add(new KeyValuePair<string, string>(modifiedKey, modifiedValue));
            
            // End the horizontal layout group
            //EditorGUILayout.EndVertical();

            if (modifiedKey != kvp.Key || modifiedValue != kvp.Value)
            {
                _dictionaryModified = true;
            }
        }

        if (_dictionaryModified)
        {
            Undo.RecordObject(targetScript, "Change Passage (Conversation)");
            EditorUtility.SetDirty(targetScript); //ScriptableObjects don't do this automatically

            //rebuild the original dictionary from modified dictionary
            targetScript.passageDictionary = new PassageDictionary();
            foreach (KeyValuePair<string, string> kvp in modifiedDictionary)
            {
                targetScript.passageDictionary.Add(kvp.Key, kvp.Value);
            }
            _dictionaryModified = false; //reset
        }
        
        // Create a horizontal layout group for the "Add Entry" button
        EditorGUILayout.BeginHorizontal();

        // Create the "Add Entry" button
        if (GUILayout.Button("Add \"more\" Passage"))
            doMoreAdd(targetScript);
        
        
        // Create the "Add Entry" button
        if (GUILayout.Button("Smart Add Passages")) 
            doSmartAdd(targetScript, modifiedDictionary);

        // End the horizontal layout group
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void doMoreAdd(GabConversationSo targetScript)
    {
        Undo.RecordObject(targetScript, "Add a Passage (Conversation)");
        EditorUtility.SetDirty(targetScript); 

        if (targetScript.passageDictionary.Count == 0)
        {
            targetScript.passageDictionary.Add("Start", "\n[[more->passage 1]]");
            return;
        }

        int newPassageNumber = 1;
        // Add a new key-value pair to the dictionary with default values
        bool added = false;
        do
        {
            added = targetScript.passageDictionary.TryAdd($"passage {newPassageNumber}",
                $"\n[[more->passage {newPassageNumber + 1}]]");
            newPassageNumber++;
        } while (!added);
    }
    private int doSmartAdd(GabConversationSo targetScript, List<KeyValuePair<string, string>> modifiedDictionary)
    {
        Undo.RecordObject(targetScript, "Smart Add Passage(s) (Conversation)");
        EditorUtility.SetDirty(targetScript);
        
        if (targetScript.passageDictionary.Count == 0)
        {
            targetScript.passageDictionary.Add("Start", "\n");
            return 1;
        }

        int totalAdded = 0;

        foreach (KeyValuePair<string, string> kvp in modifiedDictionary)
        {
            List<GabLink> links = GabManager.GetLinks(kvp.Value);
            foreach (GabLink link in links)
            {
                string throwaway = "";
                if (!targetScript.passageDictionary.TryGetValue(link.name, out throwaway))
                {
                    totalAdded += targetScript.passageDictionary.TryAdd(link.name, "\n") ? 1 : 0;
                }
            }
        }

        return totalAdded;
    }
}
