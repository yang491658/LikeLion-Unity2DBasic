using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    private float blackholeTimer; // 블랙홀 타이머
    private bool playerCanDisappear = true; // 플레이어 투명화 가능 여부
    public bool playerCanExit { get; private set; } // 플레이어 탈출 가능 여부

    [Header("팽창 정보")]
    private float maxSize; // 최대 크기
    private float growSpeed; // 팽창 속도
    private bool isGrowing = true; // 팽창 여부

    [Header("핫키 정보")]
    [SerializeField] private GameObject hotKeyPrefab; // 핫키 프리팹
    [SerializeField] private List<KeyCode> keyList; // 등록키 배열
    private List<Transform> targets = new List<Transform>(); //타겟 배열
    private List<GameObject> hotKeys = new List<GameObject>(); // 핫키 배열
    private bool canCreateHotKeys = true; // 핫키 생성 가능 여부

    [Header("공격 정보")]
    private int attackAmount; // 공격 횟수
    private float attackCooldown; // 공격 쿨다운
    private float attackTimer; // 공격 타이머
    private bool isAttacking; // 공격 여부

    [Header("수축 정보")]
    private float shrinkSpeed; // 수축 속도
    private bool isShrinking; // 수축 가능 여부

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G키 입력
        {
            StartAttack(); // 공격 시작
        }

        blackholeTimer -= Time.deltaTime; // 블랙홀 타이머 감소

        if (blackholeTimer < 0) // 블랙홀 지속시간 종료
        {
            blackholeTimer = Mathf.Infinity; // 블랙홀 타이머 초기화 = 무한대

            if (targets.Count > 0) // 타겟 있음
            {
                StartAttack(); // 공격 시작
            }
            else // 타겟 없음
            {
                FinishAttack(); // 공격 종료
            }
        }

        attackTimer -= Time.deltaTime; // 공격 타이머 감소

        CloneAttack(); // 클론 공격

        if (isGrowing) // 팽창 중
        {
            // 블랙홀 팽창
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(maxSize, maxSize),
                growSpeed * Time.deltaTime);
        }

        if (isShrinking) // 수축 중
        {
            // 블랙홀 수축
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(-1, -1),
                shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0) // 블랙홀 완전 수축
            {
                Destroy(gameObject); // 블랙홀 제거
            }
        }
    }

    // 공격 시작 함수
    private void StartAttack()
    {
        if (targets.Count > 0) // 타겟 있음
        {
            canCreateHotKeys = false; // 핫키 생성 불가능
            DestroyHotKeys(); // 핫키 제거

            isAttacking = true; // 공격 시작

            if (playerCanDisappear) // 플레이어 투명화 가능
            {
                // 플레이어 투명화
                PlayerManager.instance.player.MakeTransparent(true);

                playerCanDisappear = false;  // 플레이어 투명화 불가능
            }
        }
    }

    // 클론 공격 함수
    private void CloneAttack()
    {
        if (attackAmount > 0 && // 공격 횟수 있음
            attackTimer < 0 && // 공격 쿨다운 종료
            isAttacking) // 공격 중
        {
            attackTimer = attackCooldown; // 공격 타이머 초기화 = 공격 쿨다운

            // 랜덤 변수
            int index = Random.Range(0, targets.Count); // 인덱스
            float xOffset = Random.Range(0, 100) > 50 ? 2 : -2; // 좌우 오프셋

            if (SkillManager.instance.clone.camCrystal) // 크리스탈 스킬 가능
            {
                // 크리스탈 생성
                SkillManager.instance.crystal.CreateCrystal();

                // 크리스탈 타겟 선택
                SkillManager.instance.crystal.ChoiceTarget();
            }
            else
            {
                // 클론 생성
                SkillManager.instance.clone.CreateClone(targets[index], Vector3.right * xOffset);
            }

            attackAmount--; // 공격 횟수 감소
        }
        else if (attackAmount <= 0) // 공격 횟수 없음
        {
            // 일정시간 후 공격 종료
            Invoke("FinishAttack", shrinkSpeed / 3);
        }
    }

    // 공격 종료 함수
    private void FinishAttack()
    {
        DestroyHotKeys(); // 핫키 제거

        isGrowing = false; // 팽창 종료
        isAttacking = false; // 공격 종료
        isShrinking = true; // 수축 시작

        playerCanExit = true; // 플레이어 탈출 가능
    }

    // 트리거 충돌 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // 적과 충돌
        {
            // 적 시간 정지
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision); // 핫키 생성
        }
    }

    // 트리걸 탈출 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // 적과 충돌 해제
        {
            // 적 시간 정지 해제
            collision.GetComponent<Enemy>().FreezeTime(false);
        }
    }

    // 핫키 생성 함수
    private void CreateHotKey(Collider2D collision)
    {
        if (canCreateHotKeys) // 핫키 생성 가능
        {
            if (keyList.Count <= 0) // 등록키 없음
            {
                Debug.LogWarning("등록키 없음");
                return; // 종료
            }

            // 새로운 핫키 생성 및 추가
            GameObject newHotKey = Instantiate(
                hotKeyPrefab,
                collision.transform.position + Vector3.up * 2, // 충돌체 위
                Quaternion.identity);
            hotKeys.Add(newHotKey);

            // 등록키 중 랜덤키 설정 및 해당키 등록 제거
            KeyCode key = keyList[Random.Range(0, keyList.Count)];
            keyList.Remove(key);

            // 핫키 설정
            newHotKey.GetComponent<BlackholeHotKeyController>().SetHotKey(key, this, collision.transform);
        }
    }

    // 핫키 제거 함수
    private void DestroyHotKeys()
    {
        if (hotKeys.Count <= 0) return; // 핫키 없을 시 종료

        for (int i = 0; i < hotKeys.Count; i++)
        {
            Destroy(hotKeys[i]); // 핫키 제거
        }
    }

    // 타겟 추가 함수
    public void AddTarget(Transform _enemy) => targets.Add(_enemy); // 타겟 추가

    // 블랙홀 설정 함수
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

        blackholeTimer = _blackDuration; // 블랙홀 타이머 초기화 = 블랙홀 지속시간

        if (SkillManager.instance.clone.camCrystal) // 크리스탈 스킬 가능
        {
            playerCanDisappear = false; // 플레이어 투명화 불가능
        }
    }
}