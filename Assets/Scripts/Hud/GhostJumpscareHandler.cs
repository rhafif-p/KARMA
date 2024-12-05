using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Hud
{
    public class GhostJumpscareHandler : MonoBehaviour
    {
        [SerializeField]
        private Image BlackBackground;
        [SerializeField]
        private Image GhostFace;
        [SerializeField]
        private Image RedVignette;
        [SerializeField]
        private AudioSource JumpscareSound;

        public bool AnimationFinished => Time.time - StartTime >= Duration;
        private float AnimationProgress => (Time.time - StartTime) / Duration;
        private bool AnimationStarted = false;
        private float Duration = 2f;
        private float StartTime = 0f;

        private Vector3 GhostFaceOriginalScale;
        private Vector3 RedVignetteOriginalScale;

        private float EaseOutExpo(float x) {
            x = Mathf.Clamp01(x);
            return x == 1 ? 1f : 1f - (float)Math.Pow(2, -10 * x);
        }

        public void StartAnimation()
        {
            AnimationStarted = true;
            StartTime = Time.time;
            BlackBackground.enabled = true;
            RedVignette.enabled = true;
            GhostFace.enabled = true;

            JumpscareSound.Play();
        }

        public void FinishAnimation()
        {
            AnimationStarted = false;
            BlackBackground.enabled = false;
            RedVignette.enabled = false;
            GhostFace.enabled = false;
        }

        private void Start()
        {
            GhostFaceOriginalScale = GhostFace.rectTransform.localScale;
            RedVignetteOriginalScale = RedVignette.rectTransform.localScale;
        }

        public void Update()
        {
            if (AnimationFinished) FinishAnimation();
            if (!AnimationStarted) return;

            GhostFace.color = Color.white.WithAlpha(1f - EaseOutExpo(AnimationProgress));
            RedVignette.color = Color.white.WithAlpha(1f - EaseOutExpo(AnimationProgress));

            GhostFace.rectTransform.localScale = GhostFaceOriginalScale * (1f - EaseOutExpo(AnimationProgress) * 0.3f);
            RedVignette.rectTransform.localScale = RedVignetteOriginalScale * (1f + EaseOutExpo(AnimationProgress) * 1000f);
        }
    }
}
