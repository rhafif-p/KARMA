using Assets.Scripts.Object.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField]
        public int InventorySize = 3;
        [SerializeField]
        private ItemStack[] Inventory;

        public delegate void InventoryDataChangedArgs(int index, ItemStack item);
        public event InventoryDataChangedArgs InventoryDataChanged;

        private void InitializeItemStack(int index, Item item)
        {
            Inventory[index] = new ItemStack(item);
            Inventory[index].AmountChanged +=
                (int prev, int current) => InventoryDataChanged?.Invoke(index, Inventory[index]);

            InventoryDataChanged?.Invoke(index, Inventory[index]);
        }

        public bool TryAddItem(Item item, int amount)
        {
            if (amount <= 0) return false;

            // Try to stack items first
            for (int i = 0; i < InventorySize; i++)
            {
                if (Inventory[i] != null && Inventory[i].Item == item)
                {
                    int maxAddable = Inventory[i].Item.MaximumAmount - Inventory[i].Amount;
                    if (maxAddable > 0)
                    {
                        int addAmount = Mathf.Min(amount, maxAddable);
                        Inventory[i].Amount += addAmount;
                        amount -= addAmount;

                        InventoryDataChanged?.Invoke(i, Inventory[i]);
                        if (amount <= 0) return true;
                    }
                }
            }

            // Add to a new slot if there's still remaining amount
            for (int i = 0; i < InventorySize; i++)
            {
                if (Inventory[i] == null)
                {
                    InitializeItemStack(i, item);
                    int addAmount = Mathf.Min(amount, item.MaximumAmount);
                    Inventory[i].Amount = addAmount;
                    amount -= addAmount;

                    InventoryDataChanged?.Invoke(i, Inventory[i]);
                    if (amount <= 0) return true;
                }
            }

            // If there are leftovers, return false
            return false;
        }
        public bool TryRemoveItem(Item item, int amount)
        {
            if (amount <= 0) return false;

            for (int i = 0; i < InventorySize; i++)
            {
                if (Inventory[i] != null && Inventory[i].Item == item)
                {
                    if (Inventory[i].Amount <= amount)
                    {
                        amount -= Inventory[i].Amount;
                        Inventory[i] = null;
                        InventoryDataChanged?.Invoke(i, null);
                    }
                    else
                    {
                        Inventory[i].Amount -= amount;
                        amount = 0;
                        InventoryDataChanged?.Invoke(i, Inventory[i]);
                    }

                    if (amount <= 0) return true;
                }
            }

            return false; // Not enough items to remove
        }
        public bool HasItem(Item item)
        {
            for (int i = 0; i < InventorySize; i++) 
                if (Inventory[i] != null && Inventory[i].Item == item) return true;
            return false;
        }
        private void Awake()
        {
            Inventory = new ItemStack[InventorySize];
        }
    }
}
