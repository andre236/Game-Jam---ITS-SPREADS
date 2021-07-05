using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBehaviour : MonoBehaviour
{
    private int _currentHP = 2;
    private float _moveSpeed = 5f;
    private bool _attackingPlayer = false;
    private bool _isFollowingPlayer = false;
    private bool _onCooldownAttack = false;

    private Transform _targetPlayer;
    private SpriteRenderer _spriteRain;
    private Animator _rainAnim;
    [SerializeField]
    private GameObject _dropWaterPrefab;

    private void Awake()
    {
        _targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _spriteRain = GetComponent<SpriteRenderer>();
        _rainAnim = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();
        RefreshSprite();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void DetectPlayer()
    {
        bool detectingPlayer = Vector2.Distance(transform.position, _targetPlayer.position) < 15f;

        if (detectingPlayer && !_isFollowingPlayer)
        {
            _isFollowingPlayer = true;
        }


    }

    void RefreshSprite()
    {
        if (_targetPlayer.position.x > transform.position.x)
        {
            _spriteRain.flipX = true;
        }
        else
        {
            _spriteRain.flipX = false;
        }
    }

    void FollowPlayer()
    {
        bool rangeToAttack = (Mathf.Abs(transform.position.x - _targetPlayer.transform.position.x) < 0.5f);

        if (_isFollowingPlayer && !_attackingPlayer && !rangeToAttack)
        {
            Vector2 playerPosition = new Vector2(_targetPlayer.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, _moveSpeed * Time.fixedDeltaTime);
        }

        if (rangeToAttack && _isFollowingPlayer && !_attackingPlayer && !_onCooldownAttack)
        {
            _isFollowingPlayer = false;
            _attackingPlayer = true;
            AttackDrop();
            _onCooldownAttack = true;
            StartCoroutine(CooldownToAttack());
        }
    }

    void AttackDrop()
    {
        if (!_isFollowingPlayer && _attackingPlayer)
        {
            _attackingPlayer = false;
            _isFollowingPlayer = true;
            Instantiate(_dropWaterPrefab, new Vector2(transform.position.x + Random.Range(-0.74f, 0.83f), transform.position.y + Random.Range(-0.53f, -0.73f)), Quaternion.identity);
        }
    }

    void Die()
    {
        if (_currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(collision.gameObject);
            _rainAnim.SetTrigger("TakingDamage");
            _currentHP--;
            Die();
        }
    }

    IEnumerator CooldownToAttack()
    {
        yield return new WaitForSeconds(0.3f);
        _onCooldownAttack = false;
    }
}
