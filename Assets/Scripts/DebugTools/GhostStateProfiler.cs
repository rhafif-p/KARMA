using Assets.Scripts.GhostControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DebugTools
{
    public class GhostStateProfiler : MonoBehaviour
    {
        public BathroomGhostState BathroomGhostState;
        public DoorGhostState DoorGhostState;
        public WindowGhostState WindowGhostState;

        [SerializeField]
        private BathroomGhostController BathroomGhostController;
        [SerializeField]
        private DoorGhostController DoorGhostController;
        [SerializeField]
        private WindowGhostController WindowGhostController;

        private void Update()
        {
            BathroomGhostState = BathroomGhostController.GhostState;
            DoorGhostState = DoorGhostController.GhostState;
            WindowGhostState = WindowGhostController.GhostState;
        }
    }
}
