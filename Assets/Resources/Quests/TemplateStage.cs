using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Template quest for creating new queststages.
/// Do not inherit from this class directly, but from QuestStage instead.
/// </summary>
public abstract class TemplateStage : QuestStage {
    // Queststages can have specific references to other objects.
    // But remember that they should be accessable in the assets view.
    // E.g. [serializefield] private GameObject randomItemInScene; won't work as the stage prefab can't get a reference to it without being in the same scene.
    // But stuff like [SerializeField] private CharacterAttributes inky; will work as the CharacterAttributes are assets

    WindowManager wm;

    private void OnEnable() {
        // Put all code which needs to run at quest start here.
        // This also includes subscribing to events and adding dialogue to characters.
        // Remember to unsubscribe from events in OnDisable()!
        // Do not call CheckCompleted from here! This will trigger a loop which crashes unity's memory allocator.
        wm = GameManager.Instance.GetWindowManager();
    }

    private void OnDisable() {
        // Called when the stage is finished and removed from the scene.
        // Use this method to unsubscribe from events and add stuff e.g. dialogue reflecting quest completion to characters.
    }

    private void Start() {
        // Use this method to walk trough the stage when loading form a savefile.
        // Based on the stored state of the stage, you can e.g. call the methods to set the quest objectives.
    }

    private void CheckCompleted() {
        // Should be called after every event which can trigger the completion of the stage.
        // Add conditions in the if statement.
        UpdateState();
        if (true) {
            FinishStage();
        }
    }

    private void UpdateState() {
        // This method saves a checkpoint of the stage state to the savefile. 
        // Data can be stored in the wrapper via its constructor.
        // No generics allowed!
        string data = JsonUtility.ToJson(new StupidJSONWrapper());
        ChangeState(data);
    }

    protected override void SetQuestStageState(string state) {
        // This method is called when loading the stage from a savefile.
        // Manually map every value from the wrapper to the correct local values.
        // This is called after OnEnable(). and before Start().
        StupidJSONWrapper data = JsonUtility.FromJson<StupidJSONWrapper>(state);
        UpdateState();
    }



    /// <summary>
    ///  This is a stupid wrapper class to make the json utility work.
    ///  Add whatever data you need to save in the stage here.
    /// </summary>
    [Serializable]
    public class StupidJSONWrapper {
        public StupidJSONWrapper() { }
    }



}