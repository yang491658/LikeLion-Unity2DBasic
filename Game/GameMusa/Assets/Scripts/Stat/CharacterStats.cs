using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("�⺻ ����")]
    public Stat strength; // �ٷ�
    public Stat agility; // ��ø
    public Stat intelligence; // ����

    [Header("���� ����")]
    public Stat damage; // ������
    public Stat critical; // ġ��Ÿ ���ط�
    public Stat criticalChance; // ġ��Ÿ Ȯ��

    [Header("��� ����")]
    [SerializeField] private int currentHealth; // ���� ü��
    public Stat maxHealth; // �ִ� ü��
    public Stat evasion; // ȸ�Ƿ�
    public Stat armor; // ����
    public Stat resistance; // ���׷�

    [Header("���� ����")]
    public Stat fireDamage; // ȭ�� ������
    public Stat iceDamage; // ���� ������
    public Stat lightingDamage; // ���� ������

    public bool isIgnited; // ��ȭ
    public bool isChilled; // �ð�
    public bool isShocked; // ����

    protected virtual void Start()
    {
        // ���� ü�� �ʱ�ȭ = �ִ� ü��
        //currentHealth = maxHealth;
        currentHealth = maxHealth.GetValue();

        // ġ��Ÿ ���ط� ����
        critical.SetValue(150);
    }

    // ������ ���� �Լ�
    public virtual void DoDmage(CharacterStats _target)
    {
        if (AvoidDamage(_target)) return; // ������ ȸ�� ���� �� ����

        // ���� ������ = ������ + �ٷ�
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CriticalDamage()) // ġ��Ÿ ����
        {
            // ������ ����
            totalDamage = RaiseDamage(totalDamage);
        }

        // ������ ���
        totalDamage = DefendDamage(_target, totalDamage);

        // Ÿ�� ������ �ǰ�
        //_target.TakeDamage(totalDamage);

        // ���� ������ ����
        DoMagicalDamage(_target);
    }

    // ������ ġ��Ÿ �Լ�
    private bool CriticalDamage()
    {
        // ���� ġ��Ÿ Ȯ�� = ġ��Ÿ Ȯ�� + ��ø
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
        // ���� ġ��Ÿ ���ط� = (ġ��Ÿ ���ط� + �ٷ�) / 100 [%]
        float totalCritical = (critical.GetValue() + strength.GetValue()) * 0.01f;

        // ���� ������ = ������ x ���� ġ��Ÿ ���ط�
        float totalDamage = _damage * totalCritical;

        return Mathf.RoundToInt(totalDamage); // ������ �ݿø�
    }

    // ������ �ǰ� �Լ�
    public virtual void TakeDamage(int _damage)
    {
        // ���� ü�� ���� = ���� ������
        currentHealth -= _damage;

        if (currentHealth <= 0) // ���� ü�� 0 ����
        {
            // ���
            Die();
        }
    }

    // ������ ȸ�� �Լ�
    private bool AvoidDamage(CharacterStats _target)
    {
        // ���� ȸ�Ƿ� = ȸ�Ƿ� + ��ø
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion) // ȸ�� Ȯ��
        {
            return true; // ȸ�� ����
        }

        return false; // ȸ�� ����
    }

    // ������ ��� �Լ�
    private int DefendDamage(CharacterStats _target, int _totalDamage)
    {
        // ���� ������ = ������ - Ÿ�� ����
        _totalDamage -= _target.armor.GetValue();

        return Mathf.Clamp(_totalDamage, 0, int.MaxValue); // �ִ��ּ� ����
    }

    // ���� ������ ���� �Լ�
    public virtual void DoMagicalDamage(CharacterStats _target)
    {
        // �� �Ӽ��� ������
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        // ���� ������ = �� �Ӽ��� ������ �� + ����
        int totalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        // ������ ����
        totalDamage = ResistDamage(_target, totalDamage);

        // Ÿ�� ������ �ǰ�
        _target.TakeDamage(totalDamage);
    }

    // �Ӽ� ���� �Լ�
    public void ApplyElement(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || // ��ȭ ��
            isChilled || // �ð� ��
            isShocked) // ���� ��
        {
            return; // ����
        }

        // �Ӽ� ���� ����
        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;
    }

    // ������ ���� �Լ�
    private int ResistDamage(CharacterStats _target, int totalDamage)
    {
        // ���� ������ = ������ - (Ÿ�� ���׷� + Ÿ�� ���� x 3)
        totalDamage -= _target.resistance.GetValue() + _target.intelligence.GetValue() * 3;

        return Mathf.Clamp(totalDamage, 0, int.MaxValue); // �ִ��ּ� ����
    }

    // ��� �Լ�
    protected virtual void Die()
    {
    }
}