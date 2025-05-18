using UnityEngine;

[CreateAssetMenu(fileName = "ThunderEffect", menuName = "Data/Effect/Thunder")]
public class ThunderEffect : ItemEffect
{
    [SerializeField] private GameObject thunderPrefab; // õ�� ������

    // ȿ�� ���� �Լ� (���)
    public override void DoEffect(Transform _enemy)
    {
        // ���ο� õ�� ����
        GameObject newThunder 
            = Instantiate(thunderPrefab, _enemy.position + Vector3.up, Quaternion.identity);

        // �����ð� �� õ�� ����
        Destroy(newThunder, 1);
    }
}
