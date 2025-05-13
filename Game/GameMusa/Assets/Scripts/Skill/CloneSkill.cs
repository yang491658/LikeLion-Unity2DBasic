using System.Collections;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("클론 정보")]
    [SerializeField] private GameObject clonePrefab; // 클론 프리팹
    [SerializeField] private float cloneDuration; // 클론 지속시간
    [Space]
    [SerializeField] private bool canAttack; // 공격 가능 여부

    [Header("연계 정보")]
    [SerializeField] private bool canCreateOnDash; // 대쉬 중 클론 생성 가능 여부
    [SerializeField] private bool canCreateOnDashOver; // 대쉬 종료 후 클론 생성 가능 여부
    [SerializeField] private bool canCreateOnCounter; // 카운터 중 클론 생성 가능 여부

    [Header("복제 정보")]
    [SerializeField] private bool canDuplicate; // 복제 가능 여부
    [SerializeField] private float duplicateChance; // 복제 확률

    [Header("크리스탈 정보")]
    public bool camCrystal; // 크리스탈 스킬 가능 여부

    // 대쉬 중 클론 생성 함수
    public void CreateCloneOnDash()
    {
        if (canCreateOnDash) // 클론 생성 가능
        {
            // 클론 생성
            CreateClone(player.transform, Vector3.zero);
        }
    }

    // 대쉬 종료 후 클론 생성 함수
    public void CreateCloneOnDashOver()
    {
        if (canCreateOnDashOver) // 클론 생성 가능
        {
            // 클론 생성
            CreateClone(player.transform, Vector3.zero);
        }
    }

    // 카운터 중 클론 생성 함수
    public void CreateCloneOnCounter(Transform _enemyTransform)
    {
        if (canCreateOnCounter) // 클론 생성 가능
        {
            // 클론 지연 생성 코루틴
            StartCoroutine(CreateCLoneDelay(_enemyTransform, new Vector3(player.direction * 2, 0)));
        }
    }

    // 클론 지연 생성 코루틴
    private IEnumerator CreateCLoneDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f); // 지연

        // 클론 생성
        CreateClone(_transform, _offset);
    }

    // 클론 생성 함수
    //public void CreateClone(Transform _clonePosition)
    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (camCrystal) // 크리스탈 스킬 가능
        {
            // 크리스탈 생성
            SkillManager.instance.crystal.CreateCrystal();

            return; // 종료
        }

        // 새로운 클론 생성
        GameObject newClone = Instantiate(clonePrefab);

        // 클론 설정
        newClone.GetComponent<CloneSkillController>()
            //.SetClone(canAttack, _clonePosition, cloneDuration);
            //.SetClone(canAttack, _clonePosition, cloneDuration, _offset);
            //.SetClone(canAttack, _clonePosition, cloneDuration, _offset, FindTarget(newClone.transform));
            .SetClone(canAttack, _clonePosition, cloneDuration, _offset, FindTarget(newClone.transform),
                canDuplicate, duplicateChance);
    }
}