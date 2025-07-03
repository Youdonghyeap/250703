
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform childTransform;
    public GameObject boomerangPrefab;
    public float moveDistance = 10f; // 이동 거리 임계값
    private Vector3 childStartPosition;
    private GameObject boomerangInstance;
    private Vector3 boomerangStartPosition;
    private bool boomerangFlying = false;
    private bool boomerangReturning = false;
    private float boomerangSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        childTransform.localPosition = new Vector3(0, 2, 0);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));

        childStartPosition = childTransform.position;
    }

    // Update is called once per frame
    void Update(){
        // 마우스 좌클릭 시 boomerangPrefab을 y축 방향으로 발사
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !boomerangFlying && !boomerangReturning)
        {
            if (boomerangPrefab != null && childTransform != null)
            {
                boomerangInstance = Instantiate(boomerangPrefab, childTransform.position, childTransform.rotation);
                boomerangStartPosition = childTransform.position;
                // childTransform을 안보이게 처리
                Renderer rend = childTransform.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.enabled = false;
                }
                boomerangFlying = true;
            }
        }

        // boomerang이 앞으로 날아감
        if (boomerangFlying && boomerangInstance != null)
        {
            boomerangInstance.transform.position += boomerangInstance.transform.up * boomerangSpeed * Time.deltaTime;
            float dist = Vector3.Distance(boomerangStartPosition, boomerangInstance.transform.position);
            if (dist >= moveDistance)
            {
                boomerangFlying = false;
                boomerangReturning = true;
            }
        }
        // boomerang이 childTransform 위치로 복귀
        if (boomerangReturning && boomerangInstance != null && childTransform != null)
        {
            Vector3 dir = (childTransform.position - boomerangInstance.transform.position).normalized;
            float returnDist = Vector3.Distance(boomerangInstance.transform.position, childTransform.position);
            float moveStep = boomerangSpeed * Time.deltaTime;
            if (moveStep >= returnDist)
            {
                boomerangInstance.transform.position = childTransform.position;
                // childTransform을 안보이게 처리
                Renderer rend = childTransform.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.enabled = true;
                }
                Destroy(boomerangInstance);
                boomerangReturning = false;
            }
            else
            {
                boomerangInstance.transform.position += dir * moveStep;
            }
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(new Vector3(0, 5, 0) * Time.deltaTime);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(new Vector3(0, -5, 0) * Time.deltaTime);
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
