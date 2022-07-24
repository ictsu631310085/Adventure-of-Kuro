using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Golem : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;

    [SerializeField]
    private bool _isAttack;
    public float attackTime;
    [SerializeField]
    private float _attackTimeValue;
    public float attackCooldownTime;
    [SerializeField]
    private float _attackCooldown;

    public Transform attackPoint;
    public GameObject shockwavePrefab;

    [SerializeField]
    private int _attackRNG;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();

        _attackCooldown = attackCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.followingPlayer && _enemy.stunTime <= 0)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        // Attack Cooldown Update
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }
        else
        {
            // Attack Randomly
            _attackRNG = Random.Range(0, 9);
            if (_attackRNG % 2 == 0)
            {
                Attack();
            }
            else
            {
                _attackCooldown = attackCooldownTime;
            }
        }

        // Attack Update
        if (_isAttack)
        {
            if (_attackTimeValue > 0)
            {
                _attackTimeValue -= Time.deltaTime;
            }
            else
            {
                _isAttack = false;
                _attackCooldown = attackCooldownTime;
                GameObject shockwave = Instantiate(shockwavePrefab, attackPoint.position, attackPoint.rotation);
                shockwave.transform.localScale = transform.localScale;
                if (transform.localScale.x == 1)
                {
                    shockwave.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500);
                }
                else if (transform.localScale.x == -1)
                {
                    shockwave.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500);
                }
                Destroy(shockwave, 1);
                _animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Attack()
    {
        _attackTimeValue = attackTime;
        _isAttack = true;
        _animator.SetBool("isAttacking", true);
    }
}
