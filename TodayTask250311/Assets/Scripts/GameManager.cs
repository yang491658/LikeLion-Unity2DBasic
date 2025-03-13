using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 자기자신을 변수로 담기

    public Text scoreText; // 점수를 표시하는 텍스트 객체를 저장할 변수
    public Text startText; // 점수를 표시하는 텍스트 객체를 저장할 변수

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
        score += num; // 점수 증가
        scoreText.text = "Score : " + score; // 텍스트 반영
    }


    void Start()
    {
        StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        int i = 3;

        // 전체 시간 정지
        // 모든 물리 연산과 Update() 정지
        // 단, WaitForSeconds()은 작동하지 않음
        // RealtimeSinceStartUp 또는 WaitForSecondsRealtime() 사용
        // 게임을 멈춘 상태로 3초 카운트 : Time.unscaledTime 또는 WaitForSecondsRealtime
        Time.timeScale = 0;

        while (i > 0)
        {
            startText.text = i.ToString();

            //yield return new WaitForSeconds(1f); // 1초 대기
            yield return new WaitForSecondsRealtime(1f); // 1초 대기

            i--;

            if (i == 0)
            {
                startText.gameObject.SetActive(false); // 비활성화 : UI 숨기기
                Time.timeScale = 1; // 다시 시간 재생
            }
        }
    }

    void Update()
    {

    }
}
