using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class DirectionUtilities
    {
        public static Vector3 GetFlatDirectionToPlayer(Transform obj)
        {
            Vector3 player = GameController.Instance.PlayerCamera.transform.position;
            Vector3 direction = player - obj.transform.position;
            direction.y = 0;
            return Vector3.Normalize(direction);
        }
    }
}
