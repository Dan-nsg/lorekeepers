using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int strength;
    public int defense;
    public int knowledge;

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
    private Armor armor;
    private bool canDamage = true;
    private SpriteRenderer sprite;
    private bool isDead = false;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerNormalSpeed = maxSpeed;
        animator = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
        health = maxHealth;
        mana = maxMana;
        sprite = GetComponent<SpriteRenderer>();
        FindAnyObjectByType<UIManager>().UpdateUI();
    }

    private void Update() 
    {
        if(!isDead)
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
                FindAnyObjectByType<UIManager>().UpdateUI();
            }
        }
    }

    private void FixedUpdate() 
    {
        if(!isDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            if(canDamage)
                playerRigidBody.velocity = new Vector2(horizontal * playerNormalSpeed, playerRigidBody.velocity.y);

            animator.SetFloat("Speed", Mathf.Abs(horizontal));

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

    public void AddArmor(Armor item)
    {
        armor = item;
        defense = armor.armorDefense;
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

    public int GetHealth()
    {
        return health;
    }

    public int GetMana()
    {
        return mana;
    }

    public void TakeDamage(int damage)
    {
        canDamage = false;
        health -= (damage - defense);
        FindObjectOfType<UIManager>().UpdateUI();
        if(health <= 0)
        {
            animator.SetTrigger("Dead");
            Invoke("ReloadScene", 3f);
            isDead = true;
        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.6f; i += 0.2f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        canDamage = true;
    }

    void ReloadScene()
    {
        Knowledge.instance.gameObject.SetActive(true);
        Knowledge.instance.knowledge = knowledge;
        Knowledge.instance.transform.position = transform.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
