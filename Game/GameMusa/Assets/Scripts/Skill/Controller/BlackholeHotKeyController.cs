using TMPro;
using UnityEngine;

public class BlackholeHotKeyController : MonoBehaviour
{
    // ������Ʈ
    private SpriteRenderer sr;
    private TextMeshProUGUI text; // �ؽ�Ʈ

    private KeyCode hotKey; // ��Ű
    private BlackholeSkillController blackhole; // ��Ȧ
    private Transform enemy; // ��

    private void Update()
    {
        if (Input.GetKeyDown(hotKey)) // ��Ű �Է�
        {
            // ��Ȧ Ÿ�� �߰�
            blackhole.AddTarget(enemy);

            // ����ȭ
            sr.color = Color.clear;
            text.color = Color.clear;
        }
    }

    // ��Ű ���� �Լ�
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