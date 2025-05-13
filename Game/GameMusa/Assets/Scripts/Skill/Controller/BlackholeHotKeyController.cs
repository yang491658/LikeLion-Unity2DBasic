using TMPro;
using UnityEngine;

public class BlackholeHotKeyController : MonoBehaviour
{
    // 컴포넌트
    private SpriteRenderer sr;
    private TextMeshProUGUI text; // 텍스트

    private KeyCode hotKey; // 핫키
    private BlackholeSkillController blackhole; // 블랙홀
    private Transform enemy; // 적

    private void Update()
    {
        if (Input.GetKeyDown(hotKey)) // 핫키 입력
        {
            // 블랙홀 타겟 추가
            blackhole.AddTarget(enemy);

            // 투명화
            sr.color = Color.clear;
            text.color = Color.clear;
        }
    }

    // 핫키 설정 함수
    public void SetHotKey(KeyCode _hotKey, BlackholeSkillController _blackhole, Transform _enemy)
    {
        sr = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        hotKey = _hotKey;
        text.text = hotKey.ToString();
        blackhole = _blackhole;
        enemy = _enemy;
    }
}