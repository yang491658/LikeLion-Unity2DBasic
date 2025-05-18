using UnityEngine;

public class Missile : MonoBehaviour
{
    private void Update()
    {
        // 미사일 이동
        transform.Translate(Vector3.up * 3 * Time.deltaTime);
    }
}
