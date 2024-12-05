using Assets.Scripts.Object.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Hud
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField]
        private Item Item;
        [SerializeField]
        private int Amount;
        private Image IconComponent;
        private TextMeshProUGUI TextComponent;
        private Image BackgroundComponent;

        public void Initialize()
        {
            BackgroundComponent = this.transform.AddComponent<Image>();
            BackgroundComponent.color = new Color(0.15f, 0.15f, 0.15f, 0.8f);

            GameObject imageObject = new("Icon");
            imageObject.transform.SetParent(this.transform, false);
            
            RectTransform imageRect = imageObject.AddComponent<RectTransform>();
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.offsetMin = Vector2.zero;
            imageRect.offsetMax = Vector2.zero;

            IconComponent = imageObject.AddComponent<Image>();
            IconComponent.enabled = false;

            GameObject textObject = new("Amount");
            textObject.transform.SetParent(this.transform, false);

            RectTransform textRect = textObject.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            TextComponent = textObject.AddComponent<TextMeshProUGUI>();
            TextComponent.fontSize = 16;
            TextComponent.alignment = TextAlignmentOptions.BottomRight;
            TextComponent.enabled = false;
        }

        public void SetItem(Item item)
        {
            Item = item;
            if (!string.IsNullOrEmpty(Item?.IconPath))
            {
                Sprite iconSprite = Resources.Load<Sprite>(Item.IconPath);

                if (iconSprite != null)
                {
                    IconComponent.enabled = true;
                    IconComponent.sprite = iconSprite;
                }
            }
        }

        public void RemoveItem()
        {
            Item = null;
            IconComponent.enabled = false;
            TextComponent.enabled = false;
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
            TextComponent.text = amount.ToString();
            TextComponent.enabled = amount != 0;
        }
    }
}
