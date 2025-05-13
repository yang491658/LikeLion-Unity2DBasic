using Photon.Pun;
using UnityEngine;

public class Goal : MonoBehaviourPun
{
    public bool isPlayer1Goal;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("Ball"))
        {
            if(isPlayer1Goal)
            {
                Debug.Log("Player 2 Scored");
                _gameManager.Player2Scored();
            }
            else
            {
                Debug.Log("Player 1 Scored");
                _gameManager.Player1Scored();
            }
        }
    }
}
