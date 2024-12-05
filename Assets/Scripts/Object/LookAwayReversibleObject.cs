using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Object
{
    public class LookAwayReversibleObject : MonoBehaviour
    {
        [SerializeField]
        protected Renderer Renderer;
        [SerializeField]
        protected Transform Transform;
        private Vector3 OriginalPosition;
        private Quaternion OriginalRotation;
        private Vector3 OriginalScale;

        protected bool IsObjectVisible()
        {
            if (Renderer == null) return false;
            return Renderer.isVisible;
        }

        protected void ReverseObjectTransform()
        {
            Transform.SetPositionAndRotation(OriginalPosition, OriginalRotation);
            Transform.localScale = OriginalScale;
        }

        protected virtual void Start()
        {
            if (Transform == null) Transform = GetComponent<Transform>();
            if (Renderer == null) Renderer = GetComponent<Renderer>();

            OriginalPosition = transform.position;
            OriginalRotation = transform.rotation;
            OriginalScale = transform.localScale;
        }

        protected virtual void Update()
        {
            if (!IsObjectVisible()) ReverseObjectTransform();
        }
    }
}
