using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPun, IPunObservable
{
    public float speed;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!photonView.AmOwner) return; // ������ �ƴ� �� ����

        Launch();
    }

    // �߻� �Լ�
    private void Launch()
    {
        if (!photonView.AmOwner) return; // ������ �ƴ� �� ����

        // ���� ����
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        // �̵�
        rb.linearVelocity = new Vector2(x * speed, y * speed);
    }

    // �ʱ�ȭ �Լ�
    public void Reset()
    {
        // ���� �� �ʱ�ȭ
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        Invoke("Launch", 1); // 1�� �� �߻�
    }

    // ����ȭ �Լ�
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // ������ ������ �߽�
        {
            // ���� ��ġ�� �ӵ� ���� �߽�
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
        }
        else // ������ ������ ����
        {
            // ���� ��ġ�� �ӵ� ���� ���� �� ����
            rb.position = (Vector2)stream.ReceiveNext();
            rb.linearVelocity = (Vector2)stream.ReceiveNext();
        }
    }
}