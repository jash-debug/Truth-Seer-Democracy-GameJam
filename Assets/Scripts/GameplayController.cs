using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public Timer timer;

    public GameObject nextLevelObject,pauseMenu;
    public TMP_Text scoreText, levelText,levelPause, levelNext; // UI Texts
    public int score = 0; // Current score
    public BoxSpawner boxSpawner; // Box spawner reference
    private int moveCount = 0; // Counter for camera movement
    public CameraScript cameraFollow; // Camera follow script reference
    [HideInInspector] public BoxScript currentBox; // Current active box

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }

        // Initialize level text
        levelText.text = "Level: " + SceneManager.GetActiveScene().buildIndex.ToString();
        scoreText.text = "Score: " + score.ToString() + " / " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        levelNext.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        levelPause.text = "Level: " + SceneManager.GetActiveScene().buildIndex.ToString();

        Debug.Log("Current Level Index: " + SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        // Spawn the first box at the start of the game
        Time.timeScale = 1;
        boxSpawner.SpawnBox();
    }

    void Update()
    {
        // Detect player input
        DetectInput();
    }

    void DetectInput()
    {
        // Check for mouse click to drop the current box
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
            AudioManager.Instance.PlaySfx("Drop");
        }

        // Check if score equals 3 to activate the "next level" object
        if (score == SceneManager.GetActiveScene().buildIndex + 1 && Time.timeScale > 0)
        {
            
            nextLevelObject.SetActive(true);
            timer.StartDisplayStarsCoroutine();
            Time.timeScale = 0;


        }
    }

    public void GoNextLevel()
    {
        // Unlock the next level and load it
        Time.timeScale = 1;
        UnlockedNewLevel();
        GameManager.instance.NextLevel();
        score = 0;
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void Play()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    void UnlockedNewLevel()
    {
        // Check and update PlayerPrefs to unlock the next level
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLevelIndex >= PlayerPrefs.GetInt("ReachedIndex", 1))
        {
            PlayerPrefs.SetInt("ReachedIndex", currentLevelIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void AddScore()
    {
        // Increment score and update UI
        score++;
        scoreText.text = "Score: " + score.ToString() + " / " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    public void SpawnNewBox()
    {
        // Delay spawning the next box
        Invoke("NewBox", 0.5f);
    }

    void NewBox()
    {
        // Spawn a new box
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        // Increment move count and adjust camera position after every 3 moves
        moveCount++;

        if (moveCount == 3)
        {
            moveCount = 0;
            cameraFollow.targetPos.y += 2f;
        }
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
}
