using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 5.0f; // 점프력
    public int lastJump = Environment.TickCount;

    void Update()
    {
        // Space 키를 누르면 점프
        if (Input.GetKey(KeyCode.Space) && lastJump + 1000 < Environment.TickCount)
        {
            lastJump = Environment.TickCount;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
