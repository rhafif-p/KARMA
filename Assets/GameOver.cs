using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen; 

    void Start()
    {
        gameOverScreen.SetActive(false); 
    }

    public void TriggerGameOver()
    {
        gameOverScreen.SetActive(true); 
        Time.timeScale = 0f;
    }
}
