using UnityEngine;

public class BathroomLamp : Lamp
{
    public override string HoverMessage => Active ? "Disable Bathroom Lamp" : "Enable Bathroom Lamp";
    public override void SetState(bool state)
    {
        base.SetState(state);
        GameController.Instance.BathroomGhostController.RunExternalEvent("BathroomLampEnabled");
        GameController.Instance.DoorGhostController.RunExternalEvent("BathroomLampEnabled");
    }
    void Start()
    {
        SetState(false);
    }
}
