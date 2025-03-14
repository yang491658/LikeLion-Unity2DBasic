using UnityEngine;

public class BossHead : MonoBehaviour
{
    [SerializeField] // 직렬화
    GameObject bossBullet;

    // 애니메이션에서 함수 사용
    public void RightDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(1/3f, -1));
    }

    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(-1/3f, -1));
    }
    public void DownLaunch()
    {
        GameObject go = Instantiate(bossBullet, transform.position, Quaternion.identity);

        go.GetComponent<BossBullet>().Move(new Vector2(0, -1));
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
