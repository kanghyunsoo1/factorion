using UnityEngine;
using System.Collections;

public class CameraHandler :MonoBehaviour {

    private static readonly float panSpeed = 4f;
    private static readonly float zoomSpeedTouch = 0.05f;

    private static readonly float[] bounds = new float[] { -StaticDatas.SIZE, StaticDatas.SIZE };
    private static readonly float[] zoomBounds = new float[] { 7f, 20f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    void Awake() {
        cam = GetComponent<Camera>();
    }

    void Update() {
        HandleTouch();
        var v = cam.transform.position;
        var c = CameraScreenToUnit();
        v.x = Mathf.Clamp(v.x, bounds[0] + c.x / 2, bounds[1] - c.x / 2);
        v.y = Mathf.Clamp(v.y, bounds[0] + c.y / 2, bounds[1] - c.y / 2);
        cam.transform.position = v;
    }

    void HandleTouch() {
        switch (Input.touchCount) {

            case 1: // Panning
                wasZoomingLastFrame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                } else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
                    PanCamera(touch.position);
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

                    ZoomCamera(offset, zoomSpeedTouch);

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
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * (panSpeed + cam.orthographicSize), offset.y * (panSpeed + cam.orthographicSize));

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.z = -10f;
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), zoomBounds[0], zoomBounds[1]);
    }
    public Vector2 CameraScreenToUnit() {
        return new Vector2(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2);
    }

}