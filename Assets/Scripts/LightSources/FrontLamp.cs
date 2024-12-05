using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FrontLamp : Lamp
{
    public override string HoverMessage => Active ? "Disable Front Lamp" : "Enable Front Lamp";
    public override void SetState(bool state)
    {
        base.SetState(state);
        GameController.Instance.DoorGhostController.RunExternalEvent("FrontLampEnabled");
    }
    void Start()
    {
        SetState(false);
    }
}
