using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class DistanceUtilities
    {
        public static float FlatDistanceSquared(Vector3 vector1, Vector3 vector2)
        {
            return Mathf.Pow(vector2.x - vector1.x, 2) + Mathf.Pow(vector2.z - vector1.z, 2);
        }
        public static float FlatDistance(Vector3 vector1, Vector3 vector2)
        {
            return Mathf.Sqrt(FlatDistanceSquared(vector1, vector2));
        }
        public static float PlayerFlatDistanceFrom(Transform obj)
        {
            return FlatDistance(obj.position, GameController.Instance.PlayerCamera.transform.position);
        }
    }
}
