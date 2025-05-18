using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // �̱���
    public static SkillManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public DashSkill dash { get; private set; } // �뽬 ��ų
    public CloneSkill clone { get; private set; } // Ŭ�� ��ų
    public SwordSkill sword { get; private set; } // �ҵ� ��ų
    public BlackholeSkill blackhole { get; private set; } // ��Ȧ ��ų
    public CrystalSkill crystal { get; private set; } // ũ����Ż ��ų

    private void Start()
    {
        dash = GetComponent<DashSkill>(); // �뽬 ��ų
        clone = GetComponent<CloneSkill>(); // Ŭ�� ��ų
        sword = GetComponent<SwordSkill>(); // �ҵ� ��ų
        blackhole = GetComponent<BlackholeSkill>(); // ��Ȧ ��ų
        crystal = GetComponent<CrystalSkill>(); // ũ����Ż ��ų
    }
}