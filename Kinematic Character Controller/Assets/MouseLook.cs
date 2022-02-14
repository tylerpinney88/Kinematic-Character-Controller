using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityX = 12f;
    public float sensitivityY = 8f;
    Vector2 mouseDelta;

    public GameObject headJoint;
    [SerializeField] float minClamp = -65f;
    [SerializeField] float maxClamp = 65f;

    float rotationX;
    float rotationY;

    public bool mouseLocked;

    [SerializeField] [Range(0.0f, 0.05f)] float mouseSmoothTime = 0.03f;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private void Update()
    {       
        if (!mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            rotationY += currentMouseDelta.x * sensitivityY * 100 * Time.deltaTime;
            rotationX += currentMouseDelta.y * sensitivityX * 100 * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, minClamp, maxClamp);

            transform.localEulerAngles = new Vector3(0, rotationY, 0);
            headJoint.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RecieveInput(Vector2 mouseInput, bool mouseLock)
    {
        mouseDelta = mouseInput;
        mouseLocked = mouseLock;
    }


}
