using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Vector2 movementDir;

    CharacterController controller = null;
    GravityController gravityCon = null;
    JumpController jumpCon = null;
    MouseLook mouselook = null;


    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float crouchSpeed = 3f;


    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    float currentSpeed;

    float isSprinting;
    float isCrouching;
    [SerializeField] bool canCrouch;
    bool canUncrouch;

    public float terminalVelocity;

    [SerializeField] bool useGravity = false;
    [SerializeField] bool useJump = false;



    Vector3 finalMove = Vector3.zero;
    [HideInInspector] public Vector3 moveWithGrav = Vector3.zero;
    [HideInInspector] public Vector3 moveWithJump = Vector3.zero;

    [SerializeField] LayerMask discludePlayer;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mouselook = GetComponent<MouseLook>();
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

        CrouchUpdater();
        UncrouchUpdater();
        SpeedUpdater(isSprinting, isCrouching);
    }


    void SpeedUpdater(float sprinting, float crouching)
    {
        if (crouching == 1 && canCrouch)
        {
            currentSpeed = crouchSpeed;
        }
        
        else if (sprinting == 1)
        {
            currentSpeed = runSpeed;
        }

        else
        {
            currentSpeed = walkSpeed;
        }
    }


    void CrouchUpdater()
    {
        if (finalMove.y >= -4 && finalMove.y <= -2.4 && isCrouching == 1 && !jumpCon.isJumping)
        {
            canCrouch = true;
            controller.height = 1.4f;
            mouselook.headJoint.transform.localPosition = new Vector3(0,1.4f,0);
        }

        else if(canUncrouch)
        {
            canCrouch = false;
            controller.height = 1.8f;
            mouselook.headJoint.transform.localPosition = new Vector3(0, 1.8f, 0);
        }
    }

    void UncrouchUpdater()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 2f, discludePlayer))
        {
            Debug.Log(hit.transform.gameObject.name);
            canUncrouch = false;
        }

        else
        {
            canUncrouch = true;
        }

    }

    public void RecieveInput(Vector2 movementDirection, float sprintState, float crouchState)
    {
        movementDir = movementDirection;
        isSprinting = sprintState;
        isCrouching = crouchState;
    }
}

