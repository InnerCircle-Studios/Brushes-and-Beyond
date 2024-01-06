using System.Collections.Generic;

public class TutorialQuest : BaseQuest {
    public override void Initialize() {
        Id = 1;
        Name = "Tutorial Quest";
        Description = "This is a tutorial quest";
        stages = new List<BaseStage> {
            // new tutorialStage1(),
            // new tutorialStage2(),
            // new tutorialStage3()
        };
    }
}