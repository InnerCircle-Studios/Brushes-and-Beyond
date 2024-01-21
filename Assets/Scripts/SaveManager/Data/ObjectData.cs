using System;

[Serializable]
public class ObjectData {
    public SerializableDict<string, SerializableDict<string, DialogueSet>> InteractionData = new();
    public SerializableDict<string, bool> Toggles = new();
}