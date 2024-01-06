using System;

public class QuestArgument {
    public object Value;
    public Type Type;
    public string Name;

    public QuestArgument(object value,Type type, string name) {
        Value = value;
        Type = type;
        Name = name;
    }

}