using Assets.Scripts.Object.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LightSources
{
    public class Candle : LightSource
    {
        public override string HoverMessage => GameController.Instance.PlayerInventory.HasItem(Items.Match) ? Active ? "" : "Light Candle" : "Need a match";
        public float ElapsedLifespan => (1f - Life) / LifeDepletionRate;
        [SerializeField]
        private float Life = 1f;
        [SerializeField]
        private float LifeDepletionRate = 0.05f;

        public override void SetState(bool state)
        {
            base.SetState(state);
        }

        public override void OnInteract()
        {
            if (Active) return;
            if (!GameController.Instance.PlayerInventory.HasItem(Items.Match)) return;

            if (GameController.Instance.PlayerInventory.TryRemoveItem(Items.Match, 1))
            {
                Life = 1f;
                SetState(true);
            }
        }

        void Start()
        {
            SetState(false);
        }

        private void Update()
        {
            if (Active) Life -= Time.deltaTime * LifeDepletionRate;
            Life = Math.Clamp(Life, 0f, 1f);

            if (Life <= 0f) SetState(false);
        }
    }
}
