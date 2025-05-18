using UnityEngine;

[CreateAssetMenu(fileName = "FireEffect", menuName = "Data/Effect/Fire")]
public class FireEffect : ItemEffect
{
    [SerializeField] private GameObject firePrefab; // 불꽃 프리팹
    [SerializeField] private float fireSpeed; // 불꽃 속도

    // 효과 실행 함수 (상속)
    public override void DoEffect(Transform _respawn)
    {
        // 플레이어
        Player player = PlayerManager.instance.player;

        if (player.attackState.combo == 2) // 플레이어 최대 콤보
        {
            // 새로운 불꽃 생성
            GameObject newFire = Instantiate(firePrefab, _respawn.position, player.transform.rotation);

            // 불꽃 이동
            newFire.GetComponent<Rigidbody2D>().linearVelocity 
                = new Vector2( player.direction * fireSpeed, 0);

            // 일정시간 후 불꽃 제거
            Destroy(newFire, 10);
        }
    }
}
