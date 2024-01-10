using System;

[Serializable]
public class NPCData {
    public SerializableDict<string, CharacterAttributes> NPCvalues = new();

    public NPCData() { }
}