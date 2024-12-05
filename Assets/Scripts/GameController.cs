using Assets.Scripts;
using Assets.Scripts.Controller;
using Assets.Scripts.DebugTools;
using Assets.Scripts.Doors;
using Assets.Scripts.GhostControllers;
using Assets.Scripts.LightSources;
using Assets.Scripts.Services;
using Assets.Scripts.Utilities;
using NUnit.Framework;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Assets.Scripts.Hud;



public class GameController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    public static GameController Instance;
    private float StartTime = 0f;
    public float GameTime => Time.time - StartTime;
    public float TimeProgress => GameTime / MaxTime;

    [Header("Jumpscare References")]
    public JumpscareHUD JumpscareHUD;
    public Transform TestCapsule; 
    public GameOverScreen GameOverScreen;
    [SerializeField]
    private AudioSource BuildUp;

    [Header("Ghost Jumpscare Settings")]
    public float CapsuleFollowDistance = 2.0f;
    public float CapsuleSmoothSpeed = 2f;

    private bool isFollowing = false;

    [Header("Difficulty Modifier")]
    public float MaxTime = 300f;
    public float MovementOpportunityInterval = 5f;
    public float FlashlightDepletionMultiplier = 1f;
    public float GeneratorDepletionMultiplier = 1f;
    public int WindowGhostLevel = 1;
    public int DoorGhostLevel = 1;
    public int BathroomGhostLevel = 1;

    public event Action OnMovementOpportunity;
    public event Action OnRaycastDoneEvaluating;

    public List<RaycastHit> PlayerRaycastHits => _PlayerRaycastHits.Take(_PlayerRaycastHitCount).ToList();
    private readonly RaycastHit[] _PlayerRaycastHits = new RaycastHit[100];
    private int _PlayerRaycastHitCount = 0;

    [Header("Profiler")]
    [SerializeField]
    private bool EnableProfiler = false;
    [SerializeField]
    private RaycastHitSerializable[] RaycastHitsSerialized;

    [Header("Object Assignment")]
    public GeneratorController Generator;
    public Camera PlayerCamera;
    public Collider PlayerCollider;
    public FirstPersonController PlayerFirstPersonController;
    public PlayerInventory PlayerInventory;

    // Light Sources
    public FlashlightController Flashlight;
    public CornerLamp CornerLamp;
    public MainLamp MainLamp;
    public FrontLamp FrontLamp;
    public BathroomLamp BathroomLamp;
    public Candle TableCandle;
    public Candle FloorCandle;

    // Doors
    public Door BathroomDoor;
    public Door FrontDoor;

    // Ghosts
    public WindowGhostController WindowGhostController;
    public DoorGhostController DoorGhostController;
    public BathroomGhostController BathroomGhostController;

    public GhostJumpscareHandler GhostJumpscareHandler;
    public LevelComplete LevelComplete;

    private float timer;
    private System.Random random = new();

    private void EvaluateMovementOpportunity()
    {
        timer += Time.deltaTime;

        if (timer >= MovementOpportunityInterval)
        {
            OnMovementOpportunity?.Invoke();
            timer = 0f;
        }
    }

    public void Jumpscare()
    {
        PositionGhostInFrontOfPlayer();
        StartCoroutine(LockCameraAndZoomToGhost());


        PlayerFirstPersonController.enabled = false;
    }

    private IEnumerator LockCameraAndZoomToGhost()
    {
        float lockDuration = 1f;
        float elapsedTime = 0f;
        BuildUp.Play();

        Vector3 initialCameraPosition = PlayerCamera.transform.position;
        Vector3 targetPosition = TestCapsule.position + TestCapsule.forward * 1f;

        while (elapsedTime < lockDuration)
        {
            float t = elapsedTime / lockDuration;
            PlayerCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayerCamera.transform.position = targetPosition;

        float zoomDuration = 1f;
        elapsedTime = 0f;

        // Adjust Cinemachine Virtual Camera's Field of View
        float initialFOV = VirtualCamera.m_Lens.FieldOfView;
        float zoomedFOV = 20f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(initialFOV, zoomedFOV, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        VirtualCamera.m_Lens.FieldOfView = zoomedFOV;

        BuildUp.Stop();

        JumpscareHUD.TriggerJumpscare();
    }

    private void PositionGhostInFrontOfPlayer()
    {
        if (TestCapsule != null && PlayerCamera != null)
        {
            Vector3 targetPosition = PlayerCamera.transform.position + PlayerCamera.transform.forward * CapsuleFollowDistance;
            TestCapsule.position = targetPosition;

            TestCapsule.rotation = Quaternion.LookRotation(PlayerCamera.transform.position - TestCapsule.position);
        }
    }

    private void ApplyDifficulty()
    {
        MaxTime = DifficultyManager.MaxTime;
        MovementOpportunityInterval = DifficultyManager.MovementOpportunityInterval;

        FlashlightDepletionMultiplier = DifficultyManager.FlashlightDepletionMultiplier;
        GeneratorDepletionMultiplier = DifficultyManager.GeneratorDepletionMultiplier;
        WindowGhostLevel = 30;
        DoorGhostLevel = DifficultyManager.DoorGhostLevel;
        BathroomGhostLevel = DifficultyManager.BathroomGhostLevel;

        Debug.Log("GameController - Difficulty Applied:");
        Debug.Log($"MaxTime: {MaxTime}");
        Debug.Log($"MovementOpportunityInterval: {MovementOpportunityInterval}");
        Debug.Log($"FlashlightDepletionMultiplier: {FlashlightDepletionMultiplier}");
        Debug.Log($"GeneratorDepletionMultiplier: {GeneratorDepletionMultiplier}");
        Debug.Log($"WindowGhostLevel: {WindowGhostLevel}");
        Debug.Log($"DoorGhostLevel: {DoorGhostLevel}");
        Debug.Log($"BathroomGhostLevel: {BathroomGhostLevel}");
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ApplyDifficulty();
        StartTime = Time.time;
    }

    void Update()
    {
        EvaluateMovementOpportunity();
        EvaluateRaycastHits();



        if (GameTime >= MaxTime) LevelComplete.levelComplete();
    }

    private void EvaluateRaycastHits()
    {
        _PlayerRaycastHitCount = CountRaycastObjectsWithinRaycastWall(_PlayerRaycastHits,
            Physics.RaycastNonAlloc(PlayerCamera.transform.position, PlayerCamera.transform.forward, _PlayerRaycastHits, 20f));

        OnRaycastDoneEvaluating.Invoke();

        for (int i = 0; i < _PlayerRaycastHitCount; i++)
        {
            RaycastHit hit = _PlayerRaycastHits[i];
            if (Input.GetButtonDown("E") &&
                hit.collider.TryGetComponent<InteractiveObjectHitbox>(out var interactiveObjectHitbox) &&
                interactiveObjectHitbox.ParentObject.IsPlayerWithinInteractibleDistance())
                interactiveObjectHitbox.ParentObject.OnInteract();
        }
    }

    private int CountRaycastObjectsWithinRaycastWall(RaycastHit[] hits, int count)
    {
        Array.Sort(hits, 0, count, new RaycastHitDistanceComparer());

        for (int i = 0; i < count; i++) if (hits[i].transform != null && hits[i].transform.CompareTag("RaycastWall")) return i;
        return count;
    }

    public float RequestRandom()
    {
        return (float)random.NextDouble();
    }
}
