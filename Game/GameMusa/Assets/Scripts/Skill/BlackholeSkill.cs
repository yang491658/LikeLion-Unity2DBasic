using UnityEngine;

public class BlackholeSkill : Skill
{
    [Header("��ų ����")]
    [SerializeField] private GameObject blackholePrefab; // ��Ȧ ������
    [SerializeField] private float blackDuration; // ��Ȧ ���ӽð�
    BlackholeSkillController currentBlackhole; // ���� ��Ȧ

    [Header("��â ����")]
    [SerializeField] private float maxSize; // �ִ� ũ��
    [SerializeField] private float growSpeed; // ��â �ӵ�

    [Header("���� ����")]
    [SerializeField] private int attackAmount; // ���� Ƚ��
    [SerializeField] private float attackCooldown; // ���� ��ٿ�

    [Header("���� ����")]
    [SerializeField] private float shrinkSpeed; // ���� �ӵ�

    // ��ų ��� ���� �Լ�
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    // ��ų ��� �Լ� = ��Ȧ ���� �Լ�
    public override void UseSkill()
    {
        base.UseSkill();

        // ���ο� ��Ȧ ����
        GameObject newBlackhole =
            Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);

        // ��Ȧ ����
        currentBlackhole = newBlackhole.GetComponent<BlackholeSkillController>();
        currentBlackhole
            .SetBlackhole(maxSize, growSpeed, attackAmount, attackCooldown, shrinkSpeed, blackDuration);
    }

    // ��ų ���� ���� �Լ�
    public bool IsFinishSkill()
    {
        if (!currentBlackhole) // ���� ���Ȧ ����
        {
            return false; // ��ų ����
        }
        else if (currentBlackhole.playerCanExit) // �÷��̾� Ż�� ����
        {
            currentBlackhole = null; // ���� ��Ȧ ����
            return true; // ��ų ����
        }

        return false; // ��ų ����
    }

    // ��Ȧ ũ�� �Լ�
    public float GetSize() => maxSize;
}