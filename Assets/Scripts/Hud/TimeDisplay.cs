using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Hud
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text TextComponent;

        private void Update()
        {
            int time = (int)Math.Floor(GameController.Instance.TimeProgress * 6);
            TextComponent.text = time == 0 ? "12 AM" : $"{time} AM";
        }
    }
}
