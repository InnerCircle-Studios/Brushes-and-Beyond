using System;
using System.Collections.Generic;

using UnityEngine;


public class MeetTheTownStage : QuestStage {
    [SerializeField] CharacterAttributes baker;
    [SerializeField] CharacterAttributes villager;
    [SerializeField] CharacterAttributes blacksmith;

    WindowManager wm;

    private void OnEnable() {
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Explore the town");
        wm.SetQuestObjectives($"* Talk to the villagers");
        LoadInitialDialogue();

    }

    private void LoadInitialDialogue() {
        Actor player = GameManager.Instance.GetPlayer();
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Baker", new(new List<DialogueEntry>() {
                    new(baker, "Cupcakes, bread… maybe a cake... ", DialogueActorMood.NEUTRAL),
                    new(baker, "Oh hi! Sorry, we're still closed. ", DialogueActorMood.HAPPY),
                    new(player, "Closed in the middle of the day? What happend?", DialogueActorMood.NEUTRAL),
                    new(baker, "I'm assuming you are new arround here? The creator of our world blessed us with water, but it was stolen! So we have been preparing for when it finally reappears!", DialogueActorMood.HAPPY),
                    new(baker, "If that day will ever come…", DialogueActorMood.SAD),
                    new(player, "I'm sorry to hear that. I hope you get water soon.", DialogueActorMood.SAD),
                    new(baker, "Thank you, I hope so too. ", DialogueActorMood.NEUTRAL),

                    new(baker, "Actually, the blacksmith was asking people about going into the maze to look for the water.", DialogueActorMood.NEUTRAL),
                    new(baker, "We used to go in there a lot but someone broke to bridge!", DialogueActorMood.SAD),
                    new(baker, "Maybe you can help him fix it?", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){})
            },
            { "Villager", new(new List<DialogueEntry>() {
                    new(villager, "...", DialogueActorMood.SAD),
                    new(villager, "Oh hello, I've never seen you before. Did you move here?", DialogueActorMood.NEUTRAL),
                    new(player, " Uh yeah, I arrived here today.", DialogueActorMood.NEUTRAL),
                    new(villager, "If I were you I'd leave as soon as possible , this is a waterless town.", DialogueActorMood.SAD),
                    new(player, "What do you mean?", DialogueActorMood.CONFUSED),
                    new(villager, "We have no water, we have no food, we have no life here.", DialogueActorMood.SAD),
                    new(villager, "I'm sorry, I'm just a bit down. My daughter recently dissapeared.", DialogueActorMood.SAD),
                    new(villager, "She said she was going to find a bucket and never returned...", DialogueActorMood.SAD),
                    new(villager, "Anyways... Sorry for bothering you. I hope you have a nice stay in our town.", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){})
            },
            { "Blacksmith", new(new List<DialogueEntry>() {
                    new(blacksmith, "This spot looks unfinished...", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){})
            }
        });
    }

    private void OnDisable() {

    }

    private void Start() {

    }




    private void CheckCompleted() {
        UpdateState();
        if (true) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper());
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public int PaintCounter;
        public StupidJSONWrapper() { }
        public StupidJSONWrapper(int paintCounter) {
            PaintCounter = paintCounter;
        }
    }



}