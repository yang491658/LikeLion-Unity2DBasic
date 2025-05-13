using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Ball")]
    public Ball ball;

    [Header("Player 1")]
    //public NetPaddle player1Paddle;
    public Goal player1Goal;

    [Header("Player 2")]
    //public NetPaddle player2Paddle;
    public Goal player2Goal;

    [Header("UI")]
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    private int player1Score;
    private int player2Score;

    private void Start()
    {
        SpawnPaddle(); // 패들 소환
        if (photonView.AmOwner) SpawnBall(); // 로컬일 시 공 소환
    }

    private void SpawnPaddle()
    {
        int idx = PhotonNetwork.LocalPlayer.ActorNumber; // 로컬의 네트워크 ID
        GameObject prefab = Resources.Load<GameObject>("Paddle"); // 패들 프리팹 로드

            // 네트워크 ID에 따른 패들 생성
        if (idx == 1) // 플레이어1
        {
            // 좌측에 생성
            PhotonNetwork.Instantiate(prefab.name, new Vector3(-12, 0, 0), Quaternion.identity);
        }
        else // 플레이어2
        {
            // 우측에 생성
            PhotonNetwork.Instantiate(prefab.name, new Vector3(12, 0, 0), Quaternion.identity);
        }
    }

    // 공 소환 함수
    private void SpawnBall()
    {
        GameObject prefab = Resources.Load<GameObject>("Ball"); // 공 프리팹 로드

        // 공 생성
        GameObject go = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
        ball = go.GetComponent<Ball>();
    }

    // 플레이어1 점수 함수
    public void Player1Scored()
    {
        if (photonView.AmOwner) // 로컬
        {
            player1Score++; // 플레이어1 점수 추가
            ResetPosition(); // 위치 초기화
            photonView.RPC("UpdateScore", RpcTarget.All, player1Score, player2Score); // 점수 업데이트
        }
    }

    // 플레이어2 점수 함수
    public void Player2Scored()
    {
        if (photonView.AmOwner) // 로컬
        {
            player2Score++; // 플레이어2 점수 추가
            ResetPosition(); // 위치 초기화
            photonView.RPC("UpdateScore", RpcTarget.All, player1Score, player2Score); // 점수 업데이트
        }
    }

    [PunRPC]
    // 점수 업데이트 함수
    public void UpdateScore(int score1, int score2)
    {
        // UI 텍스트 적용
        player1Text.text = score1.ToString();
        player2Text.text = score2.ToString();

        if (score1 > 5 || score2 > 5) // 점수 5점 초과
            PhotonNetwork.LeaveRoom(); // 게임 종료
    }

    // 위치 초기화 함수
    private void ResetPosition()
    {
        ball.Reset(); // 공 초기화
    }

    // 방 탈출 함수
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene"); // 로비 씬으로 이동
    }
}