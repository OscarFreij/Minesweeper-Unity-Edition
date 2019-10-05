using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int[] currentGridSize = new int[2] { 10, 10 };
    public int bombs = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GridControl gridControl = new GridControl();
        GridControl.CreateGrid(currentGridSize[0], currentGridSize[1], 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
