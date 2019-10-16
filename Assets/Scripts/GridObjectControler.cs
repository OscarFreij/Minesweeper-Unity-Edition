using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridObjectControler : MonoBehaviour
{

    public Tile myTile { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateVisual()
    {
        if (myTile.isBomb)
        {
            transform.Find("Canvas").GetComponent<DestroyGameObject>().DestroyObject();
        }
        else if (!myTile.isBomb)
        {
            if (myTile.neighboringBombs != 0)
            {
                transform.Find("Canvas").GetComponentInChildren<Text>().text = myTile.neighboringBombs.ToString();
            }
            else
            {
                transform.Find("Canvas").GetComponent<DestroyGameObject>().DestroyObject();
            }
            transform.Find("Bomb").GetComponent<DestroyGameObject>().DestroyObject();
        }
    }

}
