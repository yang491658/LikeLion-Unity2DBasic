using UnityEngine;

public class Missile : MonoBehaviour
{
    private void Update()
    {
        // �̻��� �̵�
        transform.Translate(Vector3.up * 3 * Time.deltaTime);
    }
}
