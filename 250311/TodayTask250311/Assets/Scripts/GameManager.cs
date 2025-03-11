using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �ڱ��ڽ��� ������ ���

    public Text scoreText; // ������ ǥ���ϴ� �ؽ�Ʈ ��ü�� ������ ����
    public Text startText; // ������ ǥ���ϴ� �ؽ�Ʈ ��ü�� ������ ����

    public static int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int num)
    {
        score += num; // ���� ����
        scoreText.text = "Score : " + score; // �ؽ�Ʈ �ݿ�
    }


    void Start()
    {
        StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        int i = 3;

        // ��ü �ð� ����
        // ��� ���� ����� Update() ����
        // ��, WaitForSeconds()�� �۵����� ����
        // RealtimeSinceStartUp �Ǵ� WaitForSecondsRealtime() ���
        // ������ ���� ���·� 3�� ī��Ʈ : Time.unscaledTime �Ǵ� WaitForSecondsRealtime
        Time.timeScale = 0;

        while (i > 0)
        {
            startText.text = i.ToString();

            //yield return new WaitForSeconds(1f); // 1�� ���
            yield return new WaitForSecondsRealtime(1f); // 1�� ���

            i--;

            if (i == 0)
            {
                startText.gameObject.SetActive(false); // ��Ȱ��ȭ : UI �����
                Time.timeScale = 1; // �ٽ� �ð� ���
            }
        }
    }

    void Update()
    {

    }
}
