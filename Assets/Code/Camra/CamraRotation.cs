//CamraRotation old

using UnityEngine;
using System.Collections;

public class CamraRotation : MonoBehaviour
{
    // Rotation variables
    private float minimumX = -20F;
    private float maximumX = 20F;
    private float minimumY = 00F;
    private float maximumY = 30F;
    private float xAxis = 0;
    private float yAxis = 0;
    private float xAxisTemp = 0;
    private float yAxisTemp = 0;
    private Vector2 current = new Vector2(0, 0);
    private Vector2 firstTouch = new Vector2(0, 0);
    private Vector2 secondTouch = new Vector2(0, 0);

    // Zoom variables
    Camera camra;
    private float[] minMaxZoom = new float[2] { 30F, 90F };
    public float perspectiveZoomSpeed = 0.2f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.2f;        // The rate of change of the orthographic size in orthographic mode.


    void Start()
    {
        xAxis = transform.rotation.x;
        yAxis = transform.rotation.y;
        camra = GetComponent<Camera>();
    }

    void Update()
    {
        // Check touch count
        // Rotation
        if (Input.touchCount == 1)
        {
            // Touch started, store position
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstTouch = Input.GetTouch(0).position;
                xAxisTemp = xAxis;
                yAxisTemp = yAxis;
            }
            // Finger moved
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                secondTouch = Input.GetTouch(0).position;
                // Set new rotation
                xAxis = xAxisTemp + (secondTouch.x - firstTouch.x) * -180.0f / Screen.width;
                yAxis = yAxisTemp + (secondTouch.y - firstTouch.y) * 90.0f / Screen.height;
                xAxis = Mathf.Clamp(xAxis, minimumX, maximumX);
                yAxis = Mathf.Clamp(yAxis, minimumY, maximumY);
                transform.rotation = Quaternion.Euler(yAxis, xAxis, 0.0f);
            }
        }
        // If there are two touches on the device...
        // Zoom
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camra.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camra.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camra.orthographicSize = Mathf.Clamp(camra.orthographicSize, 30F, 90F);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camra.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 30 and 90.
                camra.fieldOfView = Mathf.Clamp(camra.fieldOfView, 30F, 90F);
            }
        }
    }
}