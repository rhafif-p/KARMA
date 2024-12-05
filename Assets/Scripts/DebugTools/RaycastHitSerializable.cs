using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DebugTools
{
    [Serializable]
    public class RaycastHitSerializable
    {
        public GameObject GameObject;
        public RaycastHitSerializable(GameObject gameObject) {
            GameObject = gameObject;
        }

        public static RaycastHitSerializable From(RaycastHit hit)
        {
            if (hit.collider == null) return null;
            return new RaycastHitSerializable(hit.collider.gameObject);
        }
    }
}
