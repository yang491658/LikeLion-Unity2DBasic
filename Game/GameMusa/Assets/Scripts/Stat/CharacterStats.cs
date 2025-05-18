using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx; // Ư��ȿ��

    public int currentHealth; // ���� ü��

    [Header("�⺻ ����")]
    public Stat strength; // �ٷ�
    public Stat agility; // ��ø
    public Stat intelligence; // ����
    public Stat vitality; // Ȱ��

    [Header("���� ����")]
    public Stat damage; // ������
    public Stat critical; // ġ��Ÿ ���ط�
    public Stat criticalChance; // ġ��Ÿ Ȯ��

    [Header("��� ����")]
    public Stat maxHealth; // �ִ� ü��
    public Stat evasion; // ȸ�Ƿ�
    public Stat armor; // ����
    public Stat resistance; // ���׷�

    [Header("���� ����")]
    public Stat fireDamage; // ȭ�� ������
    public Stat iceDamage; // ���� ������
    public Stat lightingDamage; // ���� ������

    public bool isIgniting; // ��ȭ ����
    public bool isChilling; // �ð� ����
    public bool isShocking; // ���� ����

    [SerializeField] private float ailmentDuration = 4; // �����̻� ���ӽð�
    private float igniteTimer; // ��ȭ Ÿ�̸�
    private float chillTimer; // ���� Ÿ�̸�
    private float shockTimer; // ���� Ÿ�̸�

    private float igniteCooldown = 0.3f; // ��ȭ ��ٿ�
    private int igniteDamage; // ��ȭ ������
    private float igniteDamamgeTimer; // ��ȭ ������ Ÿ�̸�

    [SerializeField] private GameObject shockPrefab; // ����
    private int shockDamage; // ���� ������

    public System.Action onChangeHealth; // ü�� ���� ��������Ʈ

    public bool isDead { get; private set; } // ��� ����

    protected virtual void Start()
    {
        // ���� ü�� �ʱ�ȭ = �ִ� ü��
        //currentHealth = maxHealth;
        currentHealth = maxHealth.GetValue();

        // ġ��Ÿ ���ط� ���� = 150%
        critical.SetValue(150);

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        // �����̻� Ÿ�̸� ����
        igniteTimer -= Time.deltaTime; // ��ȭ Ÿ�̸� ����
        chillTimer -= Time.deltaTime; // ���� Ÿ�̸� ����
        shockTimer -= Time.deltaTime; // ���� Ÿ�̸� ����

        if (igniteTimer < 0) // ��ȭ ���ӽð� ����
        {
            isIgniting = false; // ��ȭ ����
        }
        if (chillTimer < 0) // ���� ���ӽð� ����
        {
            isChilling = false; // ���� ����
        }
        if (shockTimer < 0) // ���� ���ӽð� ����
        {
            isShocking = false; // ���� ����
        }

        igniteDamamgeTimer -= Time.deltaTime; // ��ȭ ������ Ÿ�̸� ����

        if (isIgniting && // ��ȭ ��
            igniteDamamgeTimer < 0) // ��ȭ ��ٿ� ����
        {
            // ���� ü�� ���� = ��ȭ ������
            //currentHealth -= igniteDamage;
            DecreaseHealth(igniteDamage);

            if (currentHealth <= 0) // ���� ü�� 0 ����
            {
                // ���
                Die();
            }

            igniteDamamgeTimer = igniteCooldown; // ��ȭ ������ Ÿ�̸� �ʱ�ȭ = ��ȭ ��ٿ�
        }
    }

    // ������ ���� �Լ�
    public virtual void DoDmage(CharacterStats _target)
    {
        if (_target.AvoidDamage()) return; // Ÿ���� ������ ȸ�� ���� �� ����

        // ���� ������ = ������ + �ٷ�
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CriticalDamage()) // ġ��Ÿ ����
        {
            // ������ ����
            totalDamage = RaiseDamage(totalDamage);
        }

        // Ÿ�� ������ ���
        totalDamage = _target.DefendDamage(totalDamage);

        // Ÿ�� ������ �ǰ�
        _target.TakeDamage(totalDamage);

        //// ���� ������ ����
        //DoMagicalDamage(_target);
    }

    // ������ ġ��Ÿ �Լ�
    private bool CriticalDamage()
    {
        // ���� ġ��Ÿ Ȯ��(%) = ġ��Ÿ Ȯ�� + ��ø
        int totalCriticalChance = criticalChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) < totalCriticalChance) // ġ��Ÿ Ȯ��
        {
            return true; // ġ��Ÿ ����
        }

        return false; // ġ��Ÿ ����
    }

    // ������ ���� �Լ�
    private int RaiseDamage(int _damage)
    {
        // ���� ġ��Ÿ ���ط�(%) = (ġ��Ÿ ���ط� + �ٷ�) / 100
        float totalCritical = (critical.GetValue() + strength.GetValue()) * 0.01f;

        // ���� ������ = ������ x ���� ġ��Ÿ ���ط�(%)
        float totalDamage = _damage * totalCritical;

        return Mathf.RoundToInt(totalDamage); // ������ �ݿø�
    }

    // ������ �ǰ� �Լ�
    public virtual void TakeDamage(int _damage)
    {
        // ���� ü�� ���� = ���� ������
        //currentHealth -= _damage;
        DecreaseHealth(_damage);

        if (currentHealth <= 0) // ���� ü�� 0 ����
        {
            // ���
            Die();
        }
    }

    // ������ ȸ�� �Լ�
    private bool AvoidDamage()
    {
        // ���� ȸ����(%) = ȸ�Ƿ� + ��ø
        int totalEvasion = evasion.GetValue() + agility.GetValue();

        if (isShocking) // ���� ��
        {
            totalEvasion -= 20; // ���� ȸ���� 20% ����
        }

        if (Random.Range(0, 100) < totalEvasion) // ȸ����
        {
            return true; // ȸ�� ����
        }

        return false; // ȸ�� ����
    }

    // ������ ��� �Լ�
    private int DefendDamage(int _totalDamage)
    {
        if (isChilling) // �ð� ��
        {
            // ���� ������ = ������ - ���� x 0.8 : ���� 20& ����
            _totalDamage -= Mathf.RoundToInt(armor.GetValue() * 0.8f);
        }
        else
        {
            // ���� ������ = ������ - ����
            _totalDamage -= armor.GetValue();
        }

        return Mathf.Clamp(_totalDamage, 0, int.MaxValue); // �ִ��ּ� ����
    }

    // ���� ������ ���� �Լ�
    public virtual void DoMagicalDamage(CharacterStats _target)
    {
        // �� �Ӽ��� ������
        int _fire = fireDamage.GetValue();
        int _ice = iceDamage.GetValue();
        int _lighting = lightingDamage.GetValue();

        // ���� ������ = �� �Ӽ��� ������ �� + ����
        int totalDamage = _fire + _ice + _lighting + intelligence.GetValue();

        // Ÿ�� ������ ����
        totalDamage = _target.ResistDamage(totalDamage);

        // Ÿ�� ������ �ǰ�
        _target.TakeDamage(totalDamage);

        if (Mathf.Max(_fire, _ice, _lighting) <= 0) // �Ӽ� ������ ����
        {
            return; // ����
        }

        // �����̻� ���� ���� : ���� ���� �������� ������ �ش��ϴ� �����̻� ����
        bool canIgnite = _fire > _ice && _fire > _lighting; // ȭ�� ������ = ��ȭ ����
        bool canChill = _ice > _fire && _ice > _lighting; // ���� ������ = �ð� ����
        bool canShock = _lighting > _fire && _lighting > _ice; // ���� ������ = ���� ����

        while (!canIgnite && !canChill && !canShock) // �����̻� �Ұ���
        {
            if (Random.value < 0.5f && _fire > 0) // 50% Ȯ�� + ȭ�� ������ ����
            {
                canIgnite = true; // ��ȭ ����

                // Ÿ�� �����̻� ����
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }

            if (Random.value < 0.5f && _ice > 0) // 50% Ȯ�� + ���� ������ ����
            {
                canChill = true; // �ð� ����

                // Ÿ�� �����̻� ����
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }

            if (Random.value < 0.5f && _lighting > 0) // 50% Ȯ�� + ���� ������ ����
            {
                canShock = true; // ���� ����

                // Ÿ�� �����̻� ����
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }
        }

        if (canIgnite) // ��ȭ ����
        {
            // ��ȭ ������ ���� = ȭ�� �������� 20%
            _target.SetIgniteDamage(Mathf.RoundToInt(_fire * 0.2f));
        }

        if (canShock) // ���� ����
        {
            // ���� ������ ���� = ���� �������� 80%
            _target.SetShockDamage(Mathf.RoundToInt(_lighting * 0.8f));
        }

        // Ÿ�� �����̻� ����
        _target.ApplyAilment(canIgnite, canChill, canShock);
    }

    // ��ȭ ������ ���� �Լ�
    public void SetIgniteDamage(int _damage) => igniteDamage = _damage;

    // ���� ������ ���� �Լ�
    public void SetShockDamage(int _damage) => shockDamage = _damage;

    // ������ ���� �Լ�
    private int ResistDamage(int totalDamage)
    {
        // ���� ������ = ������ - (���׷� + ���� x 3)
        totalDamage -= resistance.GetValue() + intelligence.GetValue() * 3;

        return Mathf.Clamp(totalDamage, 0, int.MaxValue); // �ִ��ּ� ����
    }

    // �����̻� ���� �Լ�
    public void ApplyAilment(bool _canIgnite, bool _canChill, bool _canShock)
    {
        if (isIgniting || // ��ȭ ��
            isChilling || // �ð� ��
            isShocking) // ���� ��
        {
            if (isShocking && _canShock) // ���� �� + ���� ����
            {
                ShockTarget(); // Ÿ�� ����
            }

            return; // ����
        }

        if (_canIgnite) // ��ȭ ����
        {
            isIgniting = _canIgnite; // ��ȭ ����
            igniteTimer = ailmentDuration; // ��ȭ Ÿ�̸� �ʱ�ȭ = �����̻� ���ӽð�

            fx.IgniteFX(ailmentDuration); // ��ȭ Ư��ȿ��
        }

        if (_canChill) // �ð� ����
        {
            isChilling = _canChill; // �ð� ����
            chillTimer = ailmentDuration; // �ð� Ÿ�̸� �ʱ�ȭ = �����̻� ���ӽð�

            fx.ChillFX(ailmentDuration); // �ð� Ư��ȿ��

            GetComponent<Entity>().Slow(0.5f, ailmentDuration); // ��ƼƼ ��ȭ
        }

        if (_canShock) // ���� ����
        {
            ApplyShock(_canShock); // ���� ����
        }
    }

    // Ÿ�� ���� �Լ�
    private void ShockTarget()
    {
        // �ݶ��̴� ���� = ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float distance = Mathf.Infinity; // �Ÿ� �ʱ�ȭ = ���Ѵ�

        Transform target = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && // ���� ����
                Vector2.Distance(transform.position, hit.transform.position) > 1) // ���� �����Ÿ� �ʰ�
            {
                float distanceToEnemy
                    = Vector2.Distance(transform.position, hit.transform.position); // ������ �Ÿ� ���

                if (distanceToEnemy < distance) // ���� Ÿ�ٺ��� �� �����
                {
                    distance = distanceToEnemy; // �Ÿ� ����
                    target = hit.transform; // ���� Ÿ�� ����
                }
            }

            if (target == null) // Ÿ�� ����
            {
                target = transform; // Ÿ�� = �÷��̾�
            }
        }

        if (target != null) // Ÿ�� ����
        {
            // ���ο� ���� ����
            GameObject newShock = Instantiate(shockPrefab, transform.position, Quaternion.identity);

            // ���� ����
            newShock.GetComponent<ShockController>()
                .SetShock(target.GetComponent<CharacterStats>(), shockDamage);
        }
    }

    // ���� ���� �Լ�
    public void ApplyShock(bool _canShock)
    {
        if (isShocking) return; // ���� ���̸� ����

        isShocking = _canShock; // ���� ����
        shockTimer = ailmentDuration; // ���� Ÿ�̸� �ʱ�ȭ = �����̻� ���ӽð�

        fx.ShockFX(ailmentDuration); // ���� Ư��ȿ��
    }

    // ��� �Լ�
    protected virtual void Die()
    {
        isDead = true;
    }

    // ü�� ���� �Լ�
    protected virtual void DecreaseHealth(int _damage)
    {
        // ���� ü�� ���� = ������
        currentHealth -= _damage;

        // ü�� ���� �� ��������Ʈ �޼��� ȣ��
        if (onChangeHealth != null) onChangeHealth();
    }

    // ü�� ���� �Լ�
    public virtual void IncreaseHealth(int _amount)
    {
        // ���� ü�� ����
        currentHealth += _amount;

        // ���� ü�� �ִ밪 ���� = �ִ� ü��
        if (currentHealth > GetMaxHealth())
        {
            currentHealth = GetMaxHealth();
        }

        // ü�� ���� ��������Ʈ ����
        if (onChangeHealth != null)
        {
            onChangeHealth();
        }
    }

    // �ִ� ü�� �������� �Լ�
    public int GetMaxHealth()
    {
        // ���� �ִ� ü�� = �ִ� ü�� + Ȱ�� x 5
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    // ���� ���� �Լ�
    public virtual void BuffStat(Stat _stat, int _modifier, float _duration)
    {
        // ���� ���� �ڷ�ƾ
        StartCoroutine(BuffStatCoroutine(_stat, _modifier, _duration));
    }

    // ���� ���� �ڷ�ƾ
    private IEnumerator BuffStatCoroutine(Stat _stat, int _modifier, float _duration)
    {
        _stat.AddModifier(_modifier); // ������ �߰�
        yield return new WaitForSeconds(_duration); // ���ӽð� ����
        _stat.RemoveModifier(_modifier); // ������ ����
    }
}