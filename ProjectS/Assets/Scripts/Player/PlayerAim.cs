using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;

    [SerializeField]
    LayerMask floor;

    Camera mainCamera;
    public Vector3 mousePosition;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        GetMousePosition();
    }

    public void GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, floor))
        {
            mousePosition = hitInfo.point;
        }
    }
}