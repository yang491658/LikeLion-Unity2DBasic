using UnityEngine;

public class Player : MonoBehaviour
{
    // �ӵ�
    public float speed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        MoveX();
    }

    void MoveX()
    {
        // �̵� �Ÿ� = ����(�Է��� Ű) * �ӵ� * �ð�(������ ����)
        float distanceX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // �¿� �̵�
        transform.Translate(distanceX, 0, 0);
    }
}
