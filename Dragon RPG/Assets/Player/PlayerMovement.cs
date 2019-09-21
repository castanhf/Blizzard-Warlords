using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f; // 20 cm
    [SerializeField] float attackMoveStopRadius = 5f; // 5 m

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectMode = false;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // TODO: fix click movement and WASD conflict, which increases speed

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // G for gamepad. TODO: allow player to change later
        if (Input.GetKeyDown(KeyCode.G)) {
            isInDirectMode = !isInDirectMode; // toggle mode
            currentDestination = transform.position; // clear the click target
        }

        if(isInDirectMode) {
            ProcessDirectMovement();
        }
        else {
            ProcessMouseMovement();
        }

    }

    private void ProcessDirectMovement() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0)) {                  // Get mouse input
            clickPoint = cameraRaycaster.hit.point;     // Get click point
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);  // Short destination
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);  // Short destination
                    break;
                default:
                    print("DEFAULT - not good");
                    return;
            }
        }
        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    // Get close to target but not inside target
    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }
    void OnDrawGizmos() {
        // Draw movement gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.05f);
        Gizmos.DrawSphere(clickPoint, 0.1f);

        // Ctrl + K, C - Comment
        // Ctrl + K, U - Uncomment

        // Draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

