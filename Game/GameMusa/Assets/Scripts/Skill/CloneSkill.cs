using System.Collections;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Ŭ�� ����")]
    [SerializeField] private GameObject clonePrefab; // Ŭ�� ������
    [SerializeField] private float cloneDuration; // Ŭ�� ���ӽð�
    [Space]
    [SerializeField] private bool canAttack; // ���� ���� ����

    [Header("���� ����")]
    [SerializeField] private bool canCreateOnDash; // �뽬 �� Ŭ�� ���� ���� ����
    [SerializeField] private bool canCreateOnDashOver; // �뽬 ���� �� Ŭ�� ���� ���� ����
    [SerializeField] private bool canCreateOnCounter; // ī���� �� Ŭ�� ���� ���� ����

    [Header("���� ����")]
    [SerializeField] private bool canDuplicate; // ���� ���� ����
    [SerializeField] private float duplicateChance; // ���� Ȯ��

    [Header("ũ����Ż ����")]
    public bool camCrystal; // ũ����Ż ��ų ���� ����

    // �뽬 �� Ŭ�� ���� �Լ�
    public void CreateCloneOnDash()
    {
        if (canCreateOnDash) // Ŭ�� ���� ����
        {
            // Ŭ�� ����
            CreateClone(player.transform, Vector3.zero);
        }
    }

    // �뽬 ���� �� Ŭ�� ���� �Լ�
    public void CreateCloneOnDashOver()
    {
        if (canCreateOnDashOver) // Ŭ�� ���� ����
        {
            // Ŭ�� ����
            CreateClone(player.transform, Vector3.zero);
        }
    }

    // ī���� �� Ŭ�� ���� �Լ�
    public void CreateCloneOnCounter(Transform _enemyTransform)
    {
        if (canCreateOnCounter) // Ŭ�� ���� ����
        {
            // Ŭ�� ���� ���� �ڷ�ƾ
            StartCoroutine(CreateCLoneDelay(_enemyTransform, new Vector3(player.direction * 2, 0)));
        }
    }

    // Ŭ�� ���� ���� �ڷ�ƾ
    private IEnumerator CreateCLoneDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f); // ����

        // Ŭ�� ����
        CreateClone(_transform, _offset);
    }

    // Ŭ�� ���� �Լ�
    //public void CreateClone(Transform _clonePosition)
    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (camCrystal) // ũ����Ż ��ų ����
        {
            // ũ����Ż ����
            SkillManager.instance.crystal.CreateCrystal();

            return; // ����
        }

        // ���ο� Ŭ�� ����
        GameObject newClone = Instantiate(clonePrefab);

        // Ŭ�� ����
        newClone.GetComponent<CloneSkillController>()
            //.SetClone(canAttack, _clonePosition, cloneDuration);
            //.SetClone(canAttack, _clonePosition, cloneDuration, _offset);
            //.SetClone(canAttack, _clonePosition, cloneDuration, _offset, FindTarget(newClone.transform));
            .SetClone(canAttack, _clonePosition, cloneDuration, _offset, FindTarget(newClone.transform),
                canDuplicate, duplicateChance);
    }
}