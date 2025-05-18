using UnityEngine;
using UnityEditor;

public class EnemyDesigner : EditorWindow // 사용자 지정 윈도우 생성
{
    // 윈도우 열기 함수
    [MenuItem("Window/Enemy Designer")]
    static private void OpenWindow()
    {
        EnemyDesigner window = (EnemyDesigner)GetWindow(typeof(EnemyDesigner));

        window.minSize = new Vector2(600, 300); // 윈도우 창의 최소 크기
        window.Show(); // 에디터 표시
    }

    int count = 0;
    int countX = 0;
    int countZ = 0;

    // 사용자 인터페이스 함수
    private void OnGUI()
    {
        GUILayout.Label("라벨1"); // 기본
        GUILayout.Label("라벨2", EditorStyles.boldLabel); // 볼드체
        EditorGUILayout.LabelField("라벨3 : ", "내용"); // 2개의 문자열

        if (GUILayout.Button("큐브 생성")) // 버튼 클릭
        {
            // 큐브 생성 및 설정
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // 생성
            cube.name = "큐브"; // 이름
            cube.transform.position = new Vector3(0, 0, count++); // 위치
        }

        if (GUILayout.Button("X축 큐브 생성"))
        {
            // 큐브 생성 및 설정
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // 생성
            cube.name = "X축 " + countX; // 이름
            cube.transform.position = new Vector3(countX++, 0, countZ); // 위치
        }

        if (GUILayout.Button("Z축 큐브 생성"))
        {
            // 큐브 생성 및 설정
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // 생성
            cube.name = "Z축 " + countZ; // 이름
            cube.transform.position = new Vector3(countX, 0, countZ++); // 위치
        }
    }
}