using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� �Ӽ�")]
    public float speed = 5; // �ӵ�
    public float jump = 5; // ������
    public float power = 5; // �Ŀ�
    public Vector3 direction; // ����

    // ������
    public GameObject slash;

    // �׸���
    public GameObject shadow;
    List<GameObject> shadowList = new List<GameObject>();

    // ������
    public GameObject lazer;

    Animator ani; // �ִϸ�����
    Rigidbody2D rb; // ������ٵ�
    SpriteRenderer sr; // ��������Ʈ ������

    void Start()
    {
        direction = Vector2.zero;

        // ������ ������Ʈ
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        InputKey();
        Move();
    }

    void InputKey() // Ű �Է� �Լ�
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // A/SŰ �Ǵ� �¿� ����Ű �Է�

        // �÷��̾� ��� ����
        if (direction.x < 0) // ���� ����Ű
        {
            sr.flipX = true;
            ani.SetBool("Run", true);

            // �׸��� ���� ����
            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX;
            }
        }
        else if (direction.x > 0) // ������ ����Ű
        {
            sr.flipX = false;
            ani.SetBool("Run", true);

            // �׸��� ���� ����
            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX;
            }
        }
        else if (direction.x == 0) // ���� (�Է� ����)
        {
            ani.SetBool("Run", false);

            // �׸��� ����
            for (int i = 0; i < shadowList.Count; i++)
            {
                Destroy(shadowList[i]); // �׸��� ���� 
                shadowList.RemoveAt(i); // ����Ʈ ������ �׸��� ����
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) // WŰ �Է�
        {
            if (ani.GetBool("Jump") == false)
            {
                Jump();
                ani.SetBool("Jump", true);
            }
        }

        if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ��
        {
            ani.SetTrigger("Attack");

            // ������ ����
            Instantiate(lazer, transform.position, Quaternion.identity);
        }
    }

    public void Move() // �̵� �Լ�
    {
        // �÷����� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Jump() // ���� �Լ�
    {
        // �÷����� ����
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit
            = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rb.linearVelocityY < 0) // �ϰ� ��
        {
            if (rayHit.collider != null) // ������Ʈ�� �ݶ��̴� ����
            {
                if (rayHit.distance < 0.7f) // �Ÿ� ����
                {
                    ani.SetBool("Jump", false);
                }
            }
        }
    }

    public void Slash() // ������ �Լ�
    {
        // ���⿡ ���� �÷��̾� ������(���� ����Ʈ) ����
        if (sr.flipX == false) // �÷��̾� ���� ������
        {
            rb.AddForce(Vector2.right * power, ForceMode2D.Impulse); // �÷��̾� �̵�
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // ������ ����
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // ������ ���� ������

        }
        else // �÷��̾� ���� ����
        {
            rb.AddForce(Vector2.left * power, ForceMode2D.Impulse); // �÷��̾� �̵�
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // ������ ����
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // ������ ���� ����
        }
    }

    public void Shadow() // �׸��� �Լ�
    {
        if (shadowList.Count < 6)
        {
            GameObject go = Instantiate(shadow, transform.position, Quaternion.identity); // �׸��� ����
            go.GetComponent<Shadow>().speed = 10 - shadowList.Count; // �׸��� �ӵ� ����
            shadowList.Add(go);
        }
    }
}