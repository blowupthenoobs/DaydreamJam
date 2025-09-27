using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : PlayerSystem
{
    //Gameplay and Inspector Stuff
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private Vector2 targetMovement;
    private Vector2 currentMovement;

    public Collider2D playerCollider;

    [Header("Controlled Movement Variables")]
    public float moveSpeed;
    public float accelleration;

    [Header("Dash Variables")]
    public float dashSpeed;
    public float dashDecelleration;
    [HideInInspector]public bool dashing;

    

    protected override void Awake()
    {
        base.Awake();
        rb = transform.GetComponent<Rigidbody2D>();
        playerID.Attack += Dash;
        playerID.Dash += Dash;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        GetInput();
    }

#region Controlled Movement
    private void GetInput()
    {
        moveInput = movementActions.ReadValue<Vector2>();

        moveInput.Normalize();

        targetMovement = moveInput * moveSpeed;

        if(dashAction.WasPressedThisFrame())
            playerID.Dash.Invoke(dashSpeed);
    }

    private void Move()
    {
        if(!dashing)
        {
            currentMovement = Vector2.MoveTowards(currentMovement, targetMovement, accelleration * Time.fixedDeltaTime);
        }
        else
        {
            currentMovement = Vector2.MoveTowards(currentMovement, targetMovement * 0, dashDecelleration * Time.fixedDeltaTime);

            if(currentMovement.x == 0 && currentMovement.y == 0)
                dashing = false;
        }

        rb.linearVelocity = currentMovement;
    }
#endregion

#region Outside Forces
    public void Shove(Vector2 force)
    {
        currentMovement += force;
    }

    private void Dash(float speed)
    {
        if(!dashing)
        {
            dashing = true;
            currentMovement += moveInput * speed;

            if(Mathf.Sqrt(currentMovement.x * currentMovement.x + currentMovement.y * currentMovement.y) < Mathf.Sqrt(moveInput.x * moveInput.x + moveInput.y * moveInput.y) * speed)
                currentMovement = moveInput * speed;
        }
    }
#endregion
}
