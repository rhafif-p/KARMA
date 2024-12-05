using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class RaycastHitDistanceComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance);
    }
}
