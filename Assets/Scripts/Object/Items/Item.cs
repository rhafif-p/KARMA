using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Object.Items
{
    [Serializable]
    public class Item
    {
        public string Name;
        public int MaximumAmount = 99;
        public string IconPath;

        public Item() { }
        public Item(string name) { Name = name; }
        public Item(string name, int maximumAmount) { Name = name; MaximumAmount = maximumAmount; }
        public Item(string name, int maximumAmount, string icon)
        {
            Name = name;
            MaximumAmount = maximumAmount;
            IconPath = icon;
        }

        public Item WithName(string name)
        {
            Name = name;
            return this;
        }
        public Item WithIcon(string iconPath)
        {
            IconPath = iconPath;
            return this;
        }
        public Item WithMaximumAmount(int maximumAmount)
        {
            MaximumAmount = maximumAmount;
            return this;
        }
    }
}
