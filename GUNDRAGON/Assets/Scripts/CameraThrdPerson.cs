using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThrdPerson : MonoBehaviour
{
    public float rotateSensitivity;
    public Transform target;
    public float dstFromTarget = 4;
    public Vector2 verticalMinMax = new Vector2(-8, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float verticalRotate;
    float horizontalRotate;

	
	void LateUpdate ()
    {
        if (Player.didPause == false)
        {
            verticalRotate -= Input.GetAxis("Mouse Y") * rotateSensitivity;
            verticalRotate = Mathf.Clamp(verticalRotate, verticalMinMax.x, verticalMinMax.y);
            horizontalRotate += Input.GetAxis("Mouse X") * rotateSensitivity;

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(verticalRotate, horizontalRotate), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * dstFromTarget;
        }
        
	}
}
