using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5; // �ӷ�
    public float jump = 5; // ������
    public Vector3 direction;

    public GameObject slash; // ������(����)

    Animator ani; // �ִϸ�Ƽ��
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
        }
    }

    void InputKey() // Ű �Է� �Լ�
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // A/SŰ �Ǵ� �¿� ����Ű �Է�

        // �÷��̾� ��� ����
        if (direction.x < 0) // ����
        {
            sr.flipX = true;
            ani.SetBool("Run", true);
        }
        else if (direction.x > 0) // ������
        {
            sr.flipX = false;
            ani.SetBool("Run", true);
        }
        else if (direction.x == 0)
        {
            ani.SetBool("Run", false);
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

    public void Slash() // ������ �Լ�
    {
        // �÷��̾� ������(���� ����Ʈ) ����
        Instantiate(slash, transform.position, Quaternion.identity);
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
}