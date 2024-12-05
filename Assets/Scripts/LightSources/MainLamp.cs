using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MainLamp : Lamp
{
    public override string HoverMessage => Active ? "Disable Main Lamp" : "Enable Main Lamp";
    void Start()
    {
        SetState(false);
    }
}
