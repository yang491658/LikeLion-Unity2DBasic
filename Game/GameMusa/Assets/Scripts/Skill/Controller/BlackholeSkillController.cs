using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    private float blackholeTimer; // ��Ȧ Ÿ�̸�
    private bool playerCanDisappear = true; // �÷��̾� ����ȭ ���� ����
    public bool playerCanExit { get; private set; } // �÷��̾� Ż�� ���� ����

    [Header("��â ����")]
    private float maxSize; // �ִ� ũ��
    private float growSpeed; // ��â �ӵ�
    private bool isGrowing = true; // ��â ����

    [Header("��Ű ����")]
    [SerializeField] private GameObject hotKeyPrefab; // ��Ű ������
    [SerializeField] private List<KeyCode> keyList; // ���Ű �迭
    private List<Transform> targets = new List<Transform>(); //Ÿ�� �迭
    private List<GameObject> hotKeys = new List<GameObject>(); // ��Ű �迭
    private bool canCreateHotKeys = true; // ��Ű ���� ���� ����

    [Header("���� ����")]
    private int attackAmount; // ���� Ƚ��
    private float attackCooldown; // ���� ��ٿ�
    private float attackTimer; // ���� Ÿ�̸�
    private bool isAttacking; // ���� ����

    [Header("���� ����")]
    private float shrinkSpeed; // ���� �ӵ�
    private bool isShrinking; // ���� ���� ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // GŰ �Է�
        {
            StartAttack(); // ���� ����
        }

        blackholeTimer -= Time.deltaTime; // ��Ȧ Ÿ�̸� ����

        if (blackholeTimer < 0) // ��Ȧ ���ӽð� ����
        {
            blackholeTimer = Mathf.Infinity; // ��Ȧ Ÿ�̸� �ʱ�ȭ = ���Ѵ�

            if (targets.Count > 0) // Ÿ�� ����
            {
                StartAttack(); // ���� ����
            }
            else // Ÿ�� ����
            {
                FinishAttack(); // ���� ����
            }
        }

        attackTimer -= Time.deltaTime; // ���� Ÿ�̸� ����

        CloneAttack(); // Ŭ�� ����

        if (isGrowing) // ��â ��
        {
            // ��Ȧ ��â
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(maxSize, maxSize),
                growSpeed * Time.deltaTime);
        }

        if (isShrinking) // ���� ��
        {
            // ��Ȧ ����
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(-1, -1),
                shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0) // ��Ȧ ���� ����
            {
                Destroy(gameObject); // ��Ȧ ����
            }
        }
    }

    // ���� ���� �Լ�
    private void StartAttack()
    {
        if (targets.Count > 0) // Ÿ�� ����
        {
            canCreateHotKeys = false; // ��Ű ���� �Ұ���
            DestroyHotKeys(); // ��Ű ����

            isAttacking = true; // ���� ����

            if (playerCanDisappear) // �÷��̾� ����ȭ ����
            {
                // �÷��̾� ����ȭ
                PlayerManager.instance.player.MakeTransparent(true);

                playerCanDisappear = false;  // �÷��̾� ����ȭ �Ұ���
            }
        }
    }

    // Ŭ�� ���� �Լ�
    private void CloneAttack()
    {
        if (attackAmount > 0 && // ���� Ƚ�� ����
            attackTimer < 0 && // ���� ��ٿ� ����
            isAttacking) // ���� ��
        {
            attackTimer = attackCooldown; // ���� Ÿ�̸� �ʱ�ȭ = ���� ��ٿ�

            // ���� ����
            int index = Random.Range(0, targets.Count); // �ε���
            float xOffset = Random.Range(0, 100) > 50 ? 2 : -2; // �¿� ������

            if (SkillManager.instance.clone.camCrystal) // ũ����Ż ��ų ����
            {
                // ũ����Ż ����
                SkillManager.instance.crystal.CreateCrystal();

                // ũ����Ż Ÿ�� ����
                SkillManager.instance.crystal.ChoiceTarget();
            }
            else
            {
                // Ŭ�� ����
                SkillManager.instance.clone.CreateClone(targets[index], Vector3.right * xOffset);
            }

            attackAmount--; // ���� Ƚ�� ����
        }
        else if (attackAmount <= 0) // ���� Ƚ�� ����
        {
            // �����ð� �� ���� ����
            Invoke("FinishAttack", shrinkSpeed / 3);
        }
    }

    // ���� ���� �Լ�
    private void FinishAttack()
    {
        DestroyHotKeys(); // ��Ű ����

        isGrowing = false; // ��â ����
        isAttacking = false; // ���� ����
        isShrinking = true; // ���� ����

        playerCanExit = true; // �÷��̾� Ż�� ����
    }

    // Ʈ���� �浹 �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // ���� �浹
        {
            // �� �ð� ����
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision); // ��Ű ����
        }
    }

    // Ʈ���� Ż�� �Լ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // ���� �浹 ����
        {
            // �� �ð� ���� ����
            collision.GetComponent<Enemy>().FreezeTime(false);
        }
    }

    // ��Ű ���� �Լ�
    private void CreateHotKey(Collider2D collision)
    {
        if (canCreateHotKeys) // ��Ű ���� ����
        {
            if (keyList.Count <= 0) // ���Ű ����
            {
                Debug.LogWarning("���Ű ����");
                return; // ����
            }

            // ���ο� ��Ű ���� �� �߰�
            GameObject newHotKey = Instantiate(
                hotKeyPrefab,
                collision.transform.position + Vector3.up * 2, // �浹ü ��
                Quaternion.identity);
            hotKeys.Add(newHotKey);

            // ���Ű �� ����Ű ���� �� �ش�Ű ��� ����
            KeyCode key = keyList[Random.Range(0, keyList.Count)];
            keyList.Remove(key);

            // ��Ű ����
            newHotKey.GetComponent<BlackholeHotKeyController>().SetHotKey(key, this, collision.transform);
        }
    }

    // ��Ű ���� �Լ�
    private void DestroyHotKeys()
    {
        if (hotKeys.Count <= 0) return; // ��Ű ���� �� ����

        for (int i = 0; i < hotKeys.Count; i++)
        {
            Destroy(hotKeys[i]); // ��Ű ����
        }
    }

    // Ÿ�� �߰� �Լ�
    public void AddTarget(Transform _enemy) => targets.Add(_enemy); // Ÿ�� �߰�

    // ��Ȧ ���� �Լ�
    public void SetBlackhole(
        float _maxSize, float _growSpeed,
        int _attackAmount, float _attackCooldown,
        float _shrinkSpeed,
        float _blackDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;

        attackAmount = _attackAmount;
        attackCooldown = _attackCooldown;

        shrinkSpeed = _shrinkSpeed;

        blackholeTimer = _blackDuration; // ��Ȧ Ÿ�̸� �ʱ�ȭ = ��Ȧ ���ӽð�

        if (SkillManager.instance.clone.camCrystal) // ũ����Ż ��ų ����
        {
            playerCanDisappear = false; // �÷��̾� ����ȭ �Ұ���
        }
    }
}