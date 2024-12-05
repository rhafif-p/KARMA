using Assets.Scripts.Object.Items;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Collectible
{
    public class Matches : InteractiveObject
    {
        public override string HoverMessage => "Pick Up Matches";
        public override float InteractibleDistance => 3f;
        public override void OnInteract()
        {
            PlayerInventory playerInventory = GameController.Instance.PlayerInventory;
            if (playerInventory == null) return;

            playerInventory.TryAddItem(Items.Match, 10);
        }
    }
}
