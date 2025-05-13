using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;

    // �̱���
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}