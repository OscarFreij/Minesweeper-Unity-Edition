﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject GridGameObjects;
    public int[] currentGridSize = new int[2] { 10, 10 };
    public int bombs = 0;


    private bool admin = true;

    public GridControl gridControl;

    // Start is called before the first frame update
    void Start()
    {

        GridGameObjects = Resources.Load<GameObject>("Prefabs/GridObject_Template_new");

        if (GridGameObjects == null)
        {
            Debug.LogError("GridGameObjects Is NULL!");
        }
        else
        {
            Debug.Log("GridGameObjects was succesfully loaded");
        }

        gridControl = new GridControl();
        gridControl.CreateGrid(currentGridSize[0], currentGridSize[1], bombs, GridGameObjects);
        CenterCamera(currentGridSize[0], currentGridSize[1]);
    }

    public void CreateNewGrid()
    {
        gridControl = null;

        foreach (Transform child in GameObject.Find("Grid").transform)
        {
            Destroy(child.gameObject);
        }

        gridControl = new GridControl();
        gridControl.CreateGrid(currentGridSize[0], currentGridSize[1], bombs, GridGameObjects);
        CenterCamera(currentGridSize[0], currentGridSize[1]);
    }
    
    // Update is called once per frame
    void Update()
    {

        if (admin)
        {
            if (Input.GetKeyDown("c"))
            {
                gridControl.clearShells();
            }
        }

    }

    public void CenterCamera(float x, float y)
    {

        float hight;
        float xPos = x;
        float yPos = y;

        if (x >= y)
        {
            hight = x;
        }
        else
        {
            hight = y;
        }

        xPos = x / 2;
        yPos = y / 2;

        
        if (x % 2 == 1)
        {
            xPos -= 0.5f;
        }

        if (y % 2 == 1)
        {
            yPos -= 0.5f;
        }


        GameObject.Find("Main Camera").transform.position = new Vector3(xPos, hight, yPos);

    }
}
