/*
THIS HAS BEEN INCOPERATED WITH THE ROTATION AND MOVEMENT SCRIPTS TO 
FIX A BUG WHEN RELEASING ZOOM TO ONE FINGER
*/

using UnityEngine;
using System.Collections;

public class CamraZoom : MonoBehaviour
{
    Camera camra;
    private float[] minMaxZoom = new float[2] { 30F, 90F };


    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


    void Start()
    {
        camra = GetComponent<Camera>();
    }

	// Update is called once per frame
	void Update ()
    {
        // If there are two touches on the device...
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
                camra.orthographicSize = Mathf.Max(camra.orthographicSize, 0.1f);
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
