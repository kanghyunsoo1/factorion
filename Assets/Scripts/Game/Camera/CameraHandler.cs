using UnityEngine;
using System.Collections;

public class CameraHandler :MonoBehaviour {
    private static readonly float ZoomSpeedTouch = 0.1f;
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId;

    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

    void Awake() {
        cam = GetComponent<Camera>();
    }

    void Update() {
        HandleTouch();
    }

    void HandleTouch() {
        switch (Input.touchCount) {

            case 1: // Panning
                wasZoomingLastFrame = false;
                Touch touch = Input.GetTouch(0);

                RaycastHit hit;
                Physics.Raycast(cam.ScreenPointToRay(touch.position), out hit);

                if (touch.phase == TouchPhase.Began) {
                    lastPanPosition = hit.point;
                    panFingerId = touch.fingerId;
                } else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
                    PanCamera(hit.point);
                }
                break;

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame) {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                } else {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void PanCamera(Vector3 newPanPosition) {
        // Determine how much to move the camera
        Vector3 offset = lastPanPosition - newPanPosition;
        print(offset);
        print(lastPanPosition);
        print(newPanPosition);
        print(" ");
        Vector3 move = new Vector3(offset.x, 0, offset.z);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}