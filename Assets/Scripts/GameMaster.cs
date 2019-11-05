using System.Collections;
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
        gridControl = new GridControl();
        gridControl.CreateGrid(currentGridSize[0], currentGridSize[1], bombs, GridGameObjects);
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
}
