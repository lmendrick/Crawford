/// Thanks to Odin Inspector documentation for getting this started:
/// https://odininspector.com/tutorials/serialize-anything/serializing-dictionaries
/// 
/// ConversateSerializedDictionary inherits from Dictionary and implements the
/// ISerilizationCallbackReceiver interface from UnityEngine using a pair of lists
/// 
/// PassageDictionary and NumberDictionary are needed for concrete types
/// because Unity won't serialize generic types

using System.Collections.Generic;
using UnityEngine;

namespace Gab.Scripts
{
    public abstract class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
        ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keyData = new List<TKey>();

        [SerializeField] private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }
    

}