using System;


[Serializable]
public class DialogueEntry {
    public Actor Actor;
    public string Dialogue;
    public DialogueActorMood ActorMood;


}

[Serializable]
public enum DialogueActorMood {
    HAPPY,
    NEUTRAL,
    SAD,
    ANGRY
}
