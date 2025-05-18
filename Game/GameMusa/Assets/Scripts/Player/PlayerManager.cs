using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;

    // ΩÃ±€≈Ê
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}