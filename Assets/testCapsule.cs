using UnityEngine;

public class testCapsule : MonoBehaviour
{
    public Transform cameraTransform; 
    public float distance = 0.5f; 
    public float smoothSpeed = 5f; 
    private bool isFollowing = false; 
    public JumpscareHUD jumpscareHUD; 

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.O))
        {
            isFollowing = !isFollowing; 

            if (isFollowing && jumpscareHUD != null)
            {
                jumpscareHUD.TriggerJumpscare();
            }
        }

        if (isFollowing && cameraTransform != null)
        {
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distance;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, cameraTransform.rotation, smoothSpeed * Time.deltaTime);
        }
    }
}