using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Variables

    [Header("Speeds")]
    public float moveSpeed;
    public float axisSpeed;
    public float rotationSpeed;
    public float strafeSpeed;

    [Header("Physics")]
    public float gravity;
    public float jumpForce;

    [Header("External")]
    public Camera playerCamera;
    public LayerMask disculdePlayer;

    [Header("Surface Control")]
    public Vector3 sensorLocal;
    public float surfaceSlideSpeed;
    public float slopeClimbSpeed;
    public float slopeDecendSpeed;

    private bool grounded;
    private float currentGrav;

    PlayerInputs playerInput;
    PlayerInputs.MovementActions movement;
    Vector2 move;

    bool jumpPressed;

    #endregion

    private void Awake()
    {
        playerInput = new PlayerInputs();
        movement = playerInput.Movement;
    }


    private void Update()
    {
        Gravity();
        Jump();
        SimpleMove();
        FinalMovement();
    }

    private float jumpHeight;
    private Vector3 moveVector;
    private void SimpleMove()
    {
        move = movement.Move.ReadValue<Vector2>();
        moveVector = collisionSlopeCheck(new Vector3(move.x, 0, move.y)) * axisSpeed;
    }

    private void FinalMovement()
    {
        Vector3 movement = new Vector3(moveVector.x, -currentGrav + jumpHeight, moveVector.z) * moveSpeed;
        movement = transform.TransformDirection(movement);
        transform.position += movement;
    }

    private Vector3 collisionSlopeCheck(Vector3 dir)
    {
        Vector3 d = transform.TransformDirection(dir);
        Vector3 l = transform.TransformPoint(sensorLocal);

        Ray ray = new Ray(l, d);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, disculdePlayer))
        {
            if (hit.distance <= 0.7f)
            {
                Debug.DrawLine(transform.position, hit.point, Color.yellow, 0.2f);

                Vector3 temp = Vector3.Cross(hit.normal, d);
                Debug.DrawRay(hit.point, temp * 20, Color.green, 0.2f);

                Vector3 myDirection = Vector3.Cross(temp, hit.normal);
                Debug.DrawRay(hit.point, myDirection * 20, Color.red, 0.2f);

                Vector3 dir2 = myDirection * surfaceSlideSpeed * moveSpeed * axisSpeed;

                RaycastHit wCheck = walllCheckDetails(dir2);
                if (wCheck.transform != null)
                {
                    dir2 *= wCheck.distance * 0.5f;
                }
                transform.position += dir2;
                return Vector3.zero;

            }

            else
            {
                return dir;
            }
        }

        return dir;
    }

    private RaycastHit walllCheckDetails(Vector3 dir)
    {
        Vector3 l = transform.TransformPoint(sensorLocal);
        Ray ray = new Ray(l, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.2f, disculdePlayer))
        {
            return hit;
        }
        return hit;
    }

    private void Gravity()
    {
        Vector3 boxPos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2 - (Vector3.one / 40).y, transform.position.z);
        Vector3 boxSize = Vector3.one;
        grounded = Physics.CheckBox(boxPos, boxSize / 2);
        if (grounded)
        {
            currentGrav = 0;
        }
        else
        {
            currentGrav = -gravity;
        }

        if (grounded)
        {
            Ray ray = new Ray(transform.position, Vector3.down * 2);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, disculdePlayer))
            {
                if (hit.distance <= 2)
                {
                    Debug.DrawRay(ray.origin, ray.direction * 20, Color.green, 0.2f);
                    Vector3 needed = new Vector3(transform.position.x, hit.point.y + transform.localScale.y, transform.position.z);
                    transform.position = needed;
                }
                else if (hit.distance > 2)
                {
                    grounded = true;
                    currentGrav = -gravity * 0.5f;
                }
            }
        }
    }

    private void Jump()
    {
        if (grounded)
        {
            if (jumpHeight > 0)
            {
                jumpHeight = 0;
            }
            if (jumpPressed)
            {
                jumpPressed = false;

                jumpHeight += jumpForce;
            }
        }

        else
        {
            if (jumpHeight > 0)
            {
                jumpHeight -= (jumpHeight * Time.deltaTime);
            }
            else
            {
                jumpHeight = 0;
            }
        }
    }

    public void RecieveJump()
    {
        if (grounded)
        {
            jumpPressed = true;
        }
    }


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDrawGizmos()
    {
        Vector3 boxPos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2 - (Vector3.one / 40).y, transform.position.z);
        Vector3 boxSize = Vector3.one;
        if (!grounded)
        {
            Gizmos.color = Color.red;
        }

        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireCube(boxPos, boxSize);

        Gizmos.color = Color.yellow;
        Vector3 l = transform.TransformPoint(sensorLocal);
        Gizmos.DrawWireSphere(l, 0.2f);
    }

}
