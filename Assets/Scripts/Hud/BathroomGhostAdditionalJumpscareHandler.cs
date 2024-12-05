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
    public class BathroomGhostAdditionalJumpscareHandler : MonoBehaviour
    {
        [SerializeField]
        private Image Left;
        [SerializeField]
        private Image Right;

        public bool AnimationFinished => Time.time - StartTime >= Duration;
        private float AnimationProgress => (Time.time - StartTime) / Duration;
        private bool AnimationStarted = false;
        private float Duration = 2f;
        private float StartTime = 0f;

        private float EaseOutExpo(float x)
        {
            x = Mathf.Clamp01(x);
            return x == 1 ? 1f : 1f - (float)Math.Pow(2, -10 * x);
        }

        public void StartAnimation()
        {
            AnimationStarted = true;
            StartTime = Time.time;
            Left.enabled = true;
            Right.enabled = true;
        }

        public void FinishAnimation()
        {
            AnimationStarted = false;
            Left.enabled = false;
            Right.enabled = false;
        }

        public void Update()
        {
            if (AnimationFinished) FinishAnimation();
            if (!AnimationStarted) return;

            float initialScale = 0.75f;
            float grabScale = 1.25f;
            if (AnimationProgress < 0.75f)
            {
                Left.rectTransform.localScale = Vector3.one * initialScale;
                Right.rectTransform.localScale = Vector3.one * initialScale;

                Left.rectTransform.anchoredPosition = new Vector2(
                    (1f - EaseOutExpo(AnimationProgress * 4 / 3)) * -Left.rectTransform.rect.width * initialScale, 
                    Left.rectTransform.anchoredPosition.y);
                Right.rectTransform.anchoredPosition = new Vector2(
                    (1f - EaseOutExpo(AnimationProgress * 4 / 3)) * Right.rectTransform.rect.width * initialScale,
                    Right.rectTransform.anchoredPosition.y);
            }
            else
            {
                Left.rectTransform.localScale = Vector3.one * (EaseOutExpo(AnimationProgress * 4 - 3f) * grabScale + initialScale);
                Right.rectTransform.localScale = Vector3.one * (EaseOutExpo(AnimationProgress * 4 - 3f) * grabScale + initialScale);
            }
        }
    }
}
