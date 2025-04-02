using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 5.0f; // ������
    public int lastJump = Environment.TickCount;

    void Update()
    {
        // Space Ű�� ������ ����
        if (Input.GetKey(KeyCode.Space) && lastJump + 1000 < Environment.TickCount)
        {
            lastJump = Environment.TickCount;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
