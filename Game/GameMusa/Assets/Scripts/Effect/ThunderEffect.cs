using UnityEngine;

[CreateAssetMenu(fileName = "ThunderEffect", menuName = "Data/Effect/Thunder")]
public class ThunderEffect : ItemEffect
{
    [SerializeField] private GameObject thunderPrefab; // 천둥 프리팹

    // 효과 실행 함수 (상속)
    public override void DoEffect(Transform _enemy)
    {
        // 새로운 천둥 생성
        GameObject newThunder 
            = Instantiate(thunderPrefab, _enemy.position + Vector3.up, Quaternion.identity);

        // 일정시간 후 천둥 제거
        Destroy(newThunder, 1);
    }
}
