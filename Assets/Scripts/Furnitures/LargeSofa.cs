using Assets.Scripts.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Furnitures
{
    public class LargeSofa : LookAwayReversibleObject
    {
        private bool ShouldRotateSlightly = false;
        private Quaternion SlightRotation = Quaternion.Euler(0, -87, 0);
        public void RotateSlightly()
        {
            ShouldRotateSlightly = true;
        }
        protected override void Update()
        {
            base.Update();
            if (!IsObjectVisible()) ShouldRotateSlightly = false;
            else if (ShouldRotateSlightly) transform.rotation = Quaternion.RotateTowards(transform.rotation, SlightRotation, Time.deltaTime * 90);
        }
    }
}
