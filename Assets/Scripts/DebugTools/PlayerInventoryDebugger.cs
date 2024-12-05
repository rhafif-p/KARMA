using Assets.Scripts.Object.Items;
using Assets.Scripts.Services;
using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DebugTools
{
    public class PlayerInventoryDebugger: MonoBehaviour
    {
        [ProButton]
        public void AddFlashlightBattery()
        {
            GameController.Instance.PlayerInventory.TryAddItem(Items.FlashlightBattery, 1);
        }
    }
}
