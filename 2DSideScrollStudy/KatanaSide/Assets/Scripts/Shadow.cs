using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 10; // 속도

    private GameObject player; // 플레이어

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position
            = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}