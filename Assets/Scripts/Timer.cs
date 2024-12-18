using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;        // UI text to display the timer
    public GameObject[] stars;       // Array of star GameObjects (assign in Inspector)
    public float timeLimit = 30f;    // Time threshold for earning 3 stars

    private float currentTime = 0f;  // Timer to track elapsed time
    private int starRating = 0;      // Rating (0 to 3 stars)
    private bool isTimerRunning = true;

    void Start()
    {
        ResetStars(); // Ensure all stars are initially inactive
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime; // Track elapsed time
            UpdateTimerUI();
            CalculateStarRating();

            if (currentTime > timeLimit)
            {
                isTimerRunning = false; // Stop the timer
                
            }
        }
    }

    public void StartDisplayStarsCoroutine()
    {
        StartCoroutine(DisplayStarsAfterDelay());
    }

    void UpdateTimerUI()
    {
        // Format and display the timer as minutes:seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ResetStars()
    {
        // Deactivate all star GameObjects
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }

    void CalculateStarRating()
    {
        // Determine the star rating based on elapsed time
        if (currentTime <= (8 + (SceneManager.GetActiveScene().buildIndex * 2))) 
        {
            starRating = 3; // Best rating
        }
        else if (currentTime <= ((8 + (SceneManager.GetActiveScene().buildIndex * 2)) + 5))
        {
            starRating = 2; // Average rating
        }
        else
        {
            starRating = 1; // Minimum rating
        }
    }

    IEnumerator DisplayStarsAfterDelay()
    {
       
        // Wait 1 second before starting to display stars
        yield return new WaitForSecondsRealtime(1f);

        // Loop through the stars and display one at a time
        for (int i = 0; i < starRating; i++)
        {
            stars[i].SetActive(true); // Activate the star
            AudioManager.Instance.PlaySfx("StarWin");
            yield return new WaitForSecondsRealtime(1f); // Wait 1 second before showing the next star

            

        }
    }
}
