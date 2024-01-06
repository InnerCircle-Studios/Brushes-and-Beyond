using System.Collections.Generic;

using UnityEngine;

public class TutorialStage1 : BaseStage {
    public override void Execute(List<QuestArgument> arguments) {
        this.arguments = arguments;
        GameManager gm = arguments.Find(a => a.Name == "GameManager").Value as GameManager;
    }



}

public class TutorialStage2 : BaseStage {
    public override void Execute(List<QuestArgument> arguments) {
        this.arguments = arguments;

    }



}

public class TutorialStage3 : BaseStage {
    public override void Execute(List<QuestArgument> arguments) {
        this.arguments = arguments;

    }



}