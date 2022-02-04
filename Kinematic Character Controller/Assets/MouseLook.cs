using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityX = 8f;
    public float sensitivityY = 0.5f;
    float mouseX, mouseY;

    [SerializeField] GameObject headJoint;
    [SerializeField] float minClamp = -85f;
    [SerializeField] float maxClamp = 85f;

    float rotationX;
    float rotationY;

    private void Update()
    {

        rotationY += mouseX * sensitivityY;
        rotationX += mouseY * sensitivityX;

        rotationX = Mathf.Clamp(rotationX, minClamp, maxClamp);

        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        headJoint.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);
    }

    public void RecieveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
    }


}
