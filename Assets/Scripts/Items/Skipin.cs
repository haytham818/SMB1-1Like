using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skipin : MonoBehaviour
{
    private Vector3 skipPosition;
    public GameObject player;
    public GameObject skipCamera;
    private PlayerInputController playerInputController;
    public bool isWillSkip; 
    
    private void Awake()
    {
        playerInputController = new PlayerInputController();
        skipPosition = GameObject.Find("SkipPosition").transform.position;
        playerInputController.GamePlay.SitDown.started += Skip;
    }

    private void Skip(InputAction.CallbackContext obj)
    {
        if (isWillSkip)
        {
            Debug.Log("Skip");
            StartCoroutine(SkipCoroutine());
        }
    }


    private void OnEnable()
    {
        playerInputController.Enable();
    }
    
    private void OnDisable()
    {
        playerInputController.Disable();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            isWillSkip = true;
        }   
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWillSkip = false;
        }   
    }
    
    IEnumerator SkipCoroutine()
    {
        GameObject.Find("PipeIn").GetComponent<AudioSource>().Play();
        float originalGScale = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<PlayerControl>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        Vector3 targetPosition = player.transform.position + new Vector3(0, -2, 0);
        while (Vector3.Distance(player.transform.position, targetPosition) > 0.01f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, 2 * Time.deltaTime);
            yield return null;
        }
        player.GetComponent<PlayerControl>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = originalGScale;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        player.transform.position = skipPosition;
        skipCamera.SetActive(true);
    }
    
}
