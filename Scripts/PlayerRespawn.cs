using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn : MonoBehaviour
{
    public Vector2 respawnPoint;
    public int maxLives = 3;
    private int respawnCounter = 0;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        respawnCounter++;
        if (respawnCounter == 1)
        {
            life1.SetActive(false);
        }
        else if (respawnCounter == 2)
        {
            life2.SetActive(false);
        }
        if (respawnCounter >= maxLives)
        {
            SceneManager.LoadScene("YouLose"); 
            return;
        }
        transform.position = respawnPoint;
        GrabObjects grabScript = GetComponent<GrabObjects>();
        if (grabScript != null)
        {
            grabScript.DropHeldObject();

        }
    }
}

