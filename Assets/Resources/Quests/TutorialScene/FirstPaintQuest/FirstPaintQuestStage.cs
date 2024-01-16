
using System;
using System.Collections.Generic;

using UnityEngine;

public class FirstPaintQuestStage : QuestStage {
    [SerializeField] CharacterAttributes brushy;

    private int paintCounter = 0;
    private bool dialogueFinished = false;
    WindowManager wm;

    private void OnEnable() {
        // Load dialogue for character during quest 

        wm = GameManager.Instance.GetWindowManager();
        wm.ShowQuestMenu();
        wm.SetQuestName("Painting");
        wm.SetQuestObjectives($"* Talk with Brushy");

        EventWrapper hideQuestUI = new();
        hideQuestUI.AddListener(() => wm.ClearQuest());

        EventWrapper showevent = new();
        showevent.AddListener(() => InteractionEvents.ShowObject("FirstPaintQuestBuckets"));

        EventWrapper updateQuestUI = new();
        updateQuestUI.AddListener(() => SetPaintBucketCollectionUI());

        EventWrapper updateDialogue = new();
        updateDialogue.AddListener(() => { UpdateDialogueAfterConversation(); dialogueFinished = true; UpdateState(); });

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "Now that you have completed the tutorial, we can start with painting!", DialogueActorMood.HAPPY),
                    new(brushy, "Grab these three paintbuckets and fill in the blank spot.",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(0, hideQuestUI,true),
                    new(1,showevent ,true),
                    new(1,updateQuestUI,true),
                    new(99,updateDialogue,true)
                })
            }
        });
        InteractionEvents.OnPaintBucketActivated += OnPaintBucketActivated;
    }

    private void OnDisable() {
        // Reset dialogue back to default
        // QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
        //     { "Brushy", null },
        //     { "Blockade 1", null }
        // });

    }

    private void Start() {
        if (dialogueFinished) {
            SetPaintBucketCollectionUI();
            UpdateDialogueAfterConversation();
        }
    }

    private void SetPaintBucketCollectionUI() {
        wm.SetQuestName("Painting");
        wm.SetQuestObjectives($"* Collect the buckets : {paintCounter}/3");
    }

    private void UpdateDialogueAfterConversation() {
        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(brushy, "What are you waiting for? Go grab some paint! ", DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){})
            },
            {"Blockade 1", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "I don't have enough paint :(", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "Maybe Brushy has some?",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>())
            }
        });
    }

    private void OnPaintBucketActivated(int amount) {
        paintCounter++;
        SetPaintBucketCollectionUI();
        CheckCompleted();
    }


    private void CheckCompleted() {
        UpdateState();
        if (paintCounter >= 3) {
            InteractionEvents.OnPaintBucketActivated -= OnPaintBucketActivated;
            FinishStage();
        }
    }

    private void UpdateState() {
        string data = JsonUtility.ToJson(new StupidJSONWrapper(
            new int[] { paintCounter },
            new bool[] { dialogueFinished }
        ));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        int[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        bool[] data2 = JsonUtility.FromJson<StupidJSONWrapper>(state).Values2;
        paintCounter = data[0];
        dialogueFinished = data2[0];
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public int[] Values;
        public bool[] Values2;
        public StupidJSONWrapper(int[] values, bool[] values2) {
            Values = values;
            Values2 = values2;
        }
    }



}