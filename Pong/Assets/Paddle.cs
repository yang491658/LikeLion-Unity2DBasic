using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
     public Rigidbody2D _rigidbody;

    public KeyCode Up;
    public KeyCode Down;

    private float _movement;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        _movement = 0f;
        if(Input.GetKey(Up)) { _movement += 1f; }
        if(Input.GetKey(Down)) { _movement -= 1f; }
        _rigidbody.linearVelocity = new Vector2(0, _movement * speed);    
    }

    public void Reset()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        transform.position = _startPosition;
    }
}
