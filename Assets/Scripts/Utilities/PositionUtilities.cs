using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class PositionUtilities
    {
        public static bool IsPlayerWithin(Transform obj)
        {
            Vector3 localPos = obj.InverseTransformPoint(GameController.Instance.PlayerCamera.transform.position);
            return Mathf.Abs(localPos.x) < 0.5f && Mathf.Abs(localPos.y) < 0.5f && Mathf.Abs(localPos.z) < 0.5f;
        }
    }
}
