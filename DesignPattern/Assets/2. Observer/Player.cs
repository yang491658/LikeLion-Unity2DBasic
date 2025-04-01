using UnityEngine;

// 이벤트 발생자
public class Player : MonoBehaviour
{
    private int _health = 100;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            // 체력 변경 이벤트 발생
            EventManager.Instance.TriggerEvent("PlayerHealthChanged", _health);
            if (_health <= 0)
            {
                // 플레이어 사망 이벤트 발생
                EventManager.Instance.TriggerEvent("PlayerDied");
            }
        }
    }
    
    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
 
    private void Update()
    {
        // 테스트용 : 스페이스 키를 누르면 데미지 받기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
}