using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    private Hashtable _EventHash = new Hashtable();

    private static EventBus _EventBus;

    public static EventBus Instance
    {
        get
        {
            if (!_EventBus)
            {
                _EventBus = FindAnyObjectByType(typeof(EventBus)) as EventBus;

                if (!_EventBus)
                {
                    Debug.Log("No EventBus found in a GameObject");
                }
                else
                {
                    _EventBus.Init();
                }
            }

            return _EventBus;
        }
    }

    private void Init()
    {
        if (_EventBus._EventHash == null)
        {
            _EventBus._EventHash = new Hashtable();
        }
    }

    public static void StartListening<T>(EventBusEvents.EventName eventName, UnityAction<T> listner)
    {
        UnityEvent<T> thisEvent = null;

        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.AddListener(listner);
            Instance._EventHash[eventName] = thisEvent;
        }
        else
        {
            thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(listner);
            Instance._EventHash.Add(key, thisEvent);
        }
    }

    public static void StartListening(EventBusEvents.EventName eventName, UnityAction listner)
    {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName))
        {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.AddListener(listner);
            Instance._EventHash[eventName] = thisEvent;
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listner);
            Instance._EventHash.Add(eventName, thisEvent);
        }
    }

    public static void StopListening<T>(EventBusEvents.EventName eventName, UnityAction<T> listner)
    {
        UnityEvent<T> thisEvent = null;
        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.RemoveListener(listner);
        }
    }

    public static void StopListening(EventBusEvents.EventName eventName, UnityAction listner)
    {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName))
        {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.RemoveListener(listner);
        }
    }

    public static void TriggerEvent<T>(EventBusEvents.EventName eventName, T val)
    {
        UnityEvent<T> thisEvent = null;
        string key = GetKey<T>(eventName);

        if (Instance._EventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)Instance._EventHash[key];
            thisEvent.Invoke(val);
        }
    }

    public static void TriggerEvent(EventBusEvents.EventName eventName)
    {
        UnityEvent thisEvent = null;

        if (Instance._EventHash.ContainsKey(eventName))
        {
            thisEvent = (UnityEvent)Instance._EventHash[eventName];
            thisEvent.Invoke();
        }
    }

    public static string GetKey<T>(EventBusEvents.EventName eventName)
    {
        Type type = typeof(T);
        return type.ToString() + "_" + eventName.ToString();
    }
}

