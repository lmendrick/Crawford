using System;
using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;

public class GabController : MonoBehaviour
{
    [SerializeField] private GabConversationSo _conversation;
    void Update()
    {
        if (_conversation != null && Input.GetKeyDown(KeyCode.Return)) 
            GabManager.StartNew(_conversation);
        
        if (!GabManager.IsShowingConversation) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
            GabManager.Advance();
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            GabManager.SelectNext();
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            GabManager.SelectPrevious();
        else if (Input.GetKeyDown(KeyCode.Escape))
            GabManager.End();
        else if (Input.GetKeyDown(KeyCode.C))
            GabManager.SetValue("cash", GabManager.GetValue("cash") + 15);
    }

    //Event Examples
    
    //Register for GabManager events that you want to respond to when first enabled
    private void OnEnable()
    {
        GabManager.OnConversationStarted += OnConversationStarted;
        GabManager.OnConversationEnded += OnConversationEnded;
        GabManager.OnVariableChanged += OnVariableChanged;
    }

    //You should also unregister during disable (or there is potential for memory leaks!)
    private void OnDisable()
    {
        GabManager.OnConversationStarted -= OnConversationStarted;
        GabManager.OnConversationEnded -= OnConversationEnded;
        GabManager.OnVariableChanged -= OnVariableChanged;
    }

    //You can respond immediately when a variable was changed during a conversation
    private void OnVariableChanged(string variableName, int value)
    {
        //print("Variable was changed. " + variableName + " has a value of " + value);
        if (variableName == "cash" && value <= 100)
            print("You are out of cash!");
    }

    //You can respond when a conversation has ended (giving control back to the player, for instance)
    //and you can also check if any variable changes were made in case you want to respond to the changes
    private void OnConversationEnded(bool variableChangesMade)
    {
        print("Gab Conversation Ended. At least one gab variable was changed: " + variableChangesMade);
    }

    //You can respond to when a conversation started (and find out which one, by name)
    //You might want to suspend player movement when a conversation is started, for instance.
    private void OnConversationStarted(string conversationName)
    {
        print("Gab Conversation Started: " + conversationName);
    }
}
