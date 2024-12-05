using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Object.Items
{
    public class Items
    {
        public static Item FlashlightBattery = new Item("Battery").WithMaximumAmount(1).WithIcon("Sprites/HUD/rusty battery");
        public static Item Match = new Item("Match").WithMaximumAmount(10).WithIcon("Sprites/HUD/matchbox");
    }
}
