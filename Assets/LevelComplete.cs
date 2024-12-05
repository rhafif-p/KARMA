using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;


public class LevelComplete : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void levelComplete()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoadNextLevel()
    {
        int nextLevel = DifficultyManager.CurrentDifficultyLevel + 1;

        if (nextLevel > 5) 
        {
            Debug.Log("No more levels available!");
            return;
        }

        DifficultyManager.SetDifficultyLevel(nextLevel);
        SceneManager.LoadSceneAsync(1); 
    }
}
