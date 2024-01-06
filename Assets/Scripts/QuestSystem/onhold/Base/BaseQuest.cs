using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public abstract class BaseQuest {
    public int Id;
    public string Name;
    public string Description;
    protected int currentStage;
    protected List<BaseStage> stages = new();
    protected List<QuestArgument> arguments = new();

    protected BaseQuest() {
        Initialize();
    }
    protected BaseQuest(List<QuestArgument> arguments) {
        this.arguments = arguments;
        Initialize();
    }


    public abstract void Initialize();


    public void SetStage(int stage) {
        currentStage = stage;
        stages.Where(s => s.Id == stage).FirstOrDefault().Execute(arguments);
    }
}