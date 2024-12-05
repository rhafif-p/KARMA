using Assets.Scripts;
using Assets.Scripts.LightSources;
using UnityEngine;

public class Lamp : LightSource
{
    public override float InteractibleDistance => 0f;
    protected virtual float GeneratorDepletionRate => 0.02f;

    protected virtual void Update()
    {
        if (Active) GameController.Instance.Generator.Deplete(GeneratorDepletionRate * Time.deltaTime);
    }
}