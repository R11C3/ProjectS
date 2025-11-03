using System;
using UnityEngine;


public class InteractBase : MonoBehaviour
{
    BoxCollider interactArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactArea = GetComponent<BoxCollider>();
    }

    public void Interact(GameObject source)
    {
        Debug.Log("Interacted with " + gameObject);
    }
}
