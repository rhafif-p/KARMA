using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GhostControllers
{
    public abstract class GhostController<TState> : MonoBehaviour where TState : Enum
    {
        public virtual float MovementChance => 0.1f;
        public TState GhostState { get { return State; } }
        [SerializeField]
        protected TState State;
        [SerializeField]
        protected Transform GhostHitbox;
        [SerializeField]
        protected Transform GhostObject;
        protected abstract void HandleStateChange(TState newState);
        protected abstract void OnMovementOpportunity();
        public virtual void RunExternalEvent(string eventName) { }
        [ProButton]
        public virtual void SwitchState(TState state)
        {
            if (State.Equals(state)) return;

            HandleStateChange(state);

            State = state;
        }
        [ProButton]
        public virtual void Jumpscare() { }

        protected virtual void Start()
        {
            GameController.Instance.OnMovementOpportunity += OnMovementOpportunity;
        }
        protected virtual void OnDestroy()
        {
            if (GameController.Instance != null)
                GameController.Instance.OnMovementOpportunity -= OnMovementOpportunity;
        }
    }
}
