﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHold : ClimbingHold
{

    private IEnumerator coroutine;
    public string sceneName;
    private States grabbedState;

    void Start()
    {
        grabbedState = States.Released;
    }

    void Update()
    {
        if (grabbedState == States.Released && currentState == States.Grabbed)
        {
            grabbedState = States.Grabbed;

            StartCoroutine(coroutine);

            if (gameObject.GetComponent<SpriteRenderer>().sprite != null)
            {
                // Do some sound effect?
                Debug.Log("Do some sound effect?");
            }
            else
            {
                ClimbARHandhold.setHoldColor(gameObject, UnityEngine.Color.cyan);
            }
        }
        else if (grabbedState == States.Grabbed && currentState == States.Released)
        {
            grabbedState = States.Released;

            StopCoroutine(coroutine);

            if (gameObject.GetComponent<SpriteRenderer>().sprite != null)
            {
                // Do some sound effect?
                Debug.Log("Do some sound effect?");
            }
            else
            {
                ClimbARHandhold.setHoldColor(gameObject, UnityEngine.Color.cyan);
            }
        }
    }
    public GameObject canvasGameObject;

    // must call setup script
    public void setup(string sceneName)
    {
        this.sceneName = sceneName;
        coroutine = TransitionToSceneWithDelay(sceneName, 0.1f);
    }


    void OnMouseDown()
    {
        Debug.Log("clicked");
        OnTriggerEnter2D(null);
    }

    IEnumerator TransitionToSceneWithDelay(string sceneName, float delay)
    {
        Debug.Log("Going to scene" + sceneName);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        // Destroy menu text mesh and reset line renderers to uniform color
        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
        Destroy(textMesh);
    }
}
