using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectShellControl : MonoBehaviour
{
    private Color baseColor;
    private Color startColor;
    private bool isFlaged = false;
    private GridObjectControler myGOC;
    private GridControl GC;

    private void Start()
    {
        baseColor = GetComponentInChildren<Renderer>().material.color;
        startColor = GetComponentInChildren<Renderer>().material.color;

        myGOC = transform.parent.GetComponent<GridObjectControler>();
        GC = GameObject.Find("GameMaster").GetComponent<GameMaster>().gridControl;
    }
    void OnMouseEnter()
    {
        startColor = GetComponentInChildren<Renderer>().material.color;
        GetComponentInChildren<Renderer>().material.color = Color.yellow;
        Debug.Log("Now hovering over new block!");
    }
    void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.color = startColor;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Right Mouse Button Clicked on: " + transform.parent.name);
            flag();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Left Mouse Button Clicked on: " + transform.parent.name);
            open(true);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log($"GameObject : {transform.parent.name}\nID : {myGOC.myTile.id}\nX Pos : {myGOC.myTile.x}\nY Pos : {myGOC.myTile.y}\nisBomb : {myGOC.myTile.isBomb}\nNeighboringBombs : {myGOC.myTile.neighboringBombs}\nisOpen : {myGOC.myTile.isOpen}\nisFlaged : {myGOC.myTile.isFlaged}");
        }

    }

    public void open(bool openNext)
    {
        if (!isFlaged)
        {
            myGOC.myTile.isOpen = true;
            Debug.Log(transform.parent.name + " has been opend!");

            if (myGOC.myTile.isBomb)
            {
                //Trigger gameover  
            }
            else if (openNext)
            {
                GC.checkNeighboringTiles(myGOC.myTile.x, myGOC.myTile.y);
                //Debug.Log($"GameObject : {transform.parent.name}\nID : {myGOC.myTile.id}\nX Pos : {myGOC.myTile.x}\nY Pos : {myGOC.myTile.y}\nisBomb : {myGOC.myTile.isBomb}\nNeighboringBombs : {myGOC.myTile.neighboringBombs}\nisOpen : {myGOC.myTile.isOpen}\nisFlaged : {myGOC.myTile.isFlaged}");
            }
            else
            {

            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Can't open {transform.parent.name} as it is flaged");
        }
    }

    public void flag()
    {
        if (!isFlaged)
        {
            isFlaged = true;
            myGOC.myTile.isFlaged = isFlaged;
            GetComponentInChildren<Renderer>().material.color = Color.green;
            startColor = GetComponentInChildren<Renderer>().material.color;
            Debug.Log(transform.parent.name + " has been flaged!");
        }
        else
        {
            isFlaged = false;
            myGOC.myTile.isFlaged = isFlaged;
            startColor = baseColor;
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
            Debug.Log(transform.parent.name + " has been unflaged!");
        }

    }
}
