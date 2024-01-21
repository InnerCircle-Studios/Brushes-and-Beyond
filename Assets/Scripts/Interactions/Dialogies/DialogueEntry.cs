using System;


[Serializable]
public class DialogueEntry {
    public CharacterAttributes Actor;
    public string Dialogue;
    public DialogueActorMood ActorMood;

    public DialogueEntry(CharacterAttributes actorAttributes, string dialogue, DialogueActorMood actorMood) {
        Actor = actorAttributes;
        Dialogue = dialogue;
        ActorMood = actorMood;
    }

    public DialogueEntry(Actor actor, string dialogue, DialogueActorMood actorMood) : this(actor.GetAttrubuteManager().GetAttributeContainer(), dialogue, actorMood) { }

}

[Serializable]
public enum DialogueActorMood {
    HAPPY,
    NEUTRAL,
    SAD,
    ANGRY,
    CONFUSED,
    SCARED,
}
