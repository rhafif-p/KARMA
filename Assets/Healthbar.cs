using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;  // The slider component.
    public Text percentageText;  // Reference to a Text component to display percentage.

    // Method to set the value of the slider and update the percentage text.
    public void SetSlider(float amount)
    {
        healthSlider.value = amount;
        UpdatePercentageText(amount);
    }

    // Method to set the max value of the slider and update the percentage text.
    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount;
        SetSlider(amount);  // Set the current value after max value update.
    }

    // Method to update the percentage text displayed on the UI.
    private void UpdatePercentageText(float amount)
    {
        if (percentageText != null)
        {
            // Calculate the percentage and set it to the Text component.
            float percentage = (amount / healthSlider.maxValue) * 100;
            percentageText.text = Mathf.RoundToInt(percentage) + "%";  // Round to nearest whole number.
        }
    }
}
