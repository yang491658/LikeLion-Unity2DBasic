using UnityEngine;

public class Player : MonoBehaviour
{
    public DynamicJoystick joystick; // ���̽�ƽ
    public GameObject missile; // �̻���
    public bool isShooting = false; // �߻� ����

    private void Update()
    {
        // ���� �Է�
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 direction = new Vector3(x, y, 0);

        // �÷��̾� �̵�
        transform.Translate(direction * 3 * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �Է�
        //{
        //    // �̻��� ����
        //    Instantiate(missile, transform.position, Quaternion.identity);
        //}

        if (isShooting) // �߻� ��
        {
            // �̻��� ����
            Instantiate(missile, transform.position, Quaternion.identity);
        }
    }

    // �߻� �Լ�
    public void Shoot()
    {
        // �̻��� ����
        Instantiate(missile, transform.position, Quaternion.identity);
    }

    // �߻� ���� �Լ�
    public void EnterShooting() => isShooting = true;

    // �߻� ���� �Լ�
    public void ExitShooting() => isShooting = false;
}