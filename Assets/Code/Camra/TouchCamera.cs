// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera : MonoBehaviour {
	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;
    Vector3 touchTranslation;
    Vector3 touchRotation;
    Vector3 touchEndRotation;
    Quaternion newRotation = new Quaternion();

    private Vector2 MinVals = new Vector2 ( 20F, -10F );
    private Vector2 MaxVals = new Vector2 ( 40F, 10F );


    void Update() {
		if (Input.touchCount == 0) {
			oldTouchPositions[0] = null;
			oldTouchPositions[1] = null;
		}
		else if (Input.touchCount == 1) {
			if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = null;
			}
			else {
				Vector2 newTouchPosition = Input.GetTouch(0).position;
                //store touch differences
                touchTranslation = (Vector4)(oldTouchPositions[0] - newTouchPosition);
                
                //adding translation to rotation
                touchEndRotation.x = touchRotation.x + touchTranslation.x;
                touchEndRotation.y = touchRotation.y + touchTranslation.y;
                
                //limiting rotation to certain degrees
                touchEndRotation.x = Mathf.Clamp(touchEndRotation.x, MinVals.x, MaxVals.x);
                touchEndRotation.y = Mathf.Clamp(touchEndRotation.y, MinVals.y, MaxVals.y);
                
                //moving stuff onscreen
                //newRotation = Quaternion.LookRotation((transform.position - touchEndRotation), Vector3.up);

                transform.rotation = Quaternion.Euler((transform.position - touchEndRotation));
                Debug.Log(transform.rotation);
                
                //saves old location
                oldTouchPositions[0] = newTouchPosition;
                
            }
        }
	}
}