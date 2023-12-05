using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]private float moveDistance;
    [SerializeField]private float moveSpeed;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private WorldControl worldControl;
    private Animator animator;
    private static readonly int CoinFade = Animator.StringToHash("CoinFade");

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        worldControl = GameObject.Find("WorldController").GetComponent<WorldControl>();
    }
    
    private void Update()
    {
        if (spriteRenderer.color.a == 0)//当金币消失后销毁
        {
            Debug.Log("Coin Destroyed");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameObject.Find("CoinAudio").GetComponent<AudioSource>().Play();
        originalPosition = transform.position;
        StartCoroutine(CoinJackUp());
    }

    IEnumerator CoinJackUp()
    {
        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);//计算目标位置
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }//移动到目标位置
        worldControl.coinCount++;
        worldControl.score += 100;
        animator.SetTrigger(CoinFade);
    }
    
}
