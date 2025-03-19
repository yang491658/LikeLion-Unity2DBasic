using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    // 어디에서나 접근 할 수 있돌고 static(정적)으로 자기자신을 저장
    // 싱글톤이라는 디자인 패턴을 사용
    public static GameManager instance;
    public Text scoreText; // 점수를 표시하는 Text 객체를 에디터에서 받아옴
    public Text startText; // 게임 시작 전 카운트 3, 2, 1
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
                startText.gameObject.SetActive(false); // UI 감추기
            }
        }
    }

    public void AddScore(int num)
    {
        score += num; // 점수 추가
        scoreText.text = "Score : " + score; // 텍스트 반여
    }

    void Update()
    {

    }
}
