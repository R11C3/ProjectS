using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact(GameObject source)
    {
        Debug.Log("Interacted with " + gameObject);
    }
}
