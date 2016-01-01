/*
        FUCKING WORKS!!!!!!!!!!!!!!!!!!!!!! NO TOUCH!
*/

using UnityEngine;


public class CamraMovement : MonoBehaviour
{
    private float[] minMaxX = new float[2] { -3F, 3F };
    private float[] minMaxY = new float[2] { -15F, 15F };
    private float[] minMaxZ = new float[2] { -10F, 10F };

    Camera camra;
    private float[] minMaxZoom = new float[2] { 30F, 90F };
    public float perspectiveZoomSpeed = 0.2f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.2f;        // The rate of change of the orthographic size in orthographic mode.

    void Start()
    {
        camra = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            
            Vector2 newTouchPosition = touchZero.position;
            Vector3 tempPos = new Vector3(0, 0, 0);
            //finds the position change and adds it to the current position
            transform.localPosition += transform.TransformDirection((Vector3)((touchZeroPrevPos - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f));
            tempPos = transform.localPosition;
            //limits the x,y,z range that the camera has
            tempPos.x = Mathf.Clamp(tempPos.x, minMaxX[0], minMaxX[1]);   //new
            tempPos.z = Mathf.Clamp(tempPos.z, minMaxZ[0], minMaxZ[1]);   //new
            tempPos.y = Mathf.Clamp(transform.position.y, 0, 0);   //new
            //changes the camera to the new position
            transform.localPosition = tempPos;                                 //new
        }
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
