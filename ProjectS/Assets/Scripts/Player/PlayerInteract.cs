using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    SO_Input input;
    PlayerAim playerAim;
    Camera mainCamera;

    [SerializeField]
    float interactDistance = 3.0f;
    [SerializeField]
    LayerMask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        playerAim = GetComponent<PlayerAim>();
    }

    void OnEnable()
    {
        input.InteractEvent += OnInteract;
    }

    void OnDisable()
    {
        input.InteractEvent -= OnInteract;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnInteract()
    {
        Interact();
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

    void Interact()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {
            if (InRange(hit.point) && hit.transform.gameObject.CompareTag("Interactable"))
            {
                InteractBase target;
                GameObject hitObject = hit.transform.gameObject;
                hitObject.TryGetComponent<InteractBase>(out target);
                target.Interact(gameObject);
            }
        }
    }
}
