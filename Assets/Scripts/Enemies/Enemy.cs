using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : BaseEnemyData
{
    [HideInInspector] public Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    public Transform player;

    public int maxHP;
    private int currentHP;
    public float speed;
    private float currentSpeed;

    [HideInInspector] public float stunTime;

    public EnumHolder.Facing facing;

    private int moveX = 0;

    public float followDistance;
    public float stopDistance;

    public bool isFollowPlayer;
    [HideInInspector] public bool followingPlayer = false;

    public int attackDamage;
    public float hitStun;
    public float knockback;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = player.position.x - transform.position.x;
        if (directionX > 0)
        {
            moveX = 1;
        }
        else if (directionX < 0)
        {
            moveX = -1;
        }
        else
        {
            moveX = 0;
        }

        if (moveX <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facing = EnumHolder.Facing.Left;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facing = EnumHolder.Facing.Right;
        }

        if (stunTime <= 0)
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = 0;
            rb2D.velocity = Vector2.zero;
            stunTime -= Time.deltaTime;
        }

        if (math.abs(transform.position.x - player.position.x) < followDistance &&
            math.abs(transform.position.x - player.position.x) > stopDistance)
        {
            followingPlayer = true;
        }
        else
        {
            followingPlayer = false;
        }

        if (stunTime <= 0)
        {
            spriteRenderer.color = Color.white;
            currentSpeed = speed;
        }
        if (isFollowPlayer && followingPlayer)
        {
            rb2D.velocity = new Vector2(moveX * currentSpeed, rb2D.velocity.y);
        }
    }

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Kill();
        }

        spriteRenderer.color = Color.red;
    }

    public override void getStun(float stunTime)
    {
        this.stunTime = stunTime;
    }

    public override void getKnockback(float knockback, EnumHolder.Facing direction)
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

    public override void Kill()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();

            playerData.TakeDamage(attackDamage);
            playerData.getStun(hitStun);
            playerData.getKnockback(knockback, facing);
        }
    }
}
