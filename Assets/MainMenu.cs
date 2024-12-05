using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject PlayButton; 
    public GameObject QuitButton; 
    public GameObject LevelSelect; 

    private void Start()
    {
        LevelSelect.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        LevelSelect.SetActive(true);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
    }

    public void SelectLevel(int level)
    {
        DifficultyManager.SetDifficultyLevel(level);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}