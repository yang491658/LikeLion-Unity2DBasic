using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private Player player; // 플레이어

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger()
    {
        player.AttackOver();
    }
}