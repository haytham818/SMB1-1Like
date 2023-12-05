using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartCtor : MonoBehaviour
{
    private Vector3 position1;
    private Vector3 position2;
    private PlayerInputController playerInputController;


    private void Awake()
    {
        playerInputController = new PlayerInputController();
        position1 = GameObject.Find("p1").transform.position;
        position2 = GameObject.Find("p2").transform.position;
        playerInputController.Start.MoveUp.started += MoveUpOnstart;
        playerInputController.Start.MoveDown.started += MoveDownOnstart;
        playerInputController.Start.GamStart.started += GameStart;
    }

    private void GameStart(InputAction.CallbackContext obj)
    {
        GameObject.Find("Select").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("GamePlay");
    }

    private void MoveDownOnstart(InputAction.CallbackContext obj)
    {
        transform.position = position1;
        GameObject.Find("Select").GetComponent<AudioSource>().Play();
    }


    private void MoveUpOnstart(InputAction.CallbackContext obj)
    {
        GameObject.Find("Select").GetComponent<AudioSource>().Play();
        transform.position = position2;
    }


    private void OnEnable()
    {
        playerInputController.Enable();
    }

    private void OnDisable()
    {
        playerInputController.Disable();
    }
    
    
    
}
