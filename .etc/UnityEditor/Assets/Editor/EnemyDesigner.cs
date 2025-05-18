using UnityEngine;
using UnityEditor;

public class EnemyDesigner : EditorWindow // ����� ���� ������ ����
{
    // ������ ���� �Լ�
    [MenuItem("Window/Enemy Designer")]
    static private void OpenWindow()
    {
        EnemyDesigner window = (EnemyDesigner)GetWindow(typeof(EnemyDesigner));

        window.minSize = new Vector2(600, 300); // ������ â�� �ּ� ũ��
        window.Show(); // ������ ǥ��
    }

    int count = 0;
    int countX = 0;
    int countZ = 0;

    // ����� �������̽� �Լ�
    private void OnGUI()
    {
        GUILayout.Label("��1"); // �⺻
        GUILayout.Label("��2", EditorStyles.boldLabel); // ����ü
        EditorGUILayout.LabelField("��3 : ", "����"); // 2���� ���ڿ�

        if (GUILayout.Button("ť�� ����")) // ��ư Ŭ��
        {
            // ť�� ���� �� ����
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // ����
            cube.name = "ť��"; // �̸�
            cube.transform.position = new Vector3(0, 0, count++); // ��ġ
        }

        if (GUILayout.Button("X�� ť�� ����"))
        {
            // ť�� ���� �� ����
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // ����
            cube.name = "X�� " + countX; // �̸�
            cube.transform.position = new Vector3(countX++, 0, countZ); // ��ġ
        }

        if (GUILayout.Button("Z�� ť�� ����"))
        {
            // ť�� ���� �� ����
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // ����
            cube.name = "Z�� " + countZ; // �̸�
            cube.transform.position = new Vector3(countX, 0, countZ++); // ��ġ
        }
    }
}