using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // �̱��� ����
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

    // �̺�Ʈ�� �������� �����ϴ� ��ųʸ�
    private Dictionary<string, Action<object>> _eventDictionary = new Dictionary<string, Action<object>>();

    // �̺�Ʈ�� ������ �߰�
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

    // �̺�Ʈ���� ������ ����
    public void RemoveListener(string eventName, Action<object> listener)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent -= listener;
            _eventDictionary[eventName] = thisEvent;
        }
    }

    // �̺�Ʈ �߻� (��� ���������� �˸�)
    public void TriggerEvent(string eventName, object data = null)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent?.Invoke(data);
        }
    }
}