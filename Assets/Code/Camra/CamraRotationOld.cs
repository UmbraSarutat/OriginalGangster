//CamraRotation old

using UnityEngine;
using System.Collections;

public class CamraRotationOld : MonoBehaviour
{
    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;

    GameObject MainCamera;

    private float sensitivityX = 15F;
    private float sensitivityY = 15F;
    private float minimumX = -45F;
    private float maximumX = 30F;
    private float minimumY = -30F;
    private float maximumY = 30F;
    float rotationY = 0F;
    float rotationX = 0F;
    
    private float xAxis = 0;
    private float yAxis = 0;

    private Vector2 current = new Vector2(0, 0);

    void Update()
    {
        #region if Unity

   //  if (axes == RotationAxes.MouseXAndY)
   //  {
   //      rotationX += Input.GetAxis("Mouse X") * sensitivityX * -1;
   //      rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
   //      rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
   //      rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
   //      XAndYRotation(rotationX, rotationY);
   //  }
   //  else if (axes == RotationAxes.MouseX)
   //  {
   //      rotationX += Input.GetAxis("Mouse X") * sensitivityX * -1;
   //      rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
   //      XRotation(rotationX);
   //  }
   //  else
   //  {
   //      rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
   //      rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
   //      YRotation(rotationY);
   //  }

        #endregion
        #region if Touch

      // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
      // {
      //     Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
      //     xAxis = touchDeltaPosition.x * -1;
      //     xAxis = Mathf.Clamp(xAxis, minimumX, maximumX);
      //     yAxis = touchDeltaPosition.y;
      //     yAxis = Mathf.Clamp(yAxis, minimumY, maximumY);
      //
      //     XAndYRotation(xAxis, yAxis);
      // }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Debug.Log(touchDeltaPosition);
            if(touchDeltaPosition.x < 0)
            {
                xAxis += 1f;
            }
            if (touchDeltaPosition.x > 0)
            {
                xAxis -= 1f;
            }
            if (touchDeltaPosition.y < 0)
            {
                yAxis += 1f;
            }
            if (touchDeltaPosition.y > 0)
            {
                yAxis -= 1f;
            }
            
           // xAxis = Mathf.Clamp(xAxis, minimumX, maximumX);
           // yAxis = Mathf.Clamp(yAxis, minimumY, maximumY);
            XAndYRotation(xAxis, yAxis);
            current = transform.localEulerAngles;
        }
        #endregion
    }

    void Start()
    {
        //MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //MainCamera.transform.Rotate(0, 0, 65);
    }

    #region if Unity

    public void XAndYRotation(float rotateX, float rotateY)
    {
        transform.localEulerAngles = new Vector3(rotateY, rotateX, 0);
        //transform.localEulerAngles = new Vector3(rotateX, rotateY, 0);
    }

    public void XRotation(float rotateX)
    {
        transform.localEulerAngles = new Vector3(rotateX, transform.localEulerAngles.x, 0);
    }

    public void YRotation(float rotateY)
    {
        transform.localEulerAngles = new Vector3(rotateY, transform.localEulerAngles.y, 0);
    }
#endregion
}