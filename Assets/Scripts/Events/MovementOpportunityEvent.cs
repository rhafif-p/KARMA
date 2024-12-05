using UnityEngine;
using UnityEngine.Events;

public class MovementOpportunityEvent : UnityEvent
{
    public static MovementOpportunityEvent Trigger()
    {
        MovementOpportunityEvent eventToRaise = new();
        eventToRaise.Invoke();
        return eventToRaise;
    }
}