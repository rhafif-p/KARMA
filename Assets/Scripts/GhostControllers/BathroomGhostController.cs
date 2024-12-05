using Assets.Scripts.Extensions;
using Assets.Scripts.Extensions.Inspector;
using Assets.Scripts.GhostControllers;
using Assets.Scripts.Hud;
using Assets.Scripts.Utilities;
using StarterAssets;
using System;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public enum BathroomGhostState
{
    Active = 1, Inactive = 2
}

public class BathroomGhostController : GhostController<BathroomGhostState>
{
    [SerializeField]
    private RandomizedAudioSource Growl;
    [SerializeField]
    private float ShakeMagnitude;
    [SerializeField]
    private GameObject GhostEntity;
    [SerializeField]
    private GameOverScreen GameOverScreen;
    [SerializeField]
    private AudioSource SuddenSound;
    [SerializeField]
    private BathroomGhostAdditionalJumpscareHandler BathroomGhostAdditionalJumpscareHandler;

    private BoxCollider GhostHitboxCollider => GhostHitbox.GetComponent<BoxCollider>();
    private System.Random CameraShakeRandomizer = new();

    public event Action GhostJumpscares;

    protected override void HandleStateChange(BathroomGhostState newState)
    {
        Debug.Log($"Bathroom Ghost switched state to state {newState.GetHashCode()}!");

        GhostEntity.transform.position = newState == BathroomGhostState.Active ? new Vector3(4.5f, 0f, 5.2f) : new Vector3(5f, 0f, 10f);

        if (newState == BathroomGhostState.Active)
        {
            Growl.Loop();
            GhostEntity.transform.position = new Vector3(4.49f, 0f, 5.17f);
        }
        if (newState == BathroomGhostState.Inactive) Growl.Stop();
    }

    protected override void OnMovementOpportunity()
    {
        int ghostLevel = GameController.Instance.BathroomGhostLevel;
        float random = GameController.Instance.RequestRandom();
        if (random > MovementChance * (1 + ghostLevel / 10f)) return;

        if (State == BathroomGhostState.Inactive
            && !GameController.Instance.BathroomLamp.Active
            && !RaycastUtilities.IsPlayerFlashingAtObject(GhostHitbox))
        {
            SwitchState(BathroomGhostState.Active);
        }
    }

    public override void RunExternalEvent(string eventName)
    {
        switch (eventName)
        {
            case "BathroomLampEnabled":
                if (State == BathroomGhostState.Active) SwitchState(BathroomGhostState.Inactive);
                break;
        }
    }

    private IEnumerator RunJumpscareStoryboard()
    {
        float startTime = Time.time;
        float lockDuration = 2f;
        FirstPersonController firstPersonController = GameController.Instance.PlayerFirstPersonController;

        // Freeze ghost and player
        GameController.Instance.PlayerFirstPersonController.DisablePlayerInput();

        SuddenSound.Play();

        BathroomGhostAdditionalJumpscareHandler.StartAnimation();
        while (!BathroomGhostAdditionalJumpscareHandler.AnimationFinished) yield return null;

        SuddenSound.Stop();

        GameController.Instance.GhostJumpscareHandler.StartAnimation();
        while (!GameController.Instance.GhostJumpscareHandler.AnimationFinished) yield return null;

        GameController.Instance.PlayerFirstPersonController.EnablePlayerInput();
        GameOverScreen.gameOver();
    }

    public override void Jumpscare()
    {
        GhostJumpscares?.Invoke();
        
        StartCoroutine(RunJumpscareStoryboard());
    }

    private void ShakeCamera(float magnitude)
    {
        GameController.Instance.PlayerFirstPersonController.ShakeCamera(magnitude);
    }

    private void OnFlashlightStateActive(bool active)
    {
        if (!active) return;
        if (State == BathroomGhostState.Active && PositionUtilities.IsPlayerWithin(GhostHitbox)) Jumpscare();
    }

    protected override void Start()
    {
        base.Start();
        GhostHitboxCollider.enabled = false;
        GameController.Instance.Flashlight.OnFlashlightStateChanged += OnFlashlightStateActive;
        Debug.Log(GhostHitboxCollider.enabled);
    }

    private void Update()
    {
        if (State == BathroomGhostState.Active && PositionUtilities.IsPlayerWithin(GhostHitbox)) ShakeCamera(ShakeMagnitude);
    }
}
