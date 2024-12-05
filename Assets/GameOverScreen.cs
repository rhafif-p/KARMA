using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;


public class GameOverScreen : MonoBehaviour
{
    public GameObject[] uiElementsToDisable;  

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void gameOver()
    {
        foreach (GameObject uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(false);
        }

        gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartButton()
    {
        int currentLevel = DifficultyManager.CurrentDifficultyLevel;

        Debug.Log("Restartinggg Game");

        // Hide the cursor and lock it again
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        DifficultyManager.SetDifficultyLevel(currentLevel);
        SceneManager.LoadSceneAsync(1);  // Load scene with index 1
    }

}
