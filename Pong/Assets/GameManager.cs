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
        SpawnPaddle(); // �е� ��ȯ
        if (photonView.AmOwner) SpawnBall(); // ������ �� �� ��ȯ
    }

    private void SpawnPaddle()
    {
        int idx = PhotonNetwork.LocalPlayer.ActorNumber; // ������ ��Ʈ��ũ ID
        GameObject prefab = Resources.Load<GameObject>("Paddle"); // �е� ������ �ε�

            // ��Ʈ��ũ ID�� ���� �е� ����
        if (idx == 1) // �÷��̾�1
        {
            // ������ ����
            PhotonNetwork.Instantiate(prefab.name, new Vector3(-12, 0, 0), Quaternion.identity);
        }
        else // �÷��̾�2
        {
            // ������ ����
            PhotonNetwork.Instantiate(prefab.name, new Vector3(12, 0, 0), Quaternion.identity);
        }
    }

    // �� ��ȯ �Լ�
    private void SpawnBall()
    {
        GameObject prefab = Resources.Load<GameObject>("Ball"); // �� ������ �ε�

        // �� ����
        GameObject go = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
        ball = go.GetComponent<Ball>();
    }

    // �÷��̾�1 ���� �Լ�
    public void Player1Scored()
    {
        if (photonView.AmOwner) // ����
        {
            player1Score++; // �÷��̾�1 ���� �߰�
            ResetPosition(); // ��ġ �ʱ�ȭ
            photonView.RPC("UpdateScore", RpcTarget.All, player1Score, player2Score); // ���� ������Ʈ
        }
    }

    // �÷��̾�2 ���� �Լ�
    public void Player2Scored()
    {
        if (photonView.AmOwner) // ����
        {
            player2Score++; // �÷��̾�2 ���� �߰�
            ResetPosition(); // ��ġ �ʱ�ȭ
            photonView.RPC("UpdateScore", RpcTarget.All, player1Score, player2Score); // ���� ������Ʈ
        }
    }

    [PunRPC]
    // ���� ������Ʈ �Լ�
    public void UpdateScore(int score1, int score2)
    {
        // UI �ؽ�Ʈ ����
        player1Text.text = score1.ToString();
        player2Text.text = score2.ToString();

        if (score1 > 5 || score2 > 5) // ���� 5�� �ʰ�
            PhotonNetwork.LeaveRoom(); // ���� ����
    }

    // ��ġ �ʱ�ȭ �Լ�
    private void ResetPosition()
    {
        ball.Reset(); // �� �ʱ�ȭ
    }

    // �� Ż�� �Լ�
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene"); // �κ� ������ �̵�
    }
}