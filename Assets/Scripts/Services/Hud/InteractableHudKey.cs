using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services.Hud
{
    public class InteractableHudKey : MonoBehaviour
    {
        public bool Active;
        public Transform TextObject;
        private TextMeshPro _TextMeshPro => TextObject.GetComponent<TextMeshPro>();
        public void SetActive(bool active)
        {
            Active = active;
            gameObject.SetActive(active);
        }
    }
}
