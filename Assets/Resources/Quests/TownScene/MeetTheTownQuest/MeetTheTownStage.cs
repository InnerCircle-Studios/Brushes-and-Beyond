using System;
using System.Collections.Generic;

using UnityEngine;


public class MeetTheTownStage : QuestStage {
    [SerializeField] CharacterAttributes baker;
    [SerializeField] CharacterAttributes villager;
    [SerializeField] CharacterAttributes blacksmith;

    private bool talkedToBaker = false;
    private bool talkedToVillager = false;
    private bool talkedToBlacksmith = false;


    WindowManager wm;

    private void OnEnable() {
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Explore the town");
        wm.SetQuestObjectives($"* Talk to the villagers");
        SetupInitialDialogue();

    }

    private void SetupInitialDialogue() {
        Actor player = GameManager.Instance.GetPlayer();

        EventWrapper bakerTalkEvent = new();
        bakerTalkEvent.AddListener(() => { talkedToBaker = true; AfterBakerTalk(); });
        EventWrapper villagerTalkEvent = new();
        villagerTalkEvent.AddListener(() => { talkedToVillager = true; AfterVillagerTalk(); });
        EventWrapper blacksmithTalkEvent = new();
        blacksmithTalkEvent.AddListener(() => { talkedToBlacksmith = true; InteractionEvents.ShowObject("MTTQs2"); CheckCompleted(); });


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Baker", new(new List<DialogueEntry>() {
                    new(baker, "Cupcakes, bread… maybe a cake... ", DialogueActorMood.NEUTRAL),
                    new(baker, "Oh hi! Sorry, we're still closed. ", DialogueActorMood.HAPPY),
                    new(player, "Closed in the middle of the day? What happend?", DialogueActorMood.NEUTRAL),
                    new(baker, "I'm assuming you are new around here? The creator of our world blessed us with water, but it was stolen! So we have been preparing for when it finally reappears!", DialogueActorMood.HAPPY),
                    new(baker, "If that day will ever come…", DialogueActorMood.SAD),
                    new(player, "I'm sorry to hear that. I hope you get water soon.", DialogueActorMood.SAD),
                    new(baker, "Thank you, I hope so too. ", DialogueActorMood.NEUTRAL),

                    new(baker, "Actually, the blacksmith was asking people about going into the maze to look for the water.", DialogueActorMood.NEUTRAL),
                    new(baker, "We used to go in there a lot but someone broke to bridge!", DialogueActorMood.SAD),
                    new(baker, "Maybe you can help him fix it?", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){
                    new(99, bakerTalkEvent ,true),
                })
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
                }, new List<DialogueAction>(){
                    new(99, villagerTalkEvent ,true),
                })
            },
            { "Blacksmith", new(new List<DialogueEntry>() {
                    new(blacksmith, "Oh hi! Are you the new guy that just got into town? My neighbour across town mentioned you.", DialogueActorMood.NEUTRAL),
                    new(player, "That's me! I am Vin, nice to meet you!", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "I'm the blacksmith of this town, I make all the tools and weapons for the villagers.", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "But right now without the water I can't do anything!", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "I was thinking of going into the maze to look for the water, but the bridge is broken.", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "I need to find a way to fix it but have no idea how!", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "The only clue I have is this note I found on the ground right after the water dissapeared.", DialogueActorMood.NEUTRAL),
                    new(blacksmith, "It reads: "+
                    "\"In the square, where people walk and talk,\n" +
                    "A statue that looks like you, stands without a balk.", DialogueActorMood.NEUTRAL),
                    new(blacksmith, 
                    "Behind this figure, silent and tall,\n"+
                    "The first bucket awaits your call.", DialogueActorMood.NEUTRAL),
                    new(blacksmith, 
                    "Climb to the top of the village, where the air smells sweet,\n"+
                    "Where the bakery makes bread and treats to eat.\n"
                    , DialogueActorMood.NEUTRAL),
                    new(blacksmith, 
                    "Hidden not far, just around its back,\n"+
                    "The second bucket is on this track.\n"
                    , DialogueActorMood.NEUTRAL),
                    new(blacksmith, 
                    "Lastly, find a place where mushrooms grow in a ring,\n"+
                    "Around a special tree, where children play and sing.\n"
                    , DialogueActorMood.NEUTRAL),
                    new(blacksmith, 
                    "There, a child holds the final prize in their glee,\n"+
                    "The last bucket, for you to see.\"\n"
                    , DialogueActorMood.NEUTRAL),
                    new(blacksmith, "I have no idea what it means, but I'm sure it's the key to fixing the bridge!", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){
                    new(99, blacksmithTalkEvent ,true),
                })
            }
        });
    }

    private void OnDisable() {

    }

    private void Start() {
        if (talkedToBaker) {
            AfterBakerTalk();
        }
        if (talkedToVillager) {
            AfterVillagerTalk();
        }
    }

    private void AfterBakerTalk() {
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Baker", new(new List<DialogueEntry>() {
                    new(baker, "Cupcakes, bread… maybe a cake... ", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){
                })
            }
        });
        CheckCompleted();
    }

    private void AfterVillagerTalk() {
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Villager", new(new List<DialogueEntry>() {
                    new(villager, "...", DialogueActorMood.SAD),
                }, new List<DialogueAction>(){
                })
            }
        });
        CheckCompleted();
    }


    private void CheckCompleted() {
        UpdateState();
        if (talkedToBlacksmith) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(talkedToBaker, talkedToVillager, talkedToBlacksmith));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        talkedToBaker = data.TalkedToBaker;
        talkedToVillager = data.TalkedToVillager;
        talkedToBlacksmith = data.TalkedToBlacksmith;
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public bool TalkedToBaker;
        public bool TalkedToVillager;
        public bool TalkedToBlacksmith;

        public StupidJSONWrapper(bool talkedToBaker, bool talkedToVillager, bool talkedToBlacksmith) {
            TalkedToBaker = talkedToBaker;
            TalkedToVillager = talkedToVillager;
            TalkedToBlacksmith = talkedToBlacksmith;
        }

    }



}