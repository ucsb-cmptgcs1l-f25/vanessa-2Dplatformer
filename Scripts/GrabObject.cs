using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabbedObject;
    private GameObject lastPlacedObject;
    private GameObject lastFreeObject;

    private int objectsLayer;
    private int groundLayer;
    private string heldObjectsLayerName = "HeldObjects";
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound; 

    void Start()
    {
        objectsLayer = LayerMask.NameToLayer("Objects");
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        if ((Keyboard.current[Key.LeftShift]).wasPressedThisFrame || (Keyboard.current[Key.RightShift]).wasPressedThisFrame)
        {
            DropHeldObject();
        }
        if (Keyboard.current[Key.W].wasPressedThisFrame)
        {
            if (grabbedObject == null && hitInfo.collider != null &&
                hitInfo.collider.gameObject.layer == objectsLayer)
            {
                grabbedObject = hitInfo.collider.gameObject;
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }
                Movement move = GetComponent<Movement>();
                string tagType = grabbedObject.tag;
                move.SetCarryingType(tagType);
                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
                grabbedObject.layer = LayerMask.NameToLayer(heldObjectsLayerName);
            }
            else if (grabbedObject != null)
            {
                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;

                grabbedObject.transform.SetParent(null);
                grabbedObject.layer = groundLayer;

                lastFreeObject = grabbedObject;

                GameObject placed = grabbedObject;
                DisappearObjects disappearObjects = placed.GetComponent<DisappearObjects>();
                float delay = disappearObjects != null ? disappearObjects.disableTime : 15f;

                StartCoroutine(DisableAfterDelay(placed, delay));
                Movement move = GetComponent<Movement>();
                move.SetCarryingType("none");
                lastFreeObject = placed;
                grabbedObject = null;
            }
        }

        if (grabbedObject != null)
            grabbedObject.transform.position = grabPoint.position;

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }

    public void DropHeldObjects()
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
}
