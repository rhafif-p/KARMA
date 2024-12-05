using Assets.Scripts.Object.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Hud
{
    public class InventoryDisplayController : MonoBehaviour
    {
        private InventoryDisplay[] Displays;

        private void OnPlayerInventoryDataChanged(int index, ItemStack itemStack)
        {
            if (itemStack == null) { Displays[index].RemoveItem(); return; }

            Displays[index].SetItem(itemStack.Item);
            Displays[index].SetAmount(itemStack.Amount);
        }

        private void Initialize()
        {
            int size = GameController.Instance.PlayerInventory.InventorySize;
            Displays = new InventoryDisplay[size];
            for (int i = 0; i < size; i++)
            {
                GameObject displayObject = new($"Inventory{i}");
                displayObject.transform.parent = transform;
                displayObject.AddComponent<RectTransform>().sizeDelta = new Vector2(50, 50);

                Displays[i] = displayObject.AddComponent<InventoryDisplay>();
                Displays[i].Initialize();
            }
        }

        private void Start()
        {
            Initialize();
            GameController.Instance.PlayerInventory.InventoryDataChanged += OnPlayerInventoryDataChanged;
        }
    }
}
