using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SerializableDict<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver {
    
    [SerializeField] private List<Tkey> keys = new();
    [SerializeField] private List<Tvalue> values = new();
    
    public void OnBeforeSerialize() {
        keys.Clear();
        values.Clear();
        
        foreach (KeyValuePair<Tkey, Tvalue> pair in this) {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize() {
        Clear();
        
        if(keys.Count != values.Count) {
            Logger.LogError("SerializableDict", $"Couldn't deserialize dictionary, amount of keys {keys.Count} doesn't match values count {values.Count}"); 
        }

        for (int i = 0; i < keys.Count; i++) {
            Add(keys[i], values[i]);
        }
    }


}