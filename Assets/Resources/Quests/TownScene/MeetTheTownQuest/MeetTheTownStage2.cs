using System;
using System.Collections.Generic;

using UnityEngine;


public class MeetTheTownStage2 : QuestStage {
    [SerializeField] CharacterAttributes baker;
    [SerializeField] CharacterAttributes villager;
    [SerializeField] CharacterAttributes blacksmith;

    private int bucketsFound = 0;


    WindowManager wm;

    private void OnEnable() {
        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Explore the town");
        wm.SetQuestObjectives($"* Find the hidden paintbuckets");
        SetupInitialDialogue();
        InteractionEvents.OnPaintBucketActivated += OnPaintBucketActivated;
    }

    private void SetupInitialDialogue() {
        Actor player = GameManager.Instance.GetPlayer();


        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Baker", new(new List<DialogueEntry>() {
                    new(baker, "No I haven't seen a bucket... ", DialogueActorMood.NEUTRAL),
                }, new List<DialogueAction>(){
                })
            },
            { "Villager", new(new List<DialogueEntry>() {
                    new(villager, "...", DialogueActorMood.SAD),
                    new(player, "I'm searching for some buckets filled with paint. Have you seen any?", DialogueActorMood.NEUTRAL),
                    new(villager, "Not you as well!", DialogueActorMood.SAD)
                }, new List<DialogueAction>(){})
            },
            { "Blacksmith", new(new List<DialogueEntry>() {
                    new(blacksmith, "Do you want to hear the note again?", DialogueActorMood.NEUTRAL),
                    new(blacksmith,
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
                }, new List<DialogueAction>(){})
            }
        });
    }

    private void OnDisable() {
        InteractionEvents.OnPaintBucketActivated -= OnPaintBucketActivated;
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Baker", null},
            { "Villager", null},
        });
    }

    private void Start() {
        if (bucketsFound > 0) {
            SetPaintBucketCollectionUI();
        }
    }


    private void SetPaintBucketCollectionUI() {
        wm.SetQuestName("Explore the town");
        wm.SetQuestObjectives($"* Find the hidden paintbuckets : {bucketsFound}/3");
    }
    private void OnPaintBucketActivated(int amount) {
        bucketsFound++;
        SetPaintBucketCollectionUI();
        CheckCompleted();
    }




    private void CheckCompleted() {
        UpdateState();
        if (bucketsFound >= 3) {
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(bucketsFound));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        bucketsFound = data.BucketsFound;
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public int BucketsFound;

        public StupidJSONWrapper(int bucketsFound) {
            BucketsFound = bucketsFound;
        }

    }



}