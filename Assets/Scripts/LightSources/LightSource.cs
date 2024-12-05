using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LightSources
{
    public class LightSource : InteractiveObject
    {
        public bool Active = false;
        public GameObject Light;

        public virtual void SetState(bool state)
        {
            Active = state;
            Light.SetActive(state);
        }

        public override void OnInteract()
        {
            SetState(!Active);
        }
    }
}
