using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Character Input Variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 35;
    [SerializeField] float jumpAmount = 20;
    [SerializeField]
    WordManager wordManager;

    [Header("Ground Things")]
    [SerializeField] Transform groundPoint;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField]
    LockedBool lockedBool;

    Rigidbody2D _rig;

    bool isGrounded;
    bool isInBox;

    bool givingPush;

    SpriteRenderer playerSprite;

    float xInput;
    float yInput;

    bool pushBrickUp;
    bool pushBrickDown;
    bool isLockingTheWord;

    private void Awake()
    {
        playerSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _rig = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       
    }

    private void Update()
    {

        _rig.velocity = new Vector2(xInput * moveSpeed, _rig.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if (xInput > 0)
        {

            playerSprite.flipX = false;

        }
        else if (xInput < 0)
        {

            playerSprite.flipX = true;

        }
    }



    public void Move(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<Vector2>().x;
       

      


    }





    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            _rig.velocity = new Vector3(_rig.velocity.x, jumpForce);
        }
    }

    public void PushUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("pushing up");
            pushBrickUp = true;
        }
        else
        {
            pushBrickUp = false;
        }
       
    }

    public void PushDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pushing down");
            pushBrickDown = true;
        }
        else
        {
            pushBrickDown = false;
        }
       
    }

    public void LockWord(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isLockingTheWord = true;
        }
        else
        {
            isLockingTheWord = false;
           
        }
    } 


    public bool IsPushingUpBrick()
    {
        return pushBrickUp;
    }

    public bool IsPushingBrickDown()
    {
        return pushBrickDown;
    }

    public bool IsLockingLetter()
    {
        return isLockingTheWord;
    }


}
