using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Object.Items
{
    [Serializable]
    public class ItemStack
    {
        public Item Item;

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                int prev = value;
                
                _amount = value;
                _amount = Math.Clamp(_amount, 0, Item.MaximumAmount);

                if (_amount > Item.MaximumAmount) _amount = Item.MaximumAmount;                
                if (prev != _amount) AmountChanged.Invoke(prev, _amount);
            }
        }

        public delegate void AmountChangedArgs(int prev, int current);
        public event AmountChangedArgs AmountChanged;

        public ItemStack(Item item)
        {
            Item = item;
            Amount = 1;
        }
        public ItemStack(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}
