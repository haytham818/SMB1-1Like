using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class QmBrick : MonoBehaviour
{
    [SerializeField]private float moveDistance;
    [SerializeField]private float moveSpeed;
    [FormerlySerializedAs("coin")] [SerializeField]private GameObject spawnObj;
    private Vector3 originalPosition;
    private Animator anim;
    private UpCheck upCheck;
    private bool isTurned;
    private static readonly int QmTurn = Animator.StringToHash("QmTurn");
    private bool isCoinSpawned;
    private GameObject currentUpGameObj;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        upCheck = GetComponent<UpCheck>();
        currentUpGameObj = null;
    }
    private void Start()
    {
        originalPosition = transform.position;//��¼ԭʼλ��
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.contacts[0].normal.y == 1 && !isTurned)//�ж��Ƿ��Ǵ��·���ײ
        {
            GameObject.Find("HeadOn").GetComponent<AudioSource>().Play();
            upCheck.Check();
            if (currentUpGameObj)
            {
                Debug.Log(currentUpGameObj);
            }
            CoinSpawn();
            StartCoroutine(JackUp());
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().AddForce(-transform.up * 5,ForceMode2D.Impulse);//AddForce��һ�����µ���������������
        }
    }

    private void CoinSpawn()
    {
        if (spawnObj == null || isCoinSpawned) return;
        Instantiate(spawnObj,transform.position, Quaternion.identity);
        isCoinSpawned = true;
    }
    
    IEnumerator JackUp()
    {
        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);//����Ŀ��λ��
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }//�ƶ���Ŀ��λ��
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }//�ƶ���ԭʼλ��
        isTurned = true;
        anim.SetTrigger(QmTurn);
    }
}
