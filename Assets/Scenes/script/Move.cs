
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform childTransform;
    public GameObject bulletPrefab; // Inspector에서 큐브 프리팹 할당
    public float bulletSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        childTransform.localPosition = new Vector3(0, 2, 0);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 좌클릭 시 바라보는 방향으로 큐브(총알) 발사
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (bulletPrefab != null && childTransform != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, childTransform.position, childTransform.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // childTransform의 로컬 y축 방향으로 발사
                    rb.linearVelocity = childTransform.up * bulletSpeed;
                }
                // 3초 뒤에 bullet 오브젝트 파괴
                Destroy(bullet, 3f);
            }
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            childTransform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
            childTransform.Rotate(new Vector3(0, -180, 0) * Time.deltaTime);
        }
    }
}
