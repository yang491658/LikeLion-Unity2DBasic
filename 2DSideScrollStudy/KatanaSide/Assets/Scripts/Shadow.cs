using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 10; // �ӵ�

    private GameObject player; // �÷��̾�

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position
            = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}