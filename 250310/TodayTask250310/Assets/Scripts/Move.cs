using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f; // �ӵ�

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // �¿� �̵�
        float moveZ = Input.GetAxis("Vertical"); // �յ� �̵�
        Vector3 move = new Vector3(moveX, 0, moveZ);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
