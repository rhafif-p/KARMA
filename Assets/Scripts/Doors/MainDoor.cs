using Assets.Scripts.Utilities;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public bool Open = false;
    private Transform DoorObject;

    private Quaternion closedRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion openRotation = Quaternion.Euler(0, 40, 0);

    public void SetState(bool state)
    {
        Open = state;
    }

    void Start()
    {
        if (DoorObject == null) DoorObject = transform.Find("MainDoor_Object");
    }

    void Update()
    {
        if (Input.GetButtonDown("E"))
        {
            if (DistanceUtilities.PlayerFlatDistanceFrom(DoorObject) <= 2f && RaycastUtilities.IsPlayerLookingAtObject(DoorObject)) SetState(!Open);
        }

        if (Open) transform.rotation = Quaternion.RotateTowards(transform.rotation, openRotation, Time.deltaTime * 90);
        else transform.rotation = Quaternion.RotateTowards(transform.rotation, closedRotation, Time.deltaTime * 90);
    }
}
