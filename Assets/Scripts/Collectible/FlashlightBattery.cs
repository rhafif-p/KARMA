using Assets.Scripts.Object.Items;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Collectible
{
    public class FlashlightBattery : InteractiveObject
    {
        public override string HoverMessage => "Pick Up Battery";
        public override float InteractibleDistance => 2f;
        public override void OnInteract()
        {
            PlayerInventory playerInventory = GameController.Instance.PlayerInventory;
            if (playerInventory == null) return;

            playerInventory.TryAddItem(Items.FlashlightBattery, 1);
        }
    }
}
