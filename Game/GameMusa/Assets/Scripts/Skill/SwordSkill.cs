using UnityEngine;

public enum SwordType // 소드 타입
{
    Regular, // 기본
    Bounce, // 바운스
    Pierce, // 관통
    Spin, // 회전
}

public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular; // 소드 타입

    [Header("스킬 정보")]
    [SerializeField] private GameObject swordPrefab; // 소드 프리팹
    [SerializeField] private Vector2 swordDirection; // 소드 방향
    [SerializeField] private float swordGravity; // 소드 중력
    [SerializeField] private float returnSpeed; // 회수 속도
    [SerializeField] private float freezeTimeDuration; // 시간 정지 지속시간

    private Vector2 finalDirection; // 최종 방향

    [Header("조준 경로")]
    [SerializeField] private GameObject dotPrefab; // 도트 프리팹
    [SerializeField] private int dotNumber; // 도트 수
    [SerializeField] private float dotInterval; // 도트 간격
    [SerializeField] private Transform dotParent; // 도트 부모 => 일괄 관리
    private GameObject[] dots; // 도트 배열

    [Header("바운스 정보")]
    [SerializeField] private float bounceSpeed; // 바운스 속도
    [SerializeField] private int bounceAmount; // 바운스 횟수
    [SerializeField] private float bounceGravity; // 바운스 중력

    [Header("관통 정보")]
    [SerializeField] private int pierceAmount; // 관통 횟수
    [SerializeField] private float pierceGravity; // 관통 중력

    [Header("스핀 정보")]
    [SerializeField] private float spinDistance = 7; // 스핀 거리
    [SerializeField] private float spinDuration = 2; // 스핀 지속시간
    [SerializeField] private float spinGravity = 1; // 스핀 중력
    [SerializeField] private float hitCooldown = 0.35f; // 타격 쿨다운

    protected override void Start()
    {
        base.Start();

        GenereateDots(); // 도트 생성

        SetGravity(); // 중력 설정
    }

    // 중력 설정 함수
    private void SetGravity()
    {
        if (swordType == SwordType.Bounce) // 바운스 타입
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce) // 관통 타입
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Spin) // 스핀 타입
            swordGravity = spinGravity;
    }

    // 도트 생성 함수
    private void GenereateDots()
    {
        dots = new GameObject[dotNumber];

        for (int i = 0; i < dotNumber; i++)
        {
            // 도트 생성
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);

            // 도트 활성화
            dots[i].SetActive(false);
        }
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) // 마우스 우클릭 유지
        {
            for (int i = 0; i < dots.Length; i++)
            {
                // 시간에 따른 도트 위치 설정
                dots[i].transform.position = SetDotPos(dotInterval * i);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1)) // 마우스 우클릭 해제
        {
            // 소드 최종 방향 설정
            finalDirection = new Vector2(
                AimDirection().normalized.x * swordDirection.x,
                AimDirection().normalized.y * swordDirection.y);
        }
    }

    // 시간에 따른 도트 위치 설정 함수
    private Vector2 SetDotPos(float t)
    {
        // 도트 위치 = 포물선 공식 적용
        return (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * swordDirection.x,
            AimDirection().normalized.y * swordDirection.y) * t
                + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);
    }

    // 방향 조준 함수
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position; // 플레이어 위치
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치

        return mousePosition - playerPosition; // 플레이어에서 마우스를 향하는 벡터
    }

    // 소드 생성 함수
    public void CreateSword()
    {
        // 새로운 소드 생성
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        // 소드 타입별 설정
        if (swordType == SwordType.Bounce) // 바운스 타입
            newSwordScript.SetBounce(bounceSpeed, bounceAmount, true);
        else if (swordType == SwordType.Pierce) // 관통 타입
            newSwordScript.SetPierce(pierceAmount);
        else if (swordType == SwordType.Spin) // 스핀 타입
            newSwordScript.SetSpin(spinDistance, spinDuration, true, hitCooldown);

        // 소드 설정
        //newSword.GetComponent<SwordSkillController>()
        //.SetSword(swordDirection, swordGravity);
        //.SetSword(finalDirection, swordGravity);
        //.SetSword(finalDirection, swordGravity, player, returnSpeed);

        // 소드 설정
        newSwordScript
            //.SetSword(finalDirection, swordGravity, player, returnSpeed);
            .SetSword(finalDirection, swordGravity, player, returnSpeed, freezeTimeDuration);

        // 플레이어에게 소드 할당
        player.AssignSword(newSword);

        // 도트 비활성화
        ActiveDots(false);
    }

    // 도트 활성화 함수
    public void ActiveDots(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            // 도트 활성화
            dots[i].SetActive(_isActive);
        }
    }
}