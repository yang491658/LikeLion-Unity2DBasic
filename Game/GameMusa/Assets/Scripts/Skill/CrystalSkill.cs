using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [Header("스킬 정보")]
    [SerializeField] private GameObject crystalPrefab; // 크리스탈 프리팹
    [SerializeField] private float crystalDuration; // 크리스탈 지속시간
    private CrystalSkillController currentCrystal; // 현재 크리스탈
    [SerializeField] private bool canCreateClone; // 클론 생성 가능 여부

    [Header("폭발 정보")]
    [SerializeField] private bool isExploding; // 폭발 여부

    [Header("이동 정보")]
    [SerializeField] private bool isMoving; // 이동 여부
    [SerializeField] private float moveSpeed; // 이동 속도

    [Header("다중 스택 정보")]
    [SerializeField] private bool isStacking; // 스택 여부
    [SerializeField] private int stackAmount; // 스택 횟수
    [SerializeField] private float stackDuration; // 스택 지속시간
    [SerializeField] private float stackCooldown; // 스택 쿨다운
    [SerializeField] private List<GameObject> crystals = new List<GameObject>(); // 크리스탈 배열

    // 스킬 사용 함수
    public override void UseSkill()
    {
        base.UseSkill();

        if (MultiCrystal()) return; // 다중 크리스탈 실행 시 종료

        if (currentCrystal == null) // 현재 크리스탈 없음
        {
            CreateCrystal(); // 크리스탈 생성
        }
        else // 현재 크리스탈 있음
        {
            if (isMoving) return; // 이동 중이면 종료

            Vector2 playerPos = player.transform.position; // 플레이어 위치

            // 플레이어 순간이동 = 크리스탈 위치
            //player.transform.position = currentCrystal.transform.position;

            // 플레이어와 크리스탈 위치 교환
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (canCreateClone) // 클론 생성 가능
            {
                // 클론 생성
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);

                currentCrystal.DestroyCrystal(); // 크리스탈 제거
            }
            else // 클론 생성 불가능
            {
                currentCrystal?.FinishSkill(); // 스킬 종료
            }
        }
    }

    // 다중 크리스탈 함수
    private bool MultiCrystal()
    {
        if (isStacking) // 스택 중
        {
            if (crystals.Count > 0) // 크리스탈 있음
            {
                if (crystals.Count == stackAmount) // 최대 스택
                {
                    // 스택 지속시간 종료 후 스킬 종료
                    Invoke("FinishSkill", stackDuration);
                }

                cooldown = 0; // 쿨다운 초기화 = 0

                // 크리스탈 생성
                GameObject crystal = crystals[crystals.Count - 1]; // 배열에서 가져옴
                GameObject newCrystal // 새로운 크리스탈 생성
                    = Instantiate(crystal, player.transform.position, Quaternion.identity);
                crystals.Remove(crystal); // 배열에서 제거

                // 크리스탈 설정
                newCrystal.GetComponent<CrystalSkillController>()
                    .SetCrystal(crystalDuration, isExploding,
                    moveSpeed, isMoving, FindTarget(newCrystal.transform));
            }
            else if (crystals.Count <= 0) // 크리스탈 없음
            {
                cooldown = stackCooldown; // 쿨다운 초기화 = 스택 쿨다운

                RefillCrystal(); // 크리스탈 리필
            }

            return true; // 스택 실행 완료
        }

        return false; // 스택 실행 실패
    }

    // 스킬 종료 함수
    private void FinishSkill()
    {
        if (cooldownTimer > 0) return; // 쿨다운 중이면 종료

        cooldownTimer = stackCooldown; // 쿨다운 타이머 초기화 = 스택 쿨다운

        RefillCrystal(); // 크리스탈 리필
    }

    // 크리스탈 리필 함수
    private void RefillCrystal()
    {
        int add = stackAmount - crystals.Count; // 추가 = 부족한 크리스탈 수

        for (int i = 0; i < add; i++)
        {
            crystals.Add(crystalPrefab); // 크리스탈 추가
        }
    }

    // 크리스탈 생성 함수
    public void CreateCrystal()
    {
        // 새로운 크리스탈 생성
        GameObject newCrystal
            = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);

        // 크리스탈 설정
        currentCrystal = newCrystal.GetComponent<CrystalSkillController>();
        currentCrystal
            //.SetCrystal(crystalDuration);
            //.SetCrystal(crystalDuration, isExploding);
            .SetCrystal(crystalDuration, isExploding, 
                moveSpeed, isMoving, FindTarget(currentCrystal.transform));
    }

    // 타겟 선택 함수
    public void ChoiceTarget() => currentCrystal.ChoiceTarget();
}