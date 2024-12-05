using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public class ExcludePlayerHitbox : MonoBehaviour
    {
        [SerializeField]
        private Collider Collider;
        [SerializeField]
        private Collider PlayerCollider;

        private void Start()
        {
            if (Collider == null) Collider = GetComponent<Collider>();
            if (PlayerCollider == null) PlayerCollider = GameController.Instance.PlayerCollider;
            if (Collider != null && PlayerCollider != null) Physics.IgnoreCollision(Collider, PlayerCollider);
        }
    }
}
