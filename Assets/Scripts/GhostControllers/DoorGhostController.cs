using Assets.Scripts.Doors;
using Assets.Scripts.Utilities;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GhostControllers
{
    public enum DoorGhostState
    {
        Inactive = 1, Peeking = 2, Blocking = 3, Hunting = 4
    }
    public class DoorGhostController : GhostController<DoorGhostState>
    {
        [SerializeField]
        private GameOverScreen GameOverScreen;
        [SerializeField]
        private Door MainDoor;
        [SerializeField]
        private Transform FrontHitbox;
        [SerializeField]
        private AudioSource GhostRun;
        [SerializeField]
        private AudioSource SuddenSound;

        public event Action GhostJumpscares;

        private bool HasJumpscared = false;

        private Renderer FrontHitboxRenderer => FrontHitbox.GetComponent<MeshRenderer>();

        private Vector3 InactivePosition = new(-1.3f, 0f, 9f);
        private Vector3 BlockingPosition = new(-2.25f, 0f, 5f);

        private void OnMainDoorStateChanged(bool doorState)
        {
            Debug.Log("Door Changed State");
            SwitchState(doorState ? DoorGhostState.Peeking : DoorGhostState.Inactive);
        }

        private bool CanGhostSwitchToBlocking()
        {
            return !FrontHitboxRenderer.isVisible;
        }

        protected override void HandleStateChange(DoorGhostState newState)
        {
            Debug.Log($"Door Ghost switched state to state {newState.GetHashCode()}!");

            if (newState == DoorGhostState.Inactive || newState == DoorGhostState.Peeking) GhostObject.position = InactivePosition;
            if (newState == DoorGhostState.Blocking)
            {
                GhostObject.position = BlockingPosition;
                GhostRun.Play();
            }

            if (State == DoorGhostState.Inactive && newState == DoorGhostState.Peeking) MainDoor.SetState(true);
        }

        protected override void OnMovementOpportunity()
        {
            int ghostLevel = GameController.Instance.DoorGhostLevel;
            float random = GameController.Instance.RequestRandom();
            if (random > MovementChance * (1 + ghostLevel / 10f)) return;

            if (State == DoorGhostState.Inactive) SwitchState(DoorGhostState.Peeking);
            else if (State == DoorGhostState.Peeking &&
                !GameController.Instance.FrontLamp.Active &&
                CanGhostSwitchToBlocking()) SwitchState(DoorGhostState.Blocking);
        }

        public override void RunExternalEvent(string eventName)
        {
            switch (eventName)
            {
                case "BathroomLampEnabled":
                    if (State == DoorGhostState.Blocking && GameController.Instance.BathroomDoor.Open) SwitchState(DoorGhostState.Peeking);
                    break;
                case "FrontLampEnabled":
                    if (State == DoorGhostState.Blocking) SwitchState(DoorGhostState.Peeking);
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
            HasJumpscared = true;
            GhostJumpscares?.Invoke();
            StartCoroutine(RunJumpscareStoryboard());
        }

        protected override void Start()
        {
            base.Start();
            MainDoor.OnDoorStateChanged += OnMainDoorStateChanged;
        }

        private void Update()
        {
            if (State == DoorGhostState.Blocking && RaycastUtilities.IsPlayerFlashingAtObject(GhostHitbox))
                GameController.Instance.Flashlight.Flicker();
            if (DistanceUtilities.PlayerFlatDistanceFrom(GhostObject) < 2f && !HasJumpscared) Jumpscare();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (MainDoor != null)
                MainDoor.OnDoorStateChanged -= OnMainDoorStateChanged;
        }
    }
}
