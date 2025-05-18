using UnityEngine;

public class BlackholeSkill : Skill
{
    [Header("스킬 정보")]
    [SerializeField] private GameObject blackholePrefab; // 블랙홀 프리팹
    [SerializeField] private float blackDuration; // 블랙홀 지속시간
    BlackholeSkillController currentBlackhole; // 현재 블랙홀

    [Header("팽창 정보")]
    [SerializeField] private float maxSize; // 최대 크기
    [SerializeField] private float growSpeed; // 팽창 속도

    [Header("공격 정보")]
    [SerializeField] private int attackAmount; // 공격 횟수
    [SerializeField] private float attackCooldown; // 공격 쿨다운

    [Header("수축 정보")]
    [SerializeField] private float shrinkSpeed; // 수축 속도

    // 스킬 사용 가능 함수
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    // 스킬 사용 함수 = 블랙홀 생성 함수
    public override void UseSkill()
    {
        base.UseSkill();

        // 새로운 블랙홀 생성
        GameObject newBlackhole =
            Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);

        // 블랙홀 설정
        currentBlackhole = newBlackhole.GetComponent<BlackholeSkillController>();
        currentBlackhole
            .SetBlackhole(maxSize, growSpeed, attackAmount, attackCooldown, shrinkSpeed, blackDuration);
    }

    // 스킬 종료 여부 함수
    public bool IsFinishSkill()
    {
        if (!currentBlackhole) // 현재 블록홀 없음
        {
            return false; // 스킬 지속
        }
        else if (currentBlackhole.playerCanExit) // 플레이어 탈출 가능
        {
            currentBlackhole = null; // 현재 블랙홀 제거
            return true; // 스킬 종료
        }

        return false; // 스킬 지속
    }

    // 블랙홀 크기 함수
    public float GetSize() => maxSize;
}