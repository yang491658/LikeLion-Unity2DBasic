using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 싱글톤
    public static SkillManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public DashSkill dash { get; private set; } // 대쉬 스킬
    public CloneSkill clone { get; private set; } // 클론 스킬
    public SwordSkill sword { get; private set; } // 소드 스킬
    public BlackholeSkill blackhole { get; private set; } // 블랙홀 스킬
    public CrystalSkill crystal { get; private set; } // 크리스탈 스킬

    private void Start()
    {
        dash = GetComponent<DashSkill>(); // 대쉬 스킬
        clone = GetComponent<CloneSkill>(); // 클론 스킬
        sword = GetComponent<SwordSkill>(); // 소드 스킬
        blackhole = GetComponent<BlackholeSkill>(); // 블랙홀 스킬
        crystal = GetComponent<CrystalSkill>(); // 크리스탈 스킬
    }
}