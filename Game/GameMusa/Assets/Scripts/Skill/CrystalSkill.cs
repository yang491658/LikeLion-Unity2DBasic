using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [Header("��ų ����")]
    [SerializeField] private GameObject crystalPrefab; // ũ����Ż ������
    [SerializeField] private float crystalDuration; // ũ����Ż ���ӽð�
    private CrystalSkillController currentCrystal; // ���� ũ����Ż
    [SerializeField] private bool canCreateClone; // Ŭ�� ���� ���� ����

    [Header("���� ����")]
    [SerializeField] private bool isExploding; // ���� ����

    [Header("�̵� ����")]
    [SerializeField] private bool isMoving; // �̵� ����
    [SerializeField] private float moveSpeed; // �̵� �ӵ�

    [Header("���� ���� ����")]
    [SerializeField] private bool isStacking; // ���� ����
    [SerializeField] private int stackAmount; // ���� Ƚ��
    [SerializeField] private float stackDuration; // ���� ���ӽð�
    [SerializeField] private float stackCooldown; // ���� ��ٿ�
    [SerializeField] private List<GameObject> crystals = new List<GameObject>(); // ũ����Ż �迭

    // ��ų ��� �Լ�
    public override void UseSkill()
    {
        base.UseSkill();

        if (MultiCrystal()) return; // ���� ũ����Ż ���� �� ����

        if (currentCrystal == null) // ���� ũ����Ż ����
        {
            CreateCrystal(); // ũ����Ż ����
        }
        else // ���� ũ����Ż ����
        {
            if (isMoving) return; // �̵� ���̸� ����

            Vector2 playerPos = player.transform.position; // �÷��̾� ��ġ

            // �÷��̾� �����̵� = ũ����Ż ��ġ
            //player.transform.position = currentCrystal.transform.position;

            // �÷��̾�� ũ����Ż ��ġ ��ȯ
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (canCreateClone) // Ŭ�� ���� ����
            {
                // Ŭ�� ����
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);

                currentCrystal.DestroyCrystal(); // ũ����Ż ����
            }
            else // Ŭ�� ���� �Ұ���
            {
                currentCrystal?.FinishSkill(); // ��ų ����
            }
        }
    }

    // ���� ũ����Ż �Լ�
    private bool MultiCrystal()
    {
        if (isStacking) // ���� ��
        {
            if (crystals.Count > 0) // ũ����Ż ����
            {
                if (crystals.Count == stackAmount) // �ִ� ����
                {
                    // ���� ���ӽð� ���� �� ��ų ����
                    Invoke("FinishSkill", stackDuration);
                }

                cooldown = 0; // ��ٿ� �ʱ�ȭ = 0

                // ũ����Ż ����
                GameObject crystal = crystals[crystals.Count - 1]; // �迭���� ������
                GameObject newCrystal // ���ο� ũ����Ż ����
                    = Instantiate(crystal, player.transform.position, Quaternion.identity);
                crystals.Remove(crystal); // �迭���� ����

                // ũ����Ż ����
                newCrystal.GetComponent<CrystalSkillController>()
                    .SetCrystal(crystalDuration, isExploding,
                    moveSpeed, isMoving, FindTarget(newCrystal.transform));
            }
            else if (crystals.Count <= 0) // ũ����Ż ����
            {
                cooldown = stackCooldown; // ��ٿ� �ʱ�ȭ = ���� ��ٿ�

                RefillCrystal(); // ũ����Ż ����
            }

            return true; // ���� ���� �Ϸ�
        }

        return false; // ���� ���� ����
    }

    // ��ų ���� �Լ�
    private void FinishSkill()
    {
        if (cooldownTimer > 0) return; // ��ٿ� ���̸� ����

        cooldownTimer = stackCooldown; // ��ٿ� Ÿ�̸� �ʱ�ȭ = ���� ��ٿ�

        RefillCrystal(); // ũ����Ż ����
    }

    // ũ����Ż ���� �Լ�
    private void RefillCrystal()
    {
        int add = stackAmount - crystals.Count; // �߰� = ������ ũ����Ż ��

        for (int i = 0; i < add; i++)
        {
            crystals.Add(crystalPrefab); // ũ����Ż �߰�
        }
    }

    // ũ����Ż ���� �Լ�
    public void CreateCrystal()
    {
        // ���ο� ũ����Ż ����
        GameObject newCrystal
            = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);

        // ũ����Ż ����
        currentCrystal = newCrystal.GetComponent<CrystalSkillController>();
        currentCrystal
            //.SetCrystal(crystalDuration);
            //.SetCrystal(crystalDuration, isExploding);
            .SetCrystal(crystalDuration, isExploding, 
                moveSpeed, isMoving, FindTarget(currentCrystal.transform));
    }

    // Ÿ�� ���� �Լ�
    public void ChoiceTarget() => currentCrystal.ChoiceTarget();
}