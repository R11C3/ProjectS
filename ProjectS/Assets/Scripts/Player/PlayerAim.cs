using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAim : MonoBehaviour
{

    [SerializeField] public LayerMask mask;
    [SerializeField] private bool renderLine;

    private Transform characterTransform;
    private Camera mainCamera;
    [SerializeField]
    private SO_Player player;
    public Vector3 mousePosition;

    void Awake()
    {
        mainCamera = Camera.main;
        characterTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetInteractPosition()
    {

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
        {
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}