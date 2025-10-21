using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector2 respawnPoint; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint;
    }
}
