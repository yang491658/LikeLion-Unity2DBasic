using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGame : MonoBehaviour
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartGameProcess);
    }

    void StartGameProcess()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Playground", LoadSceneMode.Single);
    }
}