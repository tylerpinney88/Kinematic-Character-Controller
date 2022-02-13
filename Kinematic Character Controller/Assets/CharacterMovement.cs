using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementDir;

    CharacterController controller = null;
    GravityController gravityCon = null;
    JumpController jumpCon = null;

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;


    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    float currentSpeed;

    float isSprinting;

    public float terminalVelocity;

    float gravityDown;

    bool isJumping = false;


    [SerializeField] bool useGravity = false;
    [SerializeField] bool useJump = false;



    Vector3 finalMove = Vector3.zero;
    [HideInInspector] public Vector3 moveWithGrav = Vector3.zero;
    [HideInInspector] public Vector3 moveWithJump = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = walkSpeed;

        if (useGravity)
        {
            gravityCon = GetComponent<GravityController>();
        }

        if (useJump)
        {
            jumpCon = GetComponent<JumpController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUpdater(isSprinting);
        
        Vector2 targetDir = new Vector2(movementDir.x, movementDir.y);
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 finalMoveDir = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed;

        finalMove = finalMoveDir;

        if (useGravity)
        {
            gravityCon.ApplyGravity();
            finalMove += moveWithGrav;
        }

        if (useJump)
        {
            jumpCon.ApplyJump();
            finalMove += moveWithJump;
        }


        controller.Move(finalMove * Time.deltaTime);
    }


    void SpeedUpdater(float sprinting)
    {
        if (sprinting == 1)
        {
            currentSpeed = runSpeed;
        }

        else
        {
            currentSpeed = walkSpeed;
        }
    }


    public void RecieveInput(Vector2 movementDirection, float sprintState)
    {
        movementDir = movementDirection;
        isSprinting = sprintState;
    }
}

