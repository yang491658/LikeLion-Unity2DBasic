using UnityEngine;

public enum SwordType // �ҵ� Ÿ��
{
    Regular, // �⺻
    Bounce, // �ٿ
    Pierce, // ����
    Spin, // ȸ��
}

public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular; // �ҵ� Ÿ��

    [Header("��ų ����")]
    [SerializeField] private GameObject swordPrefab; // �ҵ� ������
    [SerializeField] private Vector2 swordDirection; // �ҵ� ����
    [SerializeField] private float swordGravity; // �ҵ� �߷�
    [SerializeField] private float returnSpeed; // ȸ�� �ӵ�
    [SerializeField] private float freezeTimeDuration; // �ð� ���� ���ӽð�

    private Vector2 finalDirection; // ���� ����

    [Header("���� ���")]
    [SerializeField] private GameObject dotPrefab; // ��Ʈ ������
    [SerializeField] private int dotNumber; // ��Ʈ ��
    [SerializeField] private float dotInterval; // ��Ʈ ����
    [SerializeField] private Transform dotParent; // ��Ʈ �θ� => �ϰ� ����
    private GameObject[] dots; // ��Ʈ �迭

    [Header("�ٿ ����")]
    [SerializeField] private float bounceSpeed; // �ٿ �ӵ�
    [SerializeField] private int bounceAmount; // �ٿ Ƚ��
    [SerializeField] private float bounceGravity; // �ٿ �߷�

    [Header("���� ����")]
    [SerializeField] private int pierceAmount; // ���� Ƚ��
    [SerializeField] private float pierceGravity; // ���� �߷�

    [Header("���� ����")]
    [SerializeField] private float spinDistance = 7; // ���� �Ÿ�
    [SerializeField] private float spinDuration = 2; // ���� ���ӽð�
    [SerializeField] private float spinGravity = 1; // ���� �߷�
    [SerializeField] private float hitCooldown = 0.35f; // Ÿ�� ��ٿ�

    protected override void Start()
    {
        base.Start();

        GenereateDots(); // ��Ʈ ����

        SetGravity(); // �߷� ����
    }

    // �߷� ���� �Լ�
    private void SetGravity()
    {
        if (swordType == SwordType.Bounce) // �ٿ Ÿ��
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce) // ���� Ÿ��
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Spin) // ���� Ÿ��
            swordGravity = spinGravity;
    }

    // ��Ʈ ���� �Լ�
    private void GenereateDots()
    {
        dots = new GameObject[dotNumber];

        for (int i = 0; i < dotNumber; i++)
        {
            // ��Ʈ ����
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);

            // ��Ʈ Ȱ��ȭ
            dots[i].SetActive(false);
        }
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) // ���콺 ��Ŭ�� ����
        {
            for (int i = 0; i < dots.Length; i++)
            {
                // �ð��� ���� ��Ʈ ��ġ ����
                dots[i].transform.position = SetDotPos(dotInterval * i);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1)) // ���콺 ��Ŭ�� ����
        {
            // �ҵ� ���� ���� ����
            finalDirection = new Vector2(
                AimDirection().normalized.x * swordDirection.x,
                AimDirection().normalized.y * swordDirection.y);
        }
    }

    // �ð��� ���� ��Ʈ ��ġ ���� �Լ�
    private Vector2 SetDotPos(float t)
    {
        // ��Ʈ ��ġ = ������ ���� ����
        return (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * swordDirection.x,
            AimDirection().normalized.y * swordDirection.y) * t
                + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);
    }

    // ���� ���� �Լ�
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position; // �÷��̾� ��ġ
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ

        return mousePosition - playerPosition; // �÷��̾�� ���콺�� ���ϴ� ����
    }

    // �ҵ� ���� �Լ�
    public void CreateSword()
    {
        // ���ο� �ҵ� ����
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        // �ҵ� Ÿ�Ժ� ����
        if (swordType == SwordType.Bounce) // �ٿ Ÿ��
            newSwordScript.SetBounce(bounceSpeed, bounceAmount, true);
        else if (swordType == SwordType.Pierce) // ���� Ÿ��
            newSwordScript.SetPierce(pierceAmount);
        else if (swordType == SwordType.Spin) // ���� Ÿ��
            newSwordScript.SetSpin(spinDistance, spinDuration, true, hitCooldown);

        // �ҵ� ����
        //newSword.GetComponent<SwordSkillController>()
        //.SetSword(swordDirection, swordGravity);
        //.SetSword(finalDirection, swordGravity);
        //.SetSword(finalDirection, swordGravity, player, returnSpeed);

        // �ҵ� ����
        newSwordScript
            //.SetSword(finalDirection, swordGravity, player, returnSpeed);
            .SetSword(finalDirection, swordGravity, player, returnSpeed, freezeTimeDuration);

        // �÷��̾�� �ҵ� �Ҵ�
        player.AssignSword(newSword);

        // ��Ʈ ��Ȱ��ȭ
        ActiveDots(false);
    }

    // ��Ʈ Ȱ��ȭ �Լ�
    public void ActiveDots(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            // ��Ʈ Ȱ��ȭ
            dots[i].SetActive(_isActive);
        }
    }
}