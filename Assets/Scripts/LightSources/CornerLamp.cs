using UnityEngine;

public class CornerLamp : Lamp
{
    public override string HoverMessage => Active ? "Disable Corner Lamp" : "Enable Corner Lamp";
    public override void SetState(bool state)
    {
        base.SetState(state);
        if (state) GameController.Instance.WindowGhostController.RunExternalEvent("CornerLampEnabled");
    }

    void Start()
    {
        SetState(false);
    }
}
