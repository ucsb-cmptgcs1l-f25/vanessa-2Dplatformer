using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabbedObject;
    private GameObject lastPlacedObject;

    private int objectsLayer;
    private int groundLayer;
    private string heldObjectsLayerName = "HeldObjects";

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound; 

    [SerializeField] private float horizontalOffset = 1f; 
    [SerializeField] private float feetOffset = -0.5f; 

    void Start()
    {
        objectsLayer = LayerMask.NameToLayer("Objects");
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (Keyboard.current[Key.LeftShift].wasPressedThisFrame || Keyboard.current[Key.RightShift].wasPressedThisFrame)
        {
            DropHeldObject();
        }

        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            if (grabbedObject == null && hitInfo.collider != null && hitInfo.collider.gameObject.layer == objectsLayer)
            {
                grabbedObject = hitInfo.collider.gameObject;

                if (audioSource != null && pickupSound != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                Movement move = GetComponent<Movement>();
                move.SetCarryingType(grabbedObject.tag);

                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);

                Physics2D.IgnoreCollision(
                    grabbedObject.GetComponent<Collider2D>(),
                    GetComponent<Collider2D>(),
                    true
                );

                grabbedObject.layer = LayerMask.NameToLayer(heldObjectsLayerName);
            }
            else if (grabbedObject != null)
            {
                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.transform.SetParent(null);
                
                Vector3 snapPosition;

                if (lastPlacedObject != null && lastPlacedObject.activeInHierarchy)
                {
                    snapPosition = new Vector3(
                        transform.position.x + horizontalOffset * transform.localScale.x,
                        lastPlacedObject.transform.position.y,
                        0f
                    );
                }
                else
                {
                    snapPosition = new Vector3(
                        transform.position.x + horizontalOffset * transform.localScale.x,
                        transform.position.y + feetOffset,
                        0f
                    );
                }

                grabbedObject.transform.position = snapPosition;

                grabbedObject.layer = groundLayer;

                Physics2D.IgnoreCollision(
                    grabbedObject.GetComponent<Collider2D>(),
                    GetComponent<Collider2D>(),
                    false
                );

                Movement move = GetComponent<Movement>();
                move.SetCarryingType("none");

                GameObject placed = grabbedObject;
                DisappearObjects disappearObjects = placed.GetComponent<DisappearObjects>();
                float delay = 15f;

                if (disappearObjects != null)
                    delay = disappearObjects.disableTime;

                StartCoroutine(DisableAfterDelay(placed, delay));

                lastPlacedObject = placed;
                grabbedObject = null;
            }
        }

        if (grabbedObject != null)
            grabbedObject.transform.position = grabPoint.position;
    }

    public void DropHeldObject()
    {
        if (grabbedObject != null)
        {
            Movement move = GetComponent<Movement>();
            move.SetCarryingType("none");
            grabbedObject.SetActive(false);
            grabbedObject = null;
        }
    }

    private IEnumerator DisableAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeOutMaterial(obj, 1f));
    }

    private IEnumerator FadeOutMaterial(GameObject objectToFade, float fadeSpeed)
    {
        Renderer rend = objectToFade.GetComponent<Renderer>();
        if (rend == null) yield break;

        Color matColor = rend.material.color;
        float alphaValue = matColor.a;

        while (alphaValue > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, Mathf.Max(alphaValue, 0f));
            yield return null;
        }

        rend.material.color = new Color(matColor.r, matColor.g, matColor.b, 0f);
        objectToFade.SetActive(false);
    }
}
