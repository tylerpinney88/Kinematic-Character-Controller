using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityX = 12f;
    public float sensitivityY = 8f;
    float mouseX, mouseY;

    [SerializeField] GameObject headJoint;
    [SerializeField] float minClamp = -65f;
    [SerializeField] float maxClamp = 65f;

    float rotationX;
    float rotationY;

    public bool mouseLocked;

    private void Update()
    {       
        if (mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            rotationY += mouseX * sensitivityY * 100 * Time.deltaTime;
            rotationX += mouseY * sensitivityX * 100 * Time.deltaTime;

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
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
        mouseLocked = mouseLock;
    }


}
