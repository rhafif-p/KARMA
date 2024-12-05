using UnityEngine;
using UnityEngine.UI;

public class JumpscareHUD : MonoBehaviour
{
    public Image BlackBg;  
    public Image Face;     
    public Image Screen;   
    public float fadeDuration = 1.5f; 

    [SerializeField]
    private AudioSource JumpscareSound;

    [SerializeField]
    private GameOverScreen GameOverScreen;

    private bool isJumpscareActive = false;  
    private float elapsedTime = 0f;          

    void Start()
    {
        
        SetAlpha(Face, 0);
        SetAlpha(Screen, 0);
        SetAlpha(BlackBg, 0);
    }

    void Update()
    {
        if (isJumpscareActive)
        {
            elapsedTime += Time.deltaTime;

            float fadeProgress = Mathf.Clamp01(elapsedTime / fadeDuration);

            SetAlpha(Face, 1 - fadeProgress);   // Fade out Face
            SetAlpha(Screen, 1 - fadeProgress); // Fade out Screen

            if (fadeProgress >= 1f)
            {
                isJumpscareActive = false;
                elapsedTime = 0f; 

                GameOverScreen.gameOver();
            }
        }
    }

        public void TriggerJumpscare()
    {
        JumpscareSound.Play();
        SetAlpha(Face, 1);  
        SetAlpha(Screen, 1); 
        SetAlpha(BlackBg, 1); 
        elapsedTime = 0f;    
        isJumpscareActive = true; 
    }

    private void SetAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
