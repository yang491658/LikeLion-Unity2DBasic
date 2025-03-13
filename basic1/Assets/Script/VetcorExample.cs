using UnityEngine;

public class VetcorExample : MonoBehaviour
{
    //// 벡터
    //public Vector2 v2 = new Vector2(10, 10); // 좌표 x10, y10
    //public Vector3 v3 = new Vector3(1, 1, 1); // 좌표 x1, y1, z1
    //public Vector4 v4 = new Vector4(1.0f, 0.5f, 0.0f, 1.0f);

    void Start()
    {
        //// 벡터의 합
        //Vector3 a = new Vector3(1, 0, 0);
        //Vector3 b = new Vector3(2, 0, 0);
        //Vector3 c = a + b;
        //Debug.Log("Vecotor " + c);

        //Vector3 a = new Vector3(1, 1, 0);
        //Vector3 b = new Vector3(2, 0, 0);
        //Vector3 c = a + b;
        //Debug.Log("Vecotor : " + c);

        //// 벡터의 크기 magnitude
        //Debug.Log("Length : " + c.magnitude);

        // 정규화 normalize
        // 벡터의 크기가 1인 벡터 -> 방향만 유지
        Vector3 a = new Vector3(3, 0, 0);
        Vector3 normalizedVector = a.normalized;
        Debug.Log("크기가 1인, 방향만 설정하는 정규화 : " + a);
    }

    void Update()
    {

    }
}
