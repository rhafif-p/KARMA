using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public Healthbar healthBar;
    private Renderer objectRenderer;  // Reference to the object's Renderer
    private Color originalColor;      // Store the original color of the object
    public GameOverScreen gameOverScreen;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);

        // Get the object's Renderer and store its original color
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);

        // If the object has a Renderer, change the material color to red
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.red;
        }

        // Optional: Destroy or disable the object if health reaches zero
        if (currentHealth <= 0)
        {
            Debug.Log("Object is dead");
            gameOverScreen.gameOver();
            Destroy(gameObject); 
        }

        // Start a coroutine to reset the color after a short delay
        StartCoroutine(ResetColorAfterDamage());
    }

    private System.Collections.IEnumerator ResetColorAfterDamage()
    {
        // Wait for 0.5 seconds (you can adjust this time)
        yield return new WaitForSeconds(0.5f);

        // Reset the object's material color to the original color
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}
