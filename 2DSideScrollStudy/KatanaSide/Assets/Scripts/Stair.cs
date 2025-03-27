using UnityEngine;

public class Stair : MonoBehaviour
{
    public GameObject player; // 플레이어

    // 플레이어와 충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    // 플레이어와 충돌 해제
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}