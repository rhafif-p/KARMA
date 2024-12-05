using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public class AreaAudioSource : MonoBehaviour
    {
        [SerializeField]
        private Transform AudioArea;
        private AudioSource AudioSource;

        private void Start()
        {
            if (AudioSource == null) AudioSource = GetComponent<AudioSource>();
        }
                        
        private void Update()
        {
            if (AudioArea == null || AudioSource == null) return;
            AudioSource.spatialBlend = PositionUtilities.IsPlayerWithin(AudioArea) ? 0 : 1;
        }
    }
}
