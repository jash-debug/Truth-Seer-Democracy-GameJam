using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;                    // Array to store button references
    public GameObject[] levelButtonsParent;     // Array of parent GameObjects holding buttons

    private void Awake()
    {
        ButtonsToArray(); // Populate the buttons array

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Disable all buttons by default
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Enable unlocked buttons
        for (int i = 0; i < unlockedLevel && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        AudioManager.Instance.PlaySfx("LevelStart");
        StartCoroutine(LoadLevel(levelId));
    }

    IEnumerator LoadLevel(int levelId)
    {
        string levelName = "level " + levelId;

        GameManager.instance.transitionAnim.SetTrigger("End"); // Play transition animation
        yield return new WaitForSeconds(1);                    // Wait for transition to complete
        SceneManager.LoadScene(levelName);                     // Load the level
        GameManager.instance.transitionAnim.SetTrigger("Start");
    }

    // To reset my saved data
    public void ResetGame()
    {
        //PlayerPrefs.DeleteAll();
        // Quit the application
        Application.Quit();
    }

    void ButtonsToArray()
    {
        // Calculate total number of buttons across all parents
        int totalButtons = 0;
        foreach (GameObject parent in levelButtonsParent)
        {
            if (parent != null)
                totalButtons += parent.transform.childCount;
        }

        // Initialize the buttons array
        buttons = new Button[totalButtons];

        int index = 0; // Index to populate the buttons array
        foreach (GameObject parent in levelButtonsParent)
        {
            if (parent != null)
            {
                int childCount = parent.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    // Get Button component from each child
                    Button button = parent.transform.GetChild(i).GetComponent<Button>();
                    if (button != null)
                    {
                        buttons[index] = button;
                        index++;
                    }
                    else
                    {
                        Debug.LogWarning($"Child {i} of {parent.name} does not have a Button component!");
                    }
                }
            }
        }
    }
}
