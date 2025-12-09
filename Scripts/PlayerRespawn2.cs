using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn2 : MonoBehaviour
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
            Respawns();
        }
    }

    public void Respawns()
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
        GrabObject grabScript = GetComponent<GrabObject>();
        if (grabScript != null)
        {
            grabScript.DropHeldObjects();

        }
    }
}