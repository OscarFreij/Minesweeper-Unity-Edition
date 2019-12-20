using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridObjectControler : MonoBehaviour
{

    public Tile myTile { get; set; }
    private Color baseColor { get; set; }
    private Color startColor { get; set; }
    private GameObject shell { get; set; }
    private GridControl gridControl { get; set; }


    private void Start()
    {
        shell = transform.Find("Shell").gameObject;

        baseColor = shell.GetComponentInChildren<Renderer>().material.color;
        startColor = shell.GetComponentInChildren<Renderer>().material.color;

        gridControl = GameObject.Find("GameMaster").GetComponent<GameMaster>().gridControl;
    }

    public void Open()
    {
        if (!myTile.isOpen)
        {
            if (!myTile.isFlaged)
            {
                myTile.isOpen = true;
                gridControl.openTileAmount++;
                shell.GetComponent<DestroyGameObject>().DestroyObject();

                if (myTile.isBomb)
                {
                    //Trigger GameOver!
                    Debug.Log($"GameOver {transform.name} - Tile tripped a bomb");
                    shell.GetComponent<GridShellTrigger>().acceptInput = false;
                    return;
                }

                for (int i = 0; i < 8; i++)
                {
                    int xlook = 0;
                    int ylook = 0;
                    switch (i)
                    {
                        case 0:
                            xlook = myTile.x - 1;
                            ylook = myTile.y - 1;
                            break;

                        case 1:
                            xlook = myTile.x - 1;
                            ylook = myTile.y;
                            break;

                        case 2:
                            xlook = myTile.x - 1;
                            ylook = myTile.y + 1;
                            break;

                        case 3:
                            xlook = myTile.x;
                            ylook = myTile.y - 1;
                            break;

                        case 4:
                            xlook = myTile.x;
                            ylook = myTile.y + 1;
                            break;

                        case 5:
                            xlook = myTile.x + 1;
                            ylook = myTile.y - 1;
                            break;

                        case 6:
                            xlook = myTile.x + 1;
                            ylook = myTile.y;
                            break;

                        case 7:
                            xlook = myTile.x + 1;
                            ylook = myTile.y + 1;
                            break;
                    }

                    try
                    {
                        if (
                            !gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().myTile.isFlaged &&
                            !gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().myTile.isBomb &&
                            !gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().myTile.isOpen
                            )
                        {

                            if (myTile.neighboringBombs == 0)
                            {
                                gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().Open();
                            }
                            else
                            {
                                if (gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().myTile.neighboringBombs == 0)
                                {
                                    gridControl.gridObjects[xlook, ylook].GetComponent<GridObjectControler>().Open();
                                }
                            }

                            /*
                             * If myTile has number, only open numberles.
                             * else
                             * If myTile don't have a number, open all neighbors.
                             */

                        }
                    }
                    catch
                    {
                        Debug.Log($"Can't open {transform.name} - Tile dose not exist!");
                    }
                }

            }
            else
            {
                Debug.Log($"Can't open {transform.name} - Tile is flagged");
            }
        }
        else
        {
            Debug.Log($"Can't open {transform.name} - Tile is already open");
        }
    }

    public void Flag()
    {
        if (true)
        {
            if (!myTile.isFlaged)
            {
                myTile.isFlaged = true;
                shell.GetComponentInChildren<Renderer>().material.color = Color.green * 0.8f;
                shell.GetComponent<GridShellTrigger>().startColor = Color.green * 0.8f;
                startColor = shell.GetComponentInChildren<Renderer>().material.color;
                Debug.Log(transform.name + " has been flaged!");
            }
            else
            {
                myTile.isFlaged = false;
                shell.GetComponent<GridShellTrigger>().startColor = shell.GetComponent<GridShellTrigger>().baseColor;
                shell.GetComponentInChildren<Renderer>().material.color = Color.yellow * 0.8f;
                Debug.Log(transform.name + " has been unflaged!");
            }
        }
    }

    public void UpdateVisual()
    {
        Text number = transform.Find("Canvas").GetComponentInChildren<Text>();
        if (myTile.isBomb)
        {
            transform.Find("Canvas").GetComponent<DestroyGameObject>().DestroyObject();
        }
        else if (!myTile.isBomb)
        {
            if (myTile.neighboringBombs != 0)
            {
                number.text = myTile.neighboringBombs.ToString();
                
                switch (myTile.neighboringBombs)
                {
                    case 1:
                        // 1 bomb = blue
                        number.color = Color.blue * 0.8f;
                        break;

                    case 2:
                        // 2 bomb = green
                        number.color = Color.green * 0.8f;
                        break;

                    case 3:
                        // 3 bomb = red
                        number.color = Color.red * 0.8f;
                        break;

                    case 4:
                        // 4 bomb = purple
                        number.color = new Color(128, 0, 128) * 0.8f;
                        break;

                    case 5:
                        // 5 bomb = black
                        number.color = Color.black * 0.8f;
                        break;

                    case 6:
                        // 6 bomb = maroon
                        number.color = new Color(128, 0, 0) * 0.8f;
                        break;

                    case 7:
                        // 7 bomb = gray
                        number.color = Color.gray * 0.8f;
                        break;

                    case 8:
                        // 8 bomb = turquoise
                        number.color = new Color(64, 224, 208) * 0.8f;
                        break;

                }
            }
            else
            {
                transform.Find("Canvas").GetComponent<DestroyGameObject>().DestroyObject();
            }
            transform.Find("Bomb").GetComponent<DestroyGameObject>().DestroyObject();
        }

    }

}



