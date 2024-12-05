using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField]
        private bool EnableProfiler = false;
        [SerializeField]
        private float DistanceFromPlayer;
        [SerializeField]
        private bool Interactible = false;

        public virtual string HoverMessage { get; private set; }
        public virtual float InteractibleDistance => 2f;
        public InteractiveObjectHitbox Hitbox;
        public abstract void OnInteract();
        public virtual bool IsPlayerWithinInteractibleDistance()
        {
            if (InteractibleDistance <= 0f) return true;
            return DistanceUtilities.PlayerFlatDistanceFrom(transform) <= InteractibleDistance;
        }
        private void Update()
        {
            if (EnableProfiler)
            {
                DistanceFromPlayer = DistanceUtilities.PlayerFlatDistanceFrom(transform);
                Interactible = IsPlayerWithinInteractibleDistance();
            }
        }
    }
}
