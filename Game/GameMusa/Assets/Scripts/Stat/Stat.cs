using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue; // 기본값
    [HideInInspector] public List<int> modifiers; // 스탯 변경 배열

    // 값 설정 함수
    public void SetValue(int _value) => baseValue = _value;

    // 값 가져오기 함수
    //public int GetValue() => baseValue;
    public int GetValue()
    {
        int finalValue = baseValue; // 기본값

        // 스탯 변경 반영
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue; // 최종값
    }

    // 스탯 변경 추가 함수
    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    // 스탯 변경 제거 함수
    public void RemoveModifier(int _modifier)
    {
        modifiers.RemoveAt(_modifier);
    }
}