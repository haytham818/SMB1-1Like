using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    private Vector3 targetPosition;
    private GameObject player;
    public GameObject flag;
    private bool isWin;
    private WorldControl worldControl;


    private void Awake()
    {
        targetPosition = GameObject.Find("TargetPosition").transform.position;
        worldControl = GameObject.Find("WorldController").GetComponent<WorldControl>();
    }

    private void Update()
    {
        player = GameObject.Find("PlayerState").GetComponent<PlayerState>().currentGameObj;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isWin)
            {
                StartCoroutine(PlayerWin());
            }
            isWin = true;
        }
    }
    
    IEnumerator PlayerWin()
    {
        worldControl.score += 1000;
        Debug.Log("Win");
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        GameObject.Find("Win").GetComponent<AudioSource>().Play();
        player.GetComponent<PlayerControl>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Animator>().SetTrigger("Win");
        while (Vector3.Distance(player.transform.position, targetPosition) > 0.01f)
        {
            player.transform.position =
                Vector3.MoveTowards(player.transform.position, targetPosition, 4 * Time.deltaTime);
            yield return null;
        }
        player.GetComponent<Animator>().SetTrigger("WinOver");
        player.GetComponent<Rigidbody2D>().gravityScale = 2;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().drag = 0;
        player.GetComponent<PlayerControl>().GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("ThanksForPlaying");
    }
}

