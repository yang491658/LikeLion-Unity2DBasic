using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� �Ӽ�")]
    public float speed = 5; // �ӵ�
    public float jump = 8; // ������
    public float power = 5; // �Ŀ�
    public Vector3 direction; // ����

    // ������
    public GameObject slash;

    // �׸���
    public GameObject shadow;
    List<GameObject> shadowList = new List<GameObject>();

    // ������
    public GameObject lazer;

    // �޸��� ����
    public GameObject dustRun;

    // ���� ����
    public GameObject dustJump;

    // �� ���� Ȯ��
    bool isWall; // �� ����
    public Transform wallCheck; // �� ��ġ
    public float wallDistance = 0.5f; // ������ �Ÿ�
    public LayerMask wallLayer; // �� ���̾�

    // �� ���� ����
    public float slidingSpeed = 0.8f; // ���� �ӵ�
    public bool wallJumping; // �� ���� ����
    float isRight = 1; // �� ��� ����

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
        if (!wallJumping)
        {
            InputKey(); // Ű �Է� �Լ� ����
            Move();// �̵� �Լ� ���� (�� ���� ���� �ƴ� ��)
        }

        // �� ���� Ȯ��
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right * isRight, wallDistance, wallLayer);
        ani.SetBool("Grab", isWall);

        if (Input.GetKeyDown(KeyCode.W)) // WŰ �Է�
        {
            if (ani.GetBool("Jump") == false)
            {
                Jump(); // ���� �Լ� ����
                ani.SetBool("Jump", true);
            }
        }

        if (isWall)
        {
            // �� ���
            wallJumping = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * slidingSpeed);

            // �� ��� �� ����
            if (Input.GetKeyDown(KeyCode.W)) // WŰ �Է�
            {
                wallJumping = true;

                // ��� �Լ� ����
                Invoke("Grab", 0.3f);

                // �� ����
                rb.linearVelocity = new Vector2(-isRight * jump, 0.9f * jump);

                // ���� ��ȯ
                sr.flipX = !sr.flipX;
                isRight = -isRight;
            }
        }
    }

    void InputKey() // ����Ű �Է� �Լ�
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

        DustJump(); // ���� ���� �Լ� ����
    }

    void Grab() // ��� �Լ�
    {
        wallJumping = false;
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
        //// �÷��̾� ������ ����
        //Instantiate(slash, transform.position, Quaternion.identity);

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

    public void DustRun(GameObject dustRun) // �޸��� ���� �Լ�
    {
        // �޸��� ���� ����
        Instantiate(dustRun, transform.position + new Vector3(0f, -0.35f, 0f), Quaternion.identity);
    }

    public void DustJump() // ���� ���� �Լ�
    {
        // ���� ���� ����
        Instantiate(dustJump, transform.position, Quaternion.identity);
    }
}