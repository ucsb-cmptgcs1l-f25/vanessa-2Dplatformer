
using UnityEngine;
    using TMPro;

    public class Countdown : MonoBehaviour
    {
        public TMP_Text timerText;
        public float countdownTime = 60f; // seconds

        void Update()
        {
            Tick(Time.deltaTime);
        }

        // Make a public method for testing
        public void Tick(float deltaTime)
        {
            if (countdownTime > 0)
            {
                countdownTime -= deltaTime;
                if (countdownTime < 0) countdownTime = 0;

                float minutes = Mathf.Floor(countdownTime / 60);
                float seconds = Mathf.Floor(countdownTime % 60);
                if ((object)timerText != null)
                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                if ((object)timerText != null)
                    timerText.text = "00:00";
            }
        }
    }
