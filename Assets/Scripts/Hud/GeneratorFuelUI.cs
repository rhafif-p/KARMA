using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Hud
{
    public class GeneratorFuelUI : NodeStyledProgressBar
    {
        protected override int NodesAmount => 10;
        protected override void Start()
        {
            base.Start();
            GameController.Instance.Generator.GeneratorFuelChanged += Generator_GeneratorFuelChanged; ;
        }

        private void Generator_GeneratorFuelChanged(float prev, float current)
        {
            SetProgress(current);
        }
    }
}
