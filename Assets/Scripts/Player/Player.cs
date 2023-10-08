using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpFoce;
    public Transform groundCheck;

    private float playerNormalSpeed;
    private Rigidbody2D playerRigidBody;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private bool doubleJump;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerNormalSpeed = maxSpeed;
    }

    private void Update() 
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if(onGround)
            doubleJump = false;
        
        if(Input.GetButtonDown("Jump") && (onGround || !doubleJump))
        {
            jump = true;
            if(!doubleJump && !onGround)
                doubleJump = true;
        }
    }

    private void FixedUpdate() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        playerRigidBody.velocity = new Vector2(horizontal * playerNormalSpeed, playerRigidBody.velocity.y);

        if(horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if(horizontal < 0 && facingRight)
        {
            Flip();
        }

        if(jump)
        {
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * jumpFoce);
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
