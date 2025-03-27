using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("�� ĳ���� �Ӽ�")]
    public float detectionRange = 10f; // �÷��̾� ���� �Ÿ�
    public float shootingInterval = 2f; // �߻� ��� �ð�
    public GameObject missile; // �̻���

    [Header("���� ������Ʈ")]
    private Transform player; // �÷��̾� ��ġ
    public Transform firePoint; // �̻��� �߻� ��ġ
    private float shootTimer; // �߻� Ÿ�̸�
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // ������Ʈ
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        shootTimer = shootingInterval; // Ÿ�̸� �ʱ�ȭ
    }


    void Update()
    {
        if (player == null) return;

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // �÷��̾� �������� ��������Ʈ ȸ��
            spriteRenderer.flipX = (player.position.x < transform.position.x);

            // �̻��� �߻�
            shootTimer -= Time.deltaTime;   //Ÿ�̸� ����

            if (shootTimer <= 0)
            {
                Shoot(); // �̻��� �߻� �Լ� ����
                shootTimer = shootingInterval; // Ÿ�̸� ����
            }

        }
    }

    // �̻��� �߻� �Լ�
    void Shoot()
    {
        // �̻��� ����
        GameObject go= Instantiate(missile, firePoint.position, Quaternion.identity);

        // �÷��̾� �������� �߻� ���� ��ȯ
        Vector2 direction = (player.position - firePoint.position).normalized;
        go.GetComponent<EnemyMissile>().SetDirection(direction); // �̻��� ���� ��ȯ
    }

    // ������ �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}