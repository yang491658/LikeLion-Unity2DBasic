using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // 싱글톤 구현
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("EventManager");
                _instance = go.AddComponent<EventManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    // 이벤트와 옵저버를 연결하는 딕셔너리
    private Dictionary<string, Action<object>> _eventDictionary = new Dictionary<string, Action<object>>();

    // 이벤트에 옵저버 추가
    public void AddListener(string eventName, Action<object> listener)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent += listener;
            _eventDictionary[eventName] = thisEvent;

        }
        else
        {
            _eventDictionary.Add(eventName, listener);
        }
    }

    // 이벤트에서 옵저버 제거
    public void RemoveListener(string eventName, Action<object> listener)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent -= listener;
            _eventDictionary[eventName] = thisEvent;
        }
    }

    // 이벤트 발생 (모든 옵저버에게 알림)
    public void TriggerEvent(string eventName, object data = null)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent?.Invoke(data);
        }
    }
}