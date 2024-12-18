using System;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float timer = 450f; // Start at 7 minutes 30 seconds (450 seconds)
    private float endTime = 480f; // End at 8 minutes (480 seconds)
    private float speedUpFactor = 10f; // Accelerate time (7:30 to 8:00 in 3 real-world minutes)

    void Update()
    {
        RunAcceleratedTimer();
    }

    void RunAcceleratedTimer()
    {
        if (timer < endTime)
        {
            // Accelerate time to complete in 3 minutes
            timer += (Time.deltaTime * ((endTime - 450f) / 180f));
            DisplayTime(timer);
        }
        else
        {
            timer = endTime; // Cap the timer at 8:00
            DisplayTime(timer);
            Debug.Log("Timer complete: Reached 8:00.");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Clamp(timeToDisplay, 0, Mathf.Infinity);

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
