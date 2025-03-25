using UnityEngine;

public class Stair : MonoBehaviour
{
    public GameObject player; // �÷��̾�

    // �÷��̾�� �浹
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    // �÷��̾�� �浹 ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}