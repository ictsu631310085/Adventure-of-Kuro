using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerData : MonoBehaviour, IDamageable, IKillable, IStunable ,IKnockbackable
{
    // Components
    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public PlayerPrefabsHolder prefabsHolder;

    // Walk Settings
    [HideInInspector]
    public float moveX;
    [Header("Walk Settings")]
    public float speed;
    [HideInInspector]
    public float currentSpeed;

    [HideInInspector]
    public float stunTime;

    // Facing Direction
    [HideInInspector]
    public EnumHolder.Facing facing;

    // Element Settings
    [Header("Element Settings")]
    public EnumHolder.Element currentElement;
    public ElementHUD elementHUD;

    // Jump Settings
    [HideInInspector]
    public bool isGrounded;
    [Header("Jump Settings")]
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask groundLayer;
    public float jumpForce;
    [HideInInspector]
    public bool isJumping = false;
    public float jumpTime;
    [HideInInspector]
    public int jumpRemainValue;
    [HideInInspector]
    public int jumpRemain;

    private int normalJumpCount = 1;
    private int ventusJumpCount = 2;

    // Attack Settings
    [Header("Attack")]
    public Transform attackPoint;
    public LayerMask damageableLayers;
    public float attackRange;
    public int attackDamage;

    private float hitStun;
    public float normalHitStun;
    public float terraHitStun;

    private float knockback;
    public float normalKnockback;
    public float terraKnockback;

    private GameObject hitFX;

    // Jump Attack
    [HideInInspector]
    public bool jumpAttackEnabled;
    public float jumpAttackRange;
    [HideInInspector]
    public bool isJumpAttack;

    // Dash Settings
    [Header("Dash Settings")]
    [HideInInspector]
    public float dashSpeed;
    public float ignisDashSpeed;
    public float ventusDashSpeed;
    public float dashTimeValue;
    [HideInInspector]
    public float dashTimer;

    // HP Settings
    [Header("HP Settings")]
    public int maxHP;
    [HideInInspector]
    public int currentHP;

    // MP Settings
    [Header("MP Settings")]
    public int maxMP;
    [HideInInspector]
    public int currentMP;

    // Audio Settings
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip whooshClip;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        prefabsHolder = GetComponent<PlayerPrefabsHolder>();

        currentSpeed = speed;
        jumpRemainValue = normalJumpCount;
        hitStun = normalHitStun;
        knockback = normalKnockback;
        hitFX = prefabsHolder.ignisHitFX;
        dashSpeed = ignisDashSpeed;
        dashTimer = dashTimeValue;
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (stunTime <= 0)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void TurnCharacter()
    {
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
            facing = EnumHolder.Facing.Right;
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            facing = EnumHolder.Facing.Left;
        }
    }

    public void ShiftElementRight()
    {
        switch (currentElement)
        {
            // Ignis -> Terra
            case EnumHolder.Element.Ignis:
                currentElement = EnumHolder.Element.Terra;
                jumpRemainValue = normalJumpCount;
                hitStun = terraHitStun;
                knockback = terraKnockback;
                hitFX = prefabsHolder.terraHitFX;
                animator.SetTrigger("Terra");
                Instantiate(prefabsHolder.shiftTerraFX, transform);
                break;
            // Terra -> Ventus
            case EnumHolder.Element.Terra:
                currentElement = EnumHolder.Element.Ventus;
                jumpRemainValue = ventusJumpCount;
                hitStun = normalHitStun;
                knockback = normalKnockback;
                hitFX = prefabsHolder.ventusHitFX;
                dashSpeed = ventusDashSpeed;
                animator.SetTrigger("Ventus");
                Instantiate(prefabsHolder.shiftVentusFX, transform);
                break;
            // Ventus -> Ignis
            case EnumHolder.Element.Ventus:
                currentElement = EnumHolder.Element.Ignis;
                jumpRemainValue = normalJumpCount;
                hitStun = normalHitStun;
                knockback = normalKnockback;
                hitFX = prefabsHolder.ignisHitFX;
                dashSpeed = ignisDashSpeed;
                animator.SetTrigger("Ignis");
                Instantiate(prefabsHolder.shiftIgnisFX, transform);
                break;
        }
        elementHUD.UpdateElementHUD(currentElement);

        if (isGrounded)
        {
            jumpRemain = jumpRemainValue;
        }
    }

    public void ShiftElementLeft()
    {
        switch (currentElement)
        {
            // Ignis -> Ventus
            case EnumHolder.Element.Ignis:
                currentElement = EnumHolder.Element.Ventus;
                jumpRemainValue = ventusJumpCount;
                hitStun = normalHitStun;
                knockback = normalKnockback;
                hitFX = prefabsHolder.ventusHitFX;
                dashSpeed = ventusDashSpeed;
                animator.SetTrigger("Ventus");
                Instantiate(prefabsHolder.shiftVentusFX, transform);
                break;
            // Terra -> Ignis
            case EnumHolder.Element.Terra:
                currentElement = EnumHolder.Element.Ignis;
                jumpRemainValue = normalJumpCount;
                hitStun = normalHitStun;
                knockback = normalKnockback;
                hitFX = prefabsHolder.ignisHitFX;
                dashSpeed = ignisDashSpeed;
                animator.SetTrigger("Ignis");
                Instantiate(prefabsHolder.shiftIgnisFX, transform);
                
                break;
            // Ventus -> Terra
            case EnumHolder.Element.Ventus:
                currentElement = EnumHolder.Element.Terra;
                jumpRemainValue = normalJumpCount;
                hitStun = terraHitStun;
                knockback = terraKnockback;
                hitFX = prefabsHolder.terraHitFX;
                animator.SetTrigger("Terra");
                Instantiate(prefabsHolder.shiftTerraFX, transform);
                break;
        }
        elementHUD.UpdateElementHUD(currentElement);

        if (isGrounded)
        {
            jumpRemain = jumpRemainValue;
        }
    }

    public void MeleeDash(int mDashSpeed)
    {
        switch (facing)
        {
            case EnumHolder.Facing.Right:
                rb2D.AddForce(Vector2.right * mDashSpeed);
                break;
            case EnumHolder.Facing.Left:
                rb2D.AddForce(Vector2.left * mDashSpeed);
                break;
        }
    }

    public void MeleeHitCheck()
    {
        Collider2D[] hitEntities;
        switch (isJumpAttack)
        {
            case false:
                hitEntities = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayers);
                break;
            case true:
                hitEntities = Physics2D.OverlapCircleAll(transform.position, jumpAttackRange, damageableLayers);
                break;
        }
        Quaternion _rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        foreach (Collider2D entity in hitEntities)
        {
            Vector3 _targetPosition = entity.GetComponent<Transform>().position;
            Vector3 _hitPosition = attackPoint.position + ((_targetPosition - attackPoint.position) / 2);

            entity.GetComponent<BaseEnemyData>().TakeDamage(attackDamage);
            entity.GetComponent<BaseEnemyData>().getStun(hitStun);
            entity.GetComponent<BaseEnemyData>().getKnockback(knockback, facing);
            Instantiate(hitFX, _hitPosition, _rotation);

            Debug.Log("You hit " + entity.name);
        }

        audioSource.PlayOneShot(whooshClip);
    }

    public void InstMeleeFX(PlayerStateManager stateManager)
    {
        switch (currentElement)
        {
            case EnumHolder.Element.Ignis:
                switch (stateManager.currentState)
                {
                    case PlayerStateManager.State.Melee1:
                        Instantiate(prefabsHolder.ignisMelee1FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee2:
                        Instantiate(prefabsHolder.ignisMelee2FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee3:
                        Instantiate(prefabsHolder.ignisMelee3FX, attackPoint.transform);
                        break;
                }
                break;
            case EnumHolder.Element.Terra:
                switch (stateManager.currentState)
                {
                    case PlayerStateManager.State.Melee1:
                        Instantiate(prefabsHolder.terraMelee1FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee2:
                        Instantiate(prefabsHolder.terraMelee2FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee3:
                        Instantiate(prefabsHolder.terraMelee3FX, attackPoint.transform);
                        break;
                }
                break;
            case EnumHolder.Element.Ventus:
                switch (stateManager.currentState)
                {
                    case PlayerStateManager.State.Melee1:
                        Instantiate(prefabsHolder.ventusMelee1FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee2:
                        Instantiate(prefabsHolder.ventusMelee2FX, attackPoint.transform);
                        break;
                    case PlayerStateManager.State.Melee3:
                        Instantiate(prefabsHolder.ventusMelee3FX, attackPoint.transform);
                        break;
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (feetPosition == null)
            return;

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(feetPosition.position, checkRadius);

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.DrawWireSphere(transform.position, jumpAttackRange);
    }

    public void InstDoubleJumpFX()
    {
        Instantiate(prefabsHolder.ventusDoubleJumpFX, transform);
    }

    public void InstDashFX()
    {
        if (currentElement == EnumHolder.Element.Ventus)
        {
            Instantiate(prefabsHolder.ventusDashFX, transform.position, transform.rotation);
        }

        audioSource.PlayOneShot(whooshClip);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Kill();
        }

        spriteRenderer.color = Color.red;
    }

    // Unused
    public void Kill()
    {
        
    }

    public void getStun(float stunTime)
    {
        this.stunTime = stunTime;
    }

    public void getKnockback(float knockback, EnumHolder.Facing direction)
    {
        switch (direction)
        {
            case EnumHolder.Facing.Right:
                rb2D.AddForce(Vector2.right * knockback * rb2D.mass);
                break;
            case EnumHolder.Facing.Left:
                rb2D.AddForce(Vector2.left * knockback * rb2D.mass);
                break;
        }
    }
}
