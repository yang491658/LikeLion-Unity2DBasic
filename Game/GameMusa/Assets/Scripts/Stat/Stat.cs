using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue; // �⺻��
    [HideInInspector] public List<int> modifiers; // ���� ���� �迭

    // �� ���� �Լ�
    public void SetValue(int _value) => baseValue = _value;

    // �� �������� �Լ�
    //public int GetValue() => baseValue;
    public int GetValue()
    {
        int finalValue = baseValue; // �⺻��

        // ���� ���� �ݿ�
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue; // ������
    }

    // ���� ���� �߰� �Լ�
    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    // ���� ���� ���� �Լ�
    public void RemoveModifier(int _modifier)
    {
        modifiers.RemoveAt(_modifier);
    }
}