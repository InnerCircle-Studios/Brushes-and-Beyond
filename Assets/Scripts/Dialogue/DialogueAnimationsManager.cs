using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DialogueAnimationsManager : MonoBehaviour {
    [SerializeField] List<string> animationQueue;
    [SerializeField] Animator animator;


    void AddToQueue(string animationName){
        animationQueue.Add(animationName);
    }

    public bool playQueue(){
        foreach(string animation in animationQueue){
            animator.Play(animation);
        }
        return false;
    }

}
