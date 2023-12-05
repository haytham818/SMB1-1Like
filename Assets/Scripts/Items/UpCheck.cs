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
        // 从物体的位置向上发射一条射线
        RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position + detectOffsetLeft, Vector2.up, detectDistance);
        RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position + detectOffsetRight, Vector2.up, detectDistance);
        // 检测射线是否击中了任何物体
        if (hitLeft.collider != null)
        {
            // 获取被检测到的物体
            GameObject detectedObject = hitLeft.collider.gameObject;
            // 输出被检测到的物体信息
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
            // 获取被检测到的物体
            GameObject detectedObject = hitRight.collider.gameObject;
            // 输出被检测到的物体信息
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
        
        // 如果没有检测到任何物体，返回null
        Debug.Log("No object detected above");
    }

    private void OnDrawGizmos()
    {
        // 在Scene视图中绘制射线，方便调试
        Gizmos.DrawRay((Vector2)transform.position + detectOffsetLeft, Vector2.up * detectDistance);
        Gizmos.DrawRay((Vector2)transform.position + detectOffsetRight, Vector2.up * detectDistance);
    }
}
