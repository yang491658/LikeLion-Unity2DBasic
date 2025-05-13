using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("기본 스탯")]
    public Stat strength; // 근력
    public Stat agility; // 민첩
    public Stat intelligence; // 지능

    [Header("공격 스탯")]
    public Stat damage; // 데미지
    public Stat critical; // 치명타 피해량
    public Stat criticalChance; // 치명타 확률

    [Header("방어 스탯")]
    [SerializeField] private int currentHealth; // 현재 체력
    public Stat maxHealth; // 최대 체력
    public Stat evasion; // 회피력
    public Stat armor; // 방어력
    public Stat resistance; // 저항력

    [Header("마법 스탯")]
    public Stat fireDamage; // 화염 데미지
    public Stat iceDamage; // 얼음 데미지
    public Stat lightingDamage; // 번개 데미지

    public bool isIgnited; // 점화
    public bool isChilled; // 냉각
    public bool isShocked; // 감전

    protected virtual void Start()
    {
        // 현재 체력 초기화 = 최대 체력
        //currentHealth = maxHealth;
        currentHealth = maxHealth.GetValue();

        // 치명타 피해량 설정
        critical.SetValue(150);
    }

    // 데미지 공격 함수
    public virtual void DoDmage(CharacterStats _target)
    {
        if (AvoidDamage(_target)) return; // 데미지 회피 성공 시 종료

        // 종합 데미지 = 데미지 + 근력
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CriticalDamage()) // 치명타 성공
        {
            // 데미지 증가
            totalDamage = RaiseDamage(totalDamage);
        }

        // 데미지 방어
        totalDamage = DefendDamage(_target, totalDamage);

        // 타겟 데미지 피격
        //_target.TakeDamage(totalDamage);

        // 마법 데미지 공격
        DoMagicalDamage(_target);
    }

    // 데미지 치명타 함수
    private bool CriticalDamage()
    {
        // 종합 치명타 확률 = 치명타 확률 + 민첩
        int totalCriticalChance = criticalChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) < totalCriticalChance) // 치명타 확률
        {
            return true; // 치명타 성공
        }

        return false; // 치명타 실패
    }

    // 데미지 증가 함수
    private int RaiseDamage(int _damage)
    {
        // 종합 치명타 피해량 = (치명타 피해량 + 근력) / 100 [%]
        float totalCritical = (critical.GetValue() + strength.GetValue()) * 0.01f;

        // 종합 데미지 = 데미지 x 종합 치명타 피해량
        float totalDamage = _damage * totalCritical;

        return Mathf.RoundToInt(totalDamage); // 정수로 반올림
    }

    // 데미지 피격 함수
    public virtual void TakeDamage(int _damage)
    {
        // 현재 체력 감소 = 받은 데미지
        currentHealth -= _damage;

        if (currentHealth <= 0) // 현재 체력 0 이하
        {
            // 사망
            Die();
        }
    }

    // 데미지 회피 함수
    private bool AvoidDamage(CharacterStats _target)
    {
        // 종합 회피력 = 회피력 + 민첩
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion) // 회피 확률
        {
            return true; // 회피 성공
        }

        return false; // 회피 실패
    }

    // 데미지 방어 함수
    private int DefendDamage(CharacterStats _target, int _totalDamage)
    {
        // 종합 데미지 = 데미지 - 타겟 방어력
        _totalDamage -= _target.armor.GetValue();

        return Mathf.Clamp(_totalDamage, 0, int.MaxValue); // 최대최소 제한
    }

    // 마법 데미지 공격 함수
    public virtual void DoMagicalDamage(CharacterStats _target)
    {
        // 각 속성별 데미지
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        // 종합 데미지 = 각 속성별 데미지 합 + 지능
        int totalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        // 데미지 저항
        totalDamage = ResistDamage(_target, totalDamage);

        // 타겟 데미지 피격
        _target.TakeDamage(totalDamage);
    }

    // 속성 적용 함수
    public void ApplyElement(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || // 점화 중
            isChilled || // 냉각 중
            isShocked) // 감전 중
        {
            return; // 종료
        }

        // 속성 적용 여부
        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;
    }

    // 데미지 저항 함수
    private int ResistDamage(CharacterStats _target, int totalDamage)
    {
        // 종합 데미지 = 데미지 - (타겟 저항력 + 타겟 지능 x 3)
        totalDamage -= _target.resistance.GetValue() + _target.intelligence.GetValue() * 3;

        return Mathf.Clamp(totalDamage, 0, int.MaxValue); // 최대최소 제한
    }

    // 사망 함수
    protected virtual void Die()
    {
    }
}