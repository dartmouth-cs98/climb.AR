﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ManualSync : MonoBehaviour
{
    //private constants
    private float BORDER_WIDTH = 0.05f
        ;
    // Game objects
    public GameObject[] cornerCircles;
    public GameObject CornerCircle;
    public Camera mainCam;

    // Use this for initialization
    void Start()
    {
        InitCornerCircles();
        DrawBorder(BORDER_WIDTH);
    }

    // Update is called once per frame
    void Update()
    {
        // continue to next scene, saving the coordinates of the corner circles as state
        if (Input.GetKeyDown("space"))
        {
            // 0,0 is top left, +y points down
            StateManager.instance.kinectUpperLeft = ClimbARUtils.worldSpaceToFraction(
                this.cornerCircles[0].transform.localPosition.x,
                this.cornerCircles[0].transform.localPosition.y,
                mainCam);
            StateManager.instance.kinectUpperRight = ClimbARUtils.worldSpaceToFraction(
                this.cornerCircles[1].transform.localPosition.x,
                this.cornerCircles[1].transform.localPosition.y,
                mainCam);
            StateManager.instance.kinectLowerRight = ClimbARUtils.worldSpaceToFraction(
                    this.cornerCircles[2].transform.localPosition.x,
                this.cornerCircles[2].transform.localPosition.y,
                mainCam);
            StateManager.instance.kinectLowerLeft = ClimbARUtils.worldSpaceToFraction(
                this.cornerCircles[3].transform.localPosition.x,
                this.cornerCircles[3].transform.localPosition.y,
                mainCam);
            SceneManager.LoadScene(SceneUtils.Names.holdSetup);
            //SceneManager.LoadScene(SceneUtils.Names.demo);
        }

        // reset to outer corners
        if (Input.GetKeyDown("r"))
        {
            if (this.cornerCircles != null)
            {
                this.cornerCircles[0].transform.localPosition =
                    ClimbARUtils.fractionToWorldSpace(0f, 0f, this.mainCam);
                this.cornerCircles[1].transform.localPosition =
                    ClimbARUtils.fractionToWorldSpace(1f, 0f, this.mainCam);
                this.cornerCircles[2].transform.localPosition =
                    ClimbARUtils.fractionToWorldSpace(1f, 1f, this.mainCam);
                this.cornerCircles[3].transform.localPosition =
                    ClimbARUtils.fractionToWorldSpace(0f, 1f, this.mainCam);
            }
        }
    }

    /// <summary>
    /// 0, 1, 2, 3 -> top left, top right, bottom right, bottom left
    /// </summary>
    void InitCornerCircles()
    {
        this.cornerCircles = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            this.cornerCircles[i] = GameObject.Instantiate(CornerCircle);
            this.cornerCircles[i].name = "CornerCircle " + (i);
        }
        this.cornerCircles[0].transform.localPosition =
            ClimbARUtils.fractionToWorldSpace(0.1f, 0.1f, this.mainCam);
        this.cornerCircles[1].transform.localPosition =
            ClimbARUtils.fractionToWorldSpace(0.9f, 0.1f, this.mainCam);
        this.cornerCircles[2].transform.localPosition =
            ClimbARUtils.fractionToWorldSpace(.9f, .9f, this.mainCam);
        this.cornerCircles[3].transform.localPosition =
            ClimbARUtils.fractionToWorldSpace(0.1f, 0.9f, this.mainCam);
    }

    void DrawBorder(float width)
    {
        Vector2 topLeft = ClimbARUtils.fractionToWorldSpace(0f, 0f, this.mainCam); ;
        Vector2 bottomLeft = ClimbARUtils.fractionToWorldSpace(0f, 1f, this.mainCam);
        Vector2 topRight = ClimbARUtils.fractionToWorldSpace(1f, 0f, this.mainCam);
        Vector2 bottomRight = ClimbARUtils.fractionToWorldSpace(1f, 1f, this.mainCam);

        DrawLine(topLeft, bottomLeft, width);
        DrawLine(topRight, bottomRight, width);
        DrawLine(topLeft, topRight, width);
        DrawLine(bottomLeft, bottomRight, width);
    }

    private void DrawLine(Vector2 start, Vector2 end, float width)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}