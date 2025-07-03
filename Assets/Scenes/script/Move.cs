
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform childTransform;
    public float moveDistance = 10f; // 이동 거리 임계값
    private Vector3 childStartPosition;
    private Quaternion childStartRotation;
    public GameObject childPrefab; // 프리팹 연결 필요
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        childTransform.localPosition = new Vector3(0, 2, 0);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));

        // 시작 위치와 회전 저장
        childStartPosition = childTransform.position;
        childStartRotation = childTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
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
        // 마우스 좌클릭 시 childTransform이 바라보는 방향(로컬 기준)으로 이동
        if (Mouse.current.leftButton.isPressed)
        {
            childTransform.Translate(new Vector3(0, 10, 0) * Time.deltaTime);
        }

        // childTransform이 일정 거리 이상 이동하면 초기화 및 재생성
        // childTransform이 파괴된 경우 예외 방지
        if (childTransform != null)
        {
            float dist = Vector3.Distance(childStartPosition, childTransform.position);
            if (dist >= moveDistance)
            {
                Destroy(childTransform.gameObject);
                childTransform = null;
            }
        }
        // childTransform이 null이면 새로 생성
        if (childTransform == null)
        {
            GameObject newChild = Instantiate(childPrefab, childStartPosition, childStartRotation);
            childTransform = newChild.transform;
        }
    }
}
