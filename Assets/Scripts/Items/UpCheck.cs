using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UpCheck : MonoBehaviour
{
    
    public float detectDistance;
    [FormerlySerializedAs("detectOffset")] public Vector2 detectOffsetLeft;
    public Vector2 detectOffsetRight;
    public void Check()
    {
        // �������λ�����Ϸ���һ������
        RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + detectOffsetLeft, Vector2.up, detectDistance);
        RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + detectOffsetRight, Vector2.up, detectDistance);
        // ��������Ƿ�������κ�����
        if (hitLeft.collider != null)
        {
            // ��ȡ����⵽������
            GameObject detectedObject = hitLeft.collider.gameObject;
            // �������⵽��������Ϣ
            Debug.Log("Detected Left object above: " + detectedObject.name);
            if (detectedObject.CompareTag("Enemy") && detectedObject)
            {
                detectedObject.GetComponent<Enemy>().isDie = true;
            }

            if (detectedObject.CompareTag("MushRoom"))
            {
                detectedObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            }
        }
        if (hitRight.collider != null)
        {
            // ��ȡ����⵽������
            GameObject detectedObject = hitRight.collider.gameObject;
            // �������⵽��������Ϣ
            Debug.Log("Detected Right object above: " + detectedObject.name);
            if (detectedObject.CompareTag("Enemy") && detectedObject)
            {
                detectedObject.GetComponent<Enemy>().isDie = true;
            }
            if (detectedObject.CompareTag("MushRoom"))
            {
                detectedObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            }
        }
        
        // ���û�м�⵽�κ����壬����null
        Debug.Log("No object detected above");
    }

    private void OnDrawGizmos()
    {
        // ��Scene��ͼ�л������ߣ��������
        Gizmos.DrawRay((Vector2)transform.position + detectOffsetLeft, Vector2.up * detectDistance);
        Gizmos.DrawRay((Vector2)transform.position + detectOffsetRight, Vector2.up * detectDistance);
    }
}
