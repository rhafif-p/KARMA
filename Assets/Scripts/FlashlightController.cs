using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FlashlightController : MonoBehaviour
{
    

    //[Header("UI Elements")]
    //public Healthbar BatteryBar; 

    [Header("Flashlight Settings")]
    public Light FlashlightSpotLight;

    [Header("Battery Settings")]
    [SerializeField, Range(0f, 1f)]
    private float _battery = 1f;
    public float Battery
    {
        get => _battery;
        private set => _battery = Mathf.Clamp(value, 0, 1f);
    }
    public float BatteryDrainRate = 0.05f;
    public float LowBatteryThreshold = 0.2f;
    public event Action<float, float> OnFlashlightBatteryChanged;

    [Header("Audio")]
    public AudioSource turnOn;
    public AudioSource turnOff;
    public AudioSource FlickerSound;

    [Header("State")]
    public bool Active = false;
    public event Action<bool> OnFlashlightStateChanged;

    [SerializeField]
    private bool ShouldFlicker = false;
    private readonly System.Random FlickerRandomizer = new();
    private float LastFlicker = 0f;

    /// <summary>
    /// Sets the flashlight state for the next frame.
    /// </summary>
    /// <param name="state"></param>
    public void SetState(bool state)
    {
        bool prev = Active;
        // Force inactive if battery is dead
        Active = Battery > 0 && state;

        // Play appropriate sound
        if (state) turnOn.Play();
        else turnOff.Play();

        // Invoke state change event
        if (Active != prev) OnFlashlightStateChanged?.Invoke(Active);
    }

    private void DrainBattery()
    {
        if (!Active) return;

        float prev = Battery;

        // Constant drain if active
        Battery -= BatteryDrainRate * Time.deltaTime * GameController.Instance.FlashlightDepletionMultiplier;
        Battery = Mathf.Clamp(Battery, 0f, 1f);

        OnFlashlightBatteryChanged?.Invoke(prev, Battery);
    }

    /// <summary>
    /// Call every frame to produce a flicker effect.
    /// </summary>
    public void Flicker()
    {
        if (!Active) return;

        bool canPerformFlicker = false;
        if (Time.time - LastFlicker > 0.2f) canPerformFlicker = true; // Flicker anyway if last flicker is 200ms away
        if (FlickerRandomizer.NextDouble() < 0.1f) canPerformFlicker = true; // Randomizer
        if (Time.time - LastFlicker < 0.05f) canPerformFlicker = false; // Don't flicker if last flicker is less than 50ms away

        if (canPerformFlicker) PerformFlicker();
    }

    private void PerformFlicker()
    {
        FlashlightSpotLight.enabled = false;
        LastFlicker = Time.time;
    }

    private void Start()
    {
        FlashlightSpotLight.enabled = Active;
        Battery = 1f;
    }

    private void Update()
    {
        FlashlightSpotLight.enabled = Active;

        // Keep disabled for at least 50ms
        if (Time.time - LastFlicker < 0.05) FlashlightSpotLight.enabled = false;

        // Switch state on key press
        if (Input.GetButtonDown("F")) SetState(!Active);

        DrainBattery();

        // Turn off flashlight if battery is dead
        if (Active && Battery <= 0) SetState(false);

        if (ShouldFlicker) Flicker();

    }

    public void RechargeBattery(float amount)
    {
        float prev = Battery;

        Battery += amount;
        Battery = Mathf.Clamp(Battery, 0, 1f);

        OnFlashlightBatteryChanged?.Invoke(prev, Battery);
    }
}
