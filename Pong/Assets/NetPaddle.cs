using Photon.Pun;
using UnityEngine;

public class NetPaddle : MonoBehaviourPun
{
    public float speed = 10; // �ӵ�

    void Update()
    {
        if (photonView.IsMine) // ��ü�� �������� ����
        {
            // �̵� = ���� �Է� ���� x �ӵ� x �ð�
            float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            // �÷��̾� ���� �̵�
            transform.Translate(0, move, 0);
        }
    }
}