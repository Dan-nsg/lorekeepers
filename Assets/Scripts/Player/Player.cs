using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpFoce;
    public Transform groundCheck;
    public float fireRate;
    public Weapons weaponEquipped;
    public ConsumableItem item;
    public int maxHealth;
    public int maxMana;

    private float playerNormalSpeed;
    private Rigidbody2D playerRigidBody;
    private bool facingRight = true;
    private bool onGround;
    private bool jump = false;
    private bool doubleJump;
    private Animator animator;
    private Attack attack;
    private float nextAttack;
    private int health;
    private int mana;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerNormalSpeed = maxSpeed;
        animator = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
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

        if(Input.GetButtonDown("Fire1") && Time.time > nextAttack && weaponEquipped != null)
        {
            animator.SetTrigger("Attack");
            attack.PlayAnimation(weaponEquipped.weaponAnimation);
            nextAttack = Time.time + fireRate;

        }

        if(Input.GetButtonDown("Fire3"))
        {
            UseItem(item);
            Inventory.inventory.RemoveItem(item);
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

    public void AddWeapon(Weapons weapon)
    {
        weaponEquipped = weapon;
        attack.SetWeapon(weaponEquipped.weaponDamage);
    }

    public void UseItem(ConsumableItem item)
    {
        health += item.healthGain;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }

        mana += item.manaGain;
        if(mana >= maxMana)
        {
            mana = maxMana;
        }
    }
}
