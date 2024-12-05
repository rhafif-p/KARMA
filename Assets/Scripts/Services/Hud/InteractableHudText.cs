using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services.Hud
{
    public class InteractableHudText : MonoBehaviour
    {
        public bool Active;
        private TMP_Text TextComponent;
        public void SetText(string text)
        {
            TextComponent.text = text;
        }
        public void SetActive(bool active)
        {
            Active = active;
            gameObject.SetActive(active);
        }
        public void Start() 
        {
            TextComponent = GetComponent<TMP_Text>();
        }
    }
}
