using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShellTrigger : MonoBehaviour
{
    private Color baseColor { get; set; }
    private Color startColor { get; set; }
    private GridObjectControler Controler { get; set; }


    private void Start()
    {
        baseColor = GetComponentInChildren<Renderer>().material.color;
        startColor = GetComponentInChildren<Renderer>().material.color;

        Controler = transform.parent.GetComponent<GridObjectControler>();
    }

    void OnMouseEnter()
    {
        if (true)
        {
            startColor = GetComponentInChildren<Renderer>().material.color;
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
            Debug.Log($"Now hovering over {transform.name} block!");
        }
    }

    void OnMouseExit()
    {
        if (true)
        {
            GetComponentInChildren<Renderer>().material.color = startColor;
            Debug.Log("Now hovering over new block!");
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Button Clicked on: " + transform.name);
            Controler.Flag();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Button Clicked on: " + transform.name);
            Controler.Open();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log($"GameObject : {transform.parent.name}\nID : {Controler.myTile.id}\nX Pos : {Controler.myTile.x}\nY Pos : {Controler.myTile.y}\nisBomb : {Controler.myTile.isBomb}\nNeighboringBombs : {Controler.myTile.neighboringBombs}\nisOpen : {Controler.myTile.isOpen}\nisFlaged : {Controler.myTile.isFlaged}");
        }
    }

}
