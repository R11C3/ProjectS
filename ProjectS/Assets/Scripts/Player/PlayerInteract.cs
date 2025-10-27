using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    SO_Input input;

    PlayerAim aim;

    Camera mainCamera;

    [SerializeField]
    LayerMask interactMask;

    [SerializeField]
    float interactDistance = 3.0f;

    void Awake()
    {
        aim = GetComponent<PlayerAim>();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        input.InteractEvent += OnInteract;
    }

    void OnDisable()
    {
        input.InteractEvent -= OnInteract;
    }

    bool InRange(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) <= interactDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnInteract()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactMask))
        {
            if(InRange(hit.point) && hit.transform.gameObject.CompareTag("Interactable"))
            {
                Interactable target;
                GameObject hitObject = hit.transform.gameObject;
                hitObject.TryGetComponent<Interactable>(out target);
                while (target == null && hitObject != null)
                {
                    hitObject = hitObject.transform.parent.gameObject;
                    hitObject.TryGetComponent<Interactable>(out target);
                }
                target.Interact(gameObject);
            }
        }
    }
}
