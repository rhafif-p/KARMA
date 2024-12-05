using System.Linq;
using UnityEngine;

public class RaycastUtilities
{
    private static RaycastHit cachedPlayerRaycastHit;
    private static int cachedFrame = -1;

    private static void RunPlayerRaycast()
    {
        Camera camera = GameController.Instance.PlayerCamera;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Physics.Raycast(ray, out cachedPlayerRaycastHit);

        cachedFrame = Time.frameCount;
    }

    public static bool IsPlayerLookingAtObject(Transform obj)
    {
        return GameController.Instance.PlayerRaycastHits.Select(hit => hit.transform).Any(transform => obj == transform);
        //if (cachedFrame != Time.frameCount) RunPlayerRaycast();
        //return cachedPlayerRaycastHit.transform == obj;
    }
    public static bool IsPlayerFlashingAtObject(Transform obj)
    {
        return GameController.Instance.Flashlight.Active && IsPlayerLookingAtObject(obj);
        //if (!GameController.Instance.Flashlight.Active) return false;
        //if (cachedFrame != Time.frameCount) RunPlayerRaycast();
        //return cachedPlayerRaycastHit.transform == obj;
    }
}