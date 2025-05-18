using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx; // 특수효과

    public int currentHealth; // 현재 체력

    [Header("기본 스탯")]
    public Stat strength; // 근력
    public Stat agility; // 민첩
    public Stat intelligence; // 지능
    public Stat vitality; // 활력

    [Header("공격 스탯")]
    public Stat damage; // 데미지
    public Stat critical; // 치명타 피해량
    public Stat criticalChance; // 치명타 확률

    [Header("방어 스탯")]
    public Stat maxHealth; // 최대 체력
    public Stat evasion; // 회피력
    public Stat armor; // 방어력
    public Stat resistance; // 저항력

    [Header("마법 스탯")]
    public Stat fireDamage; // 화염 데미지
    public Stat iceDamage; // 얼음 데미지
    public Stat lightingDamage; // 번개 데미지

    public bool isIgniting; // 점화 여부
    public bool isChilling; // 냉각 여부
    public bool isShocking; // 감전 여부

    [SerializeField] private float ailmentDuration = 4; // 상태이상 지속시간
    private float igniteTimer; // 점화 타이머
    private float chillTimer; // 빙결 타이머
    private float shockTimer; // 감전 타이머

    private float igniteCooldown = 0.3f; // 점화 쿨다운
    private int igniteDamage; // 점화 데미지
    private float igniteDamamgeTimer; // 점화 데미지 타이머

    [SerializeField] private GameObject shockPrefab; // 감전
    private int shockDamage; // 감전 데미지

    public System.Action onChangeHealth; // 체력 변경 델리게이트

    public bool isDead { get; private set; } // 사망 여부

    protected virtual void Start()
    {
        // 현재 체력 초기화 = 최대 체력
        //currentHealth = maxHealth;
        currentHealth = maxHealth.GetValue();

        // 치명타 피해량 설정 = 150%
        critical.SetValue(150);

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        // 상태이상 타이머 감소
        igniteTimer -= Time.deltaTime; // 점화 타이머 감소
        chillTimer -= Time.deltaTime; // 빙결 타이머 감소
        shockTimer -= Time.deltaTime; // 감전 타이머 감소

        if (igniteTimer < 0) // 점화 지속시간 종료
        {
            isIgniting = false; // 점화 종료
        }
        if (chillTimer < 0) // 빙결 지속시간 종료
        {
            isChilling = false; // 빙결 종료
        }
        if (shockTimer < 0) // 감전 지속시간 종료
        {
            isShocking = false; // 감전 종료
        }

        igniteDamamgeTimer -= Time.deltaTime; // 점화 데미지 타이머 감소

        if (isIgniting && // 점화 중
            igniteDamamgeTimer < 0) // 점화 쿨다운 종료
        {
            // 현재 체력 감소 = 점화 데미지
            //currentHealth -= igniteDamage;
            DecreaseHealth(igniteDamage);

            if (currentHealth <= 0) // 현재 체력 0 이하
            {
                // 사망
                Die();
            }

            igniteDamamgeTimer = igniteCooldown; // 점화 데미지 타이머 초기화 = 점화 쿨다운
        }
    }

    // 데미지 공격 함수
    public virtual void DoDmage(CharacterStats _target)
    {
        if (_target.AvoidDamage()) return; // 타겟이 데미지 회피 성공 시 종료

        // 종합 데미지 = 데미지 + 근력
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CriticalDamage()) // 치명타 성공
        {
            // 데미지 증가
            totalDamage = RaiseDamage(totalDamage);
        }

        // 타겟 데미지 방어
        totalDamage = _target.DefendDamage(totalDamage);

        // 타겟 데미지 피격
        _target.TakeDamage(totalDamage);

        //// 마법 데미지 공격
        //DoMagicalDamage(_target);
    }

    // 데미지 치명타 함수
    private bool CriticalDamage()
    {
        // 종합 치명타 확률(%) = 치명타 확률 + 민첩
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
        // 종합 치명타 피해량(%) = (치명타 피해량 + 근력) / 100
        float totalCritical = (critical.GetValue() + strength.GetValue()) * 0.01f;

        // 종합 데미지 = 데미지 x 종합 치명타 피해량(%)
        float totalDamage = _damage * totalCritical;

        return Mathf.RoundToInt(totalDamage); // 정수로 반올림
    }

    // 데미지 피격 함수
    public virtual void TakeDamage(int _damage)
    {
        // 현재 체력 감소 = 받은 데미지
        //currentHealth -= _damage;
        DecreaseHealth(_damage);

        if (currentHealth <= 0) // 현재 체력 0 이하
        {
            // 사망
            Die();
        }
    }

    // 데미지 회피 함수
    private bool AvoidDamage()
    {
        // 종합 회피율(%) = 회피력 + 민첩
        int totalEvasion = evasion.GetValue() + agility.GetValue();

        if (isShocking) // 감전 중
        {
            totalEvasion -= 20; // 종합 회피율 20% 감소
        }

        if (Random.Range(0, 100) < totalEvasion) // 회피율
        {
            return true; // 회피 성공
        }

        return false; // 회피 실패
    }

    // 데미지 방어 함수
    private int DefendDamage(int _totalDamage)
    {
        if (isChilling) // 냉각 중
        {
            // 종합 데미지 = 데미지 - 방어력 x 0.8 : 방어력 20& 감소
            _totalDamage -= Mathf.RoundToInt(armor.GetValue() * 0.8f);
        }
        else
        {
            // 종합 데미지 = 데미지 - 방어력
            _totalDamage -= armor.GetValue();
        }

        return Mathf.Clamp(_totalDamage, 0, int.MaxValue); // 최대최소 제한
    }

    // 마법 데미지 공격 함수
    public virtual void DoMagicalDamage(CharacterStats _target)
    {
        // 각 속성별 데미지
        int _fire = fireDamage.GetValue();
        int _ice = iceDamage.GetValue();
        int _lighting = lightingDamage.GetValue();

        // 종합 데미지 = 각 속성별 데미지 합 + 지능
        int totalDamage = _fire + _ice + _lighting + intelligence.GetValue();

        // 타겟 데미지 저항
        totalDamage = _target.ResistDamage(totalDamage);

        // 타겟 데미지 피격
        _target.TakeDamage(totalDamage);

        if (Mathf.Max(_fire, _ice, _lighting) <= 0) // 속성 데미지 없음
        {
            return; // 종료
        }

        // 상태이상 가능 여부 : 가장 높은 데미지의 종류에 해당하는 상태이상 적용
        bool canIgnite = _fire > _ice && _fire > _lighting; // 화염 데미지 = 점화 가능
        bool canChill = _ice > _fire && _ice > _lighting; // 얼음 데미지 = 냉각 가능
        bool canShock = _lighting > _fire && _lighting > _ice; // 번개 데미지 = 감전 가능

        while (!canIgnite && !canChill && !canShock) // 상태이상 불가능
        {
            if (Random.value < 0.5f && _fire > 0) // 50% 확률 + 화염 데미지 있음
            {
                canIgnite = true; // 점화 가능

                // 타겟 상태이상 적용
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }

            if (Random.value < 0.5f && _ice > 0) // 50% 확률 + 얼음 데미지 있음
            {
                canChill = true; // 냉각 가능

                // 타겟 상태이상 적용
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }

            if (Random.value < 0.5f && _lighting > 0) // 50% 확률 + 번개 데미지 있음
            {
                canShock = true; // 감전 가능

                // 타겟 상태이상 적용
                _target.ApplyAilment(canIgnite, canChill, canShock);

                return;
            }
        }

        if (canIgnite) // 점화 가능
        {
            // 점화 데미지 설정 = 화염 데미지의 20%
            _target.SetIgniteDamage(Mathf.RoundToInt(_fire * 0.2f));
        }

        if (canShock) // 감전 가능
        {
            // 감전 데미지 설정 = 번개 데미지의 80%
            _target.SetShockDamage(Mathf.RoundToInt(_lighting * 0.8f));
        }

        // 타겟 상태이상 적용
        _target.ApplyAilment(canIgnite, canChill, canShock);
    }

    // 점화 데미지 설정 함수
    public void SetIgniteDamage(int _damage) => igniteDamage = _damage;

    // 감전 데미지 설정 함수
    public void SetShockDamage(int _damage) => shockDamage = _damage;

    // 데미지 저항 함수
    private int ResistDamage(int totalDamage)
    {
        // 종합 데미지 = 데미지 - (저항력 + 지능 x 3)
        totalDamage -= resistance.GetValue() + intelligence.GetValue() * 3;

        return Mathf.Clamp(totalDamage, 0, int.MaxValue); // 최대최소 제한
    }

    // 상태이상 적용 함수
    public void ApplyAilment(bool _canIgnite, bool _canChill, bool _canShock)
    {
        if (isIgniting || // 점화 중
            isChilling || // 냉각 중
            isShocking) // 감전 중
        {
            if (isShocking && _canShock) // 감전 중 + 감전 가능
            {
                ShockTarget(); // 타겟 감전
            }

            return; // 종료
        }

        if (_canIgnite) // 점화 가능
        {
            isIgniting = _canIgnite; // 점화 시작
            igniteTimer = ailmentDuration; // 점화 타이머 초기화 = 상태이상 지속시간

            fx.IgniteFX(ailmentDuration); // 점화 특수효과
        }

        if (_canChill) // 냉각 가능
        {
            isChilling = _canChill; // 냉각 시작
            chillTimer = ailmentDuration; // 냉각 타이머 초기화 = 상태이상 지속시간

            fx.ChillFX(ailmentDuration); // 냉각 특수효과

            GetComponent<Entity>().Slow(0.5f, ailmentDuration); // 엔티티 둔화
        }

        if (_canShock) // 감전 가능
        {
            ApplyShock(_canShock); // 감전 적용
        }
    }

    // 타겟 감전 함수
    private void ShockTarget()
    {
        // 콜라이더 형성 = 감전 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float distance = Mathf.Infinity; // 거리 초기화 = 무한대

        Transform target = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && // 적과 접촉
                Vector2.Distance(transform.position, hit.transform.position) > 1) // 적과 일정거리 초과
            {
                float distanceToEnemy
                    = Vector2.Distance(transform.position, hit.transform.position); // 적과의 거리 계산

                if (distanceToEnemy < distance) // 현재 타겟보다 더 가까움
                {
                    distance = distanceToEnemy; // 거리 저장
                    target = hit.transform; // 현재 타겟 지정
                }
            }

            if (target == null) // 타겟 없음
            {
                target = transform; // 타겟 = 플레이어
            }
        }

        if (target != null) // 타겟 있음
        {
            // 새로운 감전 생성
            GameObject newShock = Instantiate(shockPrefab, transform.position, Quaternion.identity);

            // 감정 설정
            newShock.GetComponent<ShockController>()
                .SetShock(target.GetComponent<CharacterStats>(), shockDamage);
        }
    }

    // 감전 적용 함수
    public void ApplyShock(bool _canShock)
    {
        if (isShocking) return; // 감전 중이면 종료

        isShocking = _canShock; // 감전 시작
        shockTimer = ailmentDuration; // 감전 타이머 초기화 = 상태이상 지속시간

        fx.ShockFX(ailmentDuration); // 감전 특수효과
    }

    // 사망 함수
    protected virtual void Die()
    {
        isDead = true;
    }

    // 체력 감소 함수
    protected virtual void DecreaseHealth(int _damage)
    {
        // 현재 체력 감소 = 데미지
        currentHealth -= _damage;

        // 체력 변경 시 델리게이트 메서드 호출
        if (onChangeHealth != null) onChangeHealth();
    }

    // 체력 증가 함수
    public virtual void IncreaseHealth(int _amount)
    {
        // 현재 체력 증가
        currentHealth += _amount;

        // 현재 체력 최대값 제한 = 최대 체력
        if (currentHealth > GetMaxHealth())
        {
            currentHealth = GetMaxHealth();
        }

        // 체력 변경 델리게이트 실행
        if (onChangeHealth != null)
        {
            onChangeHealth();
        }
    }

    // 최대 체력 가져오기 함수
    public int GetMaxHealth()
    {
        // 종합 최대 체력 = 최대 체력 + 활력 x 5
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    // 스탯 버프 함수
    public virtual void BuffStat(Stat _stat, int _modifier, float _duration)
    {
        // 스탯 버프 코루틴
        StartCoroutine(BuffStatCoroutine(_stat, _modifier, _duration));
    }

    // 스탯 버프 코루틴
    private IEnumerator BuffStatCoroutine(Stat _stat, int _modifier, float _duration)
    {
        _stat.AddModifier(_modifier); // 변경자 추가
        yield return new WaitForSeconds(_duration); // 지속시간 적용
        _stat.RemoveModifier(_modifier); // 변경자 제거
    }
}