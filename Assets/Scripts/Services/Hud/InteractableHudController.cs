using Assets.Scripts.Services.Hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class InteractableHudController : MonoBehaviour
    {
        [SerializeField]
        private int ObjectAmount;

        public InteractiveObject TargetedInteractiveObject;
        public InteractableHudKey InteractableHudKey;
        public InteractableHudText InteractableHudText;

        public void Start()
        {
            InteractableHudKey.SetActive(false);
            GameController.Instance.OnRaycastDoneEvaluating += OnRaycastDoneEvaluating;
        }

        private void OnRaycastDoneEvaluating()
        {
            GetTargetedInteractiveObject();
            TryDisplayHoverName();
        }

        private void GetTargetedInteractiveObject()
        {
            List<InteractiveObject> interactiveObjects = GameController.Instance.PlayerRaycastHits
                .Select(hit => hit.collider.GetComponent<InteractiveObjectHitbox>())
                .Where(hitbox => hitbox != null)
                .Select(hitbox => hitbox.ParentObject)
                .Where(parent => parent.IsPlayerWithinInteractibleDistance())
                .ToList();
            ObjectAmount = interactiveObjects.Count;
            TargetedInteractiveObject = interactiveObjects.FirstOrDefault();
        }

        private void TryDisplayHoverName()
        {
            if (TargetedInteractiveObject == null)
            {
                InteractableHudKey.SetActive(false);
                InteractableHudText.SetActive(false);
                return;
            }

            InteractableHudKey.SetActive(true);
            InteractableHudText.SetActive(true);

            InteractableHudText.SetText(TargetedInteractiveObject.HoverMessage);
        }

        protected virtual void OnDestroy()
        {
            if (GameController.Instance != null)
                GameController.Instance.OnRaycastDoneEvaluating -= OnRaycastDoneEvaluating;
        }
    }
}
