using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementDir;

    [SerializeField] CharacterController controller = null;

    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = new Vector2(movementDir.x, movementDir.y);
        inputDir.Normalize();

        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * currentSpeed;

        controller.Move(velocity * Time.deltaTime);
    }

    public void RecieveInput(Vector2 movementDirection, float currentMoveSpeed)
    {
        movementDir = movementDirection;
        currentSpeed = currentMoveSpeed;
    }
}
