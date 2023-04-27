using UnityEngine;

namespace Gab.Scripts
{
    [CreateAssetMenu(fileName = "New Gab Variable Set", menuName = "Gab/Variable Set", order = 52)]
    [System.Serializable]
    public class GabVariableSetSo : ScriptableObject
    {
        public NumberDictionary numberDictionary;
    }

}