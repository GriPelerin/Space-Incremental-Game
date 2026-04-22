using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EventType
{
    DemandCompleted,
    DemandUpdated,
    DemandFailed,
    DemandUnlocked
}
public static class EventManager<TEventArgs>
{
    private static Dictionary<EventType, Action<TEventArgs>> _eventDictionary = new Dictionary<EventType, Action<TEventArgs>>();

    public static void Subscribe(EventType eventType, Action<TEventArgs> listener)
    {
        if (!_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType] = listener;
        }
        else
        {
            _eventDictionary[eventType] += listener;
        }
    }

    public static void Unsubscribe(EventType eventType, Action<TEventArgs> listener)
    {
        if(_eventDictionary[eventType] != null)
        {
            _eventDictionary[eventType] -= listener;
        }
        if (_eventDictionary[eventType] == null)
        {
            _eventDictionary.Remove(eventType);
        }
    }

    public static void TriggerEvent(EventType eventType, TEventArgs eventArgs)
    {
        if (_eventDictionary.TryGetValue(eventType, out var listener))
        {
            listener?.Invoke(eventArgs);
        }
    }
}