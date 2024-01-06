using System;
using System.Collections.Generic;

[Serializable]
public abstract class BaseStage {
    public int Id;
    protected  List<QuestArgument> arguments = new();
    public abstract void Execute(List<QuestArgument> arguments);
}