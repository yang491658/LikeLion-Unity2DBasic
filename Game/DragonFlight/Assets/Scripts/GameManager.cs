using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱���
    // ��𿡼��� ���� �� �� �ֵ��� static(����)���� �ڱ��ڽ��� ����
    // �̱����̶�� ������ ������ ���
    public static GameManager instance;
    public Text scoreText; // ������ ǥ���ϴ� Text ��ü�� �����Ϳ��� �޾ƿ�
    public Text startText; // ���� ���� �� ī��Ʈ 3, 2, 1
    int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        int i = 3;
        while (i > 0)
        {
            startText.text = i.ToString();

            yield return new WaitForSeconds(1);

            i--;

            if (i == 0)
            {
                startText.gameObject.SetActive(false); // UI ���߱�
            }
        }
    }

    public void AddScore(int num)
    {
        score += num; // ���� �߰�
        scoreText.text = "Score : " + score; // �ؽ�Ʈ �ݿ�
    }

    void Update()
    {

    }
}
