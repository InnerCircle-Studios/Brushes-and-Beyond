using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class QuestBus : MonoBehaviour {
    private Hashtable _EventHash = new();

    private static QuestBus _QuestBus;

    public static QuestBus Instance {
        get {
            if (!_QuestBus) {
                _QuestBus = FindAnyObjectByType<QuestBus>();

                if (!_QuestBus) {
                    Debug.Log("No QuestBus found in a GameObject");
                }
                else {
                    _QuestBus.Init();
                }
            }

            return _QuestBus;
        }
    }

    private void Init() {
        if (_QuestBus._EventHash == null) {
            _QuestBus._EventHash = new Hashtable();
        }
    }

    public static void StartListening<T>(QuestBusEvents eventName, UnityAction<T> listner) {
        UnityEvent<T> thisEvent = null;

        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key)) {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.AddListener(listner);
            Instance._EventHash[eventName] = thisEvent;
        }
        else {
            thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(listner);
            Instance._EventHash.Add(key, thisEvent);
        }
    }

    public static void StartListening(QuestBusEvents eventName, UnityAction listner) {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName)) {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.AddListener(listner);
            Instance._EventHash[eventName] = thisEvent;
        }
        else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listner);
            Instance._EventHash.Add(eventName, thisEvent);
        }
    }

    public static void StopListening<T>(QuestBusEvents eventName, UnityAction<T> listner) {
        UnityEvent<T> thisEvent = null;
        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key)) {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.RemoveListener(listner);
        }
    }

    public static void StopListening(QuestBusEvents eventName, UnityAction listner) {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName)) {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.RemoveListener(listner);
        }
    }

    public static void TriggerEvent<T>(QuestBusEvents eventName, T val) {
        UnityEvent<T> thisEvent = null;
        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key)) {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.Invoke(val);
        }
    }

    public static void TriggerEvent(QuestBusEvents eventName) {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName)) {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.Invoke();
        }
    }

    public static string GetKey<T>(QuestBusEvents eventName) {
        Type type = typeof(T);
        return type.ToString() + "_" + eventName.ToString();
    }
}

