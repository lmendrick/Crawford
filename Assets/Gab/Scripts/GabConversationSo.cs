using UnityEngine;

namespace Gab.Scripts
{
    [CreateAssetMenu(fileName = "New Gab Conversation", menuName = "Gab/Conversation", order = 51)]
    [System.Serializable]
    public class GabConversationSo : ScriptableObject
    {
        public PassageDictionary passageDictionary;
    }
    
}