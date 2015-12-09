/*
        FUCKING WORKS!!!!!!!!!!!!!!!!!!!!!! NO TOUCH!
*/

using UnityEngine;


public class CamraMovement : MonoBehaviour
{
    Vector2?[] oldTouchPositions = {
        null,
        null
    };
    Vector2 oldTouchVector;
    float oldTouchDistance;
    private float[] minMaxX = new float[2] { -3F, 3F };
    private float[] minMaxY = new float[2] { -15F, 15F };
    private float[] minMaxZ = new float[2] { -10F, 10F };
    private Vector3 tempPos = new Vector3(0, 0, 0);

    void Update()
    {
        if (Input.touchCount == 0)
        {
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
        }
        else
        {
            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;
                //finds the position change and adds it to the current position
                transform.localPosition += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f));
                tempPos = transform.localPosition;     
                //limits the x,y,z range that the camera has
                tempPos.x = Mathf.Clamp(tempPos.x, minMaxX[0], minMaxX[1]);   //new
                tempPos.z = Mathf.Clamp(tempPos.z, minMaxZ[0], minMaxZ[1]);   //new
                tempPos.y = Mathf.Clamp(transform.position.y, 0, 0);   //new
                //changes the camera to the new position
                transform.localPosition = tempPos;                                 //new
                oldTouchPositions[0] = newTouchPosition;
            }
        }
    }
}
