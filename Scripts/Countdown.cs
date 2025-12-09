using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text timerText;
    public float countdownTime = 90f; // seconds
    private float currentTime;
    private PlayerRespawn respawn;

    void Start()
    {
        currentTime = countdownTime;
        respawn = PlayerRespawn.FindFirstObjectByType<PlayerRespawn>();
    }

    void Update()
    {
        Tick(Time.deltaTime);
    }

    public void Tick(float deltaTime)
    {
        if (currentTime > 0)
        {
            currentTime -= deltaTime;
            if (currentTime < 0) currentTime = 0;

            float minutes = Mathf.Floor(currentTime / 60);
            float seconds = Mathf.Floor(currentTime % 60);
            if (timerText != null)
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            if (timerText != null)
                timerText.text = "00:00";

            // Respawn player and reset timer
            if (respawn != null)
            {
                respawn.Respawn();
                currentTime = countdownTime; // reset to 1:30
            }
        }
    }
}