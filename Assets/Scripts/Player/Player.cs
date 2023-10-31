using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerSkills
{
    dash, doubleJump
}

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpFoce;
    public Transform groundCheck;
    public float fireRate;
    public Weapons weaponEquipped;
    public Armor armor;
    public ConsumableItem item;
    public int maxHealth;
    public int maxMana;
    public int strength;
    public int defense;
    public int knowledge;
    public float dashForce;
    public bool doubleJumpSkill = false;
    public bool dashSkill = false;

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
    private bool canDamage = true;
    private SpriteRenderer sprite;
    private bool isDead = false;
    private bool dash = false;
    private GameManager gm;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
        sprite = GetComponent<SpriteRenderer>();
        
        gm = GameManager.gm;
        SetPlayer();
    }

    private void Update() 
    {
        if(!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            if(onGround)
                doubleJump = false;
            
            if(Input.GetButtonDown("Jump") && (onGround || (!doubleJump && doubleJumpSkill)))
            {
                jump = true;
                if(!doubleJump && !onGround)
                    doubleJump = true;
            }

            if(Input.GetButtonDown("Fire1") && Time.time > nextAttack && weaponEquipped != null)
            {
                dash = false;
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

            if(Input.GetKeyDown(KeyCode.Q) && onGround && !dash && dashSkill)
            {
                playerRigidBody.velocity = Vector2.zero;
                animator.SetTrigger("Dash");
            }
        }
    }

    private void FixedUpdate() 
    {
        if(!isDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            if(canDamage && !dash)
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
            if(dash)
            {
                int hforce = facingRight ? 1 : -1;
                playerRigidBody.velocity = Vector2.left * dashForce * hforce;
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

    public void DashTrue()
    {
        dash = true;
    }

    public void DashFalse()
    {
        dash = false;
    }

    public void SetPlayerSkill(PlayerSkills skills)
    {
        if(skills == PlayerSkills.dash)
        {
            dashSkill = true;
        }
        else if(skills == PlayerSkills.doubleJump)
        {
            doubleJumpSkill = true;
        }
    }

    public void SetPlayer()
    {
        Vector3 playerPos = new Vector3(gm.playerPosX, gm.playerPosY, 0);
        transform.position = playerPos;
        maxHealth = gm.health;
        maxHealth = gm.mana;
        playerNormalSpeed = maxSpeed;
        health = maxHealth;
        mana = maxMana;
        strength = gm.strength;
        knowledge = gm.knowledge;
        doubleJumpSkill = gm.canDoubleJump;
        dashSkill = gm.canBackDash;
        if(gm.currentArmorID > 0)
            AddArmor(Inventory.inventory.itemDatabase.GetArmor(gm.currentArmorID));
        if(gm.currentWeaponID > 0)
            AddWeapon(Inventory.inventory.itemDatabase.GetWeapon(gm.currentWeaponID));
    }
}
