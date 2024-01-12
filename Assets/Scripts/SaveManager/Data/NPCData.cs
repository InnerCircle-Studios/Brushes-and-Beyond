using System;
using System.Collections.Generic;

[Serializable]
public class NPCData {
    public SerializableDict<string, CharacterAttributes> NPCvalues = new();
    
    public NPCData() { }
}