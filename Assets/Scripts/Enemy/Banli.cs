using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banli : MonoBehaviour
{
    Animator animator;
    private static readonly int Die = Animator.StringToHash("die");
    public bool isDie;
    private Rigidbody2D rb;
    private Enemy enemy;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }
    
    private void Update()
    {
        if (isDie || enemy.isDie)
        {
            StartCoroutine(OnDie());
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.contacts[0].normal.y == -1 || other.gameObject.CompareTag("Enemy") && other.contacts[0].normal.y == -1)
        {
            animator.SetTrigger(Die);
            enemy.moveSpeed = 0;
        }
    }
    
    IEnumerator OnDie()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.AddForce(-Vector2.up * 0.1f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
