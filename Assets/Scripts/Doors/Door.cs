using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Doors
{
    public class Door : InteractiveObject
    {
        public bool Open;
        public Action<bool> OnDoorStateChanged;
        [SerializeField]
        protected Transform DoorObject;
        public override float InteractibleDistance => 3.5f;
        public override string HoverMessage => Open ? "Close" : "Open";
        [SerializeField]
        protected Quaternion closedRotation = Quaternion.Euler(0, 0, 0);
        [SerializeField]
        protected Quaternion openRotation = Quaternion.Euler(0, 0, 0);

        public void SetState(bool state)
        {
            if (Open == state) return;

            Open = state;
            OnDoorStateChanged?.Invoke(Open);
        }

        public override void OnInteract()
        {
            SetState(!Open);
        }

        public override bool IsPlayerWithinInteractibleDistance()
        {
            if (InteractibleDistance <= 0f) return true;
            return DistanceUtilities.PlayerFlatDistanceFrom(DoorObject.transform) <= InteractibleDistance;
        }

        public void Update()
        {
            if (Open) transform.rotation = Quaternion.RotateTowards(transform.rotation, openRotation, Time.deltaTime * 90);
            else transform.rotation = Quaternion.RotateTowards(transform.rotation, closedRotation, Time.deltaTime * 90);
        }
    }
}
