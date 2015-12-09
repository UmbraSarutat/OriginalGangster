using UnityEngine;


public class CamraRotation : MonoBehaviour
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
    GameObject MainCamera;

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
        }
        else /*if (Input.touchCount == 1)*/
        {
            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;
                Vector4 tempVector4 = transform.position;
                Quaternion tempQuaternion = new Quaternion( 0f, 0f, 0f, 0f );
                tempQuaternion = MainCamera.transform.localRotation;

                tempQuaternion = Quaternion.LookRotation(tempVector4 + ((Vector4)transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f))));
                Debug.Log(tempQuaternion);
                //tempPos = transform.localPosition;                                 //new
                tempQuaternion.x = Mathf.Clamp(tempQuaternion.x, minMaxX[0], minMaxX[1]);   //new
                tempQuaternion.y = Mathf.Clamp(tempQuaternion.z, minMaxZ[0], minMaxZ[1]);   //new
                //tempPos.y = Mathf.Clamp(5, 5, 5);   //new
                //tempPos.z = Mathf.Clamp(tempPos.z, minMaxZ[0], minMaxZ[1]);   //new
                tempQuaternion.z = Mathf.Clamp(tempQuaternion.y, 0, 0);   //new
                tempQuaternion.w = 0;
                transform.rotation = tempQuaternion;                                 //new
                oldTouchPositions[0] = newTouchPosition;
            }
        }
    }
}
