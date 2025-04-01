using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // �̱��� �ν��Ͻ��� �����Ͽ� ���� �߰�
            GameManager.Instance.AddScore(10);
            Destroy(other.gameObject);
        }
    }
}