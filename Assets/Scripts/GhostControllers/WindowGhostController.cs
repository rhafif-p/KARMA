using Assets.Scripts.Extensions;
using Assets.Scripts.Furnitures;
using Assets.Scripts.GhostControllers;
using Assets.Scripts.Utilities;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum WindowGhostState
{
    Inactive = 1, Hiding = 2, Lurking = 3
}

public class WindowGhostController : GhostController<WindowGhostState>
{
    [SerializeField]
    private GameOverScreen GameOverScreen;
    [SerializeField]
    private AudioSource WindowThump;
    [SerializeField]
    private AudioSource Whoosh;
    [SerializeField]
    private RandomizedAudioSource GhostStep;
    [SerializeField]
    private AudioSource SuddenSound;

    [SerializeField]
    private Transform CurtainHitbox;
    [SerializeField]
    private Transform CornerHitbox;

    // Additional effects
    [SerializeField]
    private LargeSofa SofaObject;

    private float LastStep = 0f;
    private NavMeshAgent NavigationAgent;

    // Disable Candle Ability
    [SerializeField]
    private float DisableCandleCooldown = 30f;

    private bool IsFrozen = false;

    protected override void HandleStateChange(WindowGhostState newState)
    {
        Debug.Log($"Window Ghost switched state to state {newState.GetHashCode()}!");
        if (newState == WindowGhostState.Inactive || newState == WindowGhostState.Hiding) NavigationAgent.enabled = false;
        if (newState == WindowGhostState.Lurking) NavigationAgent.enabled = true;

        if (newState == WindowGhostState.Inactive) GhostObject.transform.position = new Vector3(0, 0, -10);
        if (newState == WindowGhostState.Hiding) GhostObject.transform.position = new Vector3(3.8f, 0, -7);

        // Reset cooldown
        if (newState == WindowGhostState.Hiding) DisableCandleCooldown = 30f;

        if (State == WindowGhostState.Inactive && newState == WindowGhostState.Hiding) WindowThump.Play();
        if (State == WindowGhostState.Lurking && newState == WindowGhostState.Hiding) SofaObject.RotateSlightly();
    }

    protected override void OnMovementOpportunity()
    {
        int ghostLevel = GameController.Instance.WindowGhostLevel;
        float random = GameController.Instance.RequestRandom();
        if (random > MovementChance * (1 + ghostLevel / 10f)) return;

        if (State == WindowGhostState.Inactive &&
            !GameController.Instance.CornerLamp.Active &&
            !RaycastUtilities.IsPlayerFlashingAtObject(CurtainHitbox)) SwitchState(WindowGhostState.Hiding);
        else if (State == WindowGhostState.Hiding &&
            !GameController.Instance.MainLamp.Active &&
            !GameController.Instance.CornerLamp.Active &&
            !GameController.Instance.TableCandle.Active &&
            !RaycastUtilities.IsPlayerFlashingAtObject(CornerHitbox)) SwitchState(WindowGhostState.Lurking);
    }

    public override void RunExternalEvent(string eventName)
    {
        switch (eventName)
        {
            case "CornerLampEnabled":
                if (State == WindowGhostState.Hiding) SwitchState(WindowGhostState.Inactive);
                break;
        }
    }
    private IEnumerator RunJumpscareStoryboard()
    {
        float startTime = Time.time;
        float lockDuration = 2f;
        FirstPersonController firstPersonController = GameController.Instance.PlayerFirstPersonController;

        // Freeze ghost and player
        IsFrozen = true;
        GameController.Instance.PlayerFirstPersonController.DisablePlayerInput();

        SuddenSound.Play();

        while (Time.time - startTime < lockDuration)
        {
            firstPersonController.ForceRotateCameraTowardsTarget(GhostObject.transform.position);
            yield return null;
        }

        SuddenSound.Stop();

        GameController.Instance.GhostJumpscareHandler.StartAnimation();
        while (!GameController.Instance.GhostJumpscareHandler.AnimationFinished) yield return null;

        GameController.Instance.PlayerFirstPersonController.EnablePlayerInput();
        GameOverScreen.gameOver();
    }

    public override void Jumpscare()
    {
        GhostObject.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(RunJumpscareStoryboard());
    }
    private void ThreatenMultipleCandleUsage()
    {

    }
    private void DisableTableCandle()
    {
        GameController.Instance.TableCandle.SetState(false);
        Whoosh.Play();

        DisableCandleCooldown -= 5f;
        DisableCandleCooldown = Mathf.Clamp(DisableCandleCooldown, 0.5f, 30f);

        if (DisableCandleCooldown <= 0.5f) ThreatenMultipleCandleUsage();
    }

    private void OnOtherGhostJumpscares()
    {
        IsFrozen = true;
    }

    protected override void Start()
    {
        base.Start();
        SwitchState(WindowGhostState.Inactive);
        NavigationAgent = GhostObject.GetComponent<NavMeshAgent>();
        GhostObject.GetComponent<MeshRenderer>().enabled = true;

        GameController.Instance.BathroomGhostController.GhostJumpscares += OnOtherGhostJumpscares;
        GameController.Instance.DoorGhostController.GhostJumpscares += OnOtherGhostJumpscares;
    }

    protected void Update()
    {
        if (IsFrozen)
        {
            if (NavigationAgent.isOnNavMesh)
                NavigationAgent.SetDestination(GhostObject.transform.position);
            return;
        }

        if (State == WindowGhostState.Hiding)
        {
            // Ensure the agent is not trying to navigate while in the Hiding state.
            if (NavigationAgent.enabled && NavigationAgent.isOnNavMesh)
            {
                NavigationAgent.SetDestination(GhostObject.transform.position);
            }
            return;
        }

        if (State == WindowGhostState.Lurking)
        {
            if (RaycastUtilities.IsPlayerFlashingAtObject(GhostHitbox))
            {
                SwitchState(WindowGhostState.Hiding);
                return;
            }

            if (NavigationAgent.enabled && NavigationAgent.isOnNavMesh)
            {
                NavigationAgent.SetDestination(GameController.Instance.PlayerCamera.transform.position);
            }

            GhostStep.transform.position = GhostObject.position;
            if (Time.time - LastStep > 2f)
            {
                GhostStep.Play();
                LastStep = Time.time;
            }

            if (DistanceUtilities.PlayerFlatDistanceFrom(GhostObject) < 2f)
            {
                Jumpscare();
            }
        }
    }

}
