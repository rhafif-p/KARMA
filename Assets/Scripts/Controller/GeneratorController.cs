using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class GeneratorController : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float Fuel = 1f;
        public event Action<float, float> GeneratorFuelChanged;

        public void Deplete(float amount)
        {
            float prev = Fuel;

            Fuel -= amount * Time.deltaTime * GameController.Instance.GeneratorDepletionMultiplier;
            Fuel = Math.Clamp(Fuel, 0f, 1f);

            GeneratorFuelChanged?.Invoke(prev, Fuel);
        }
    }
}
