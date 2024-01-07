
using System;
using System.Collections.Generic;

using UnityEngine;

public class FirstPaintQuestStage : QuestStage {
    private int paintCounter = 0;

    private void OnEnable() {
        // Load dialogue for character during quest 

        EventWrapper showevent = new();
        showevent.AddListener(() => InteractionEvents.ShowObject("FirstPaintQuestBuckets"));

        QuestEvents.ChangeDialogue(new Dictionary<string, DialogueSet>() {
            { "Brushy", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetBrushy(), "Now that you have completed the tutorial, we can start with painting!", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetBrushy(), "Grab these three paintbuckets and fill in the blank spot.",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>(){
                    new(1,showevent ,true)
                })
            },
            { "Blockade 1", new(new List<DialogueEntry>() {
                    new(GameManager.Instance.GetPlayer(), "I don't have enough paint :(", DialogueActorMood.HAPPY),
                    new(GameManager.Instance.GetPlayer(), "Maybe those buckets contain some?",DialogueActorMood.HAPPY),
                }, new List<DialogueAction>())
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

    private void OnPaintBucketActivated(int amount) {
        paintCounter += amount;
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
        string data = JsonUtility.ToJson(new StupidJSONWrapper(new int[] { paintCounter }));
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        int[] data = JsonUtility.FromJson<StupidJSONWrapper>(state).Values;
        paintCounter = data[0];
        UpdateState();
    }

    [Serializable]
    public class StupidJSONWrapper {
        public int[] Values;
        public StupidJSONWrapper(int[] values) {
            Values = values;
        }
    }



}