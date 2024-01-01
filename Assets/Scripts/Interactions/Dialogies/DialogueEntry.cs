using System;


[Serializable]
public class DialogueEntry {
    public Actor Actor;
    public string Dialogue;
    public DialogueActorMood ActorMood;

    public DialogueEntry(Actor actor, string dialogue, DialogueActorMood actorMood) {
        Actor = actor;
        Dialogue = dialogue;
        ActorMood = actorMood;
    }
}

[Serializable]
public enum DialogueActorMood {
    HAPPY,
    NEUTRAL,
    SAD,
    ANGRY
}
