﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl
{
    public GameObject grid;
    public int gridCells = 0;
    public int openTileAmount = 0;

    public Tile[,] Tiles;

    protected int height;
    protected int width;

    public GameObject[,] gridObjects;

    public void CreateGrid(int width, int height, int bombs, GameObject GridGameObjects)
    {
        grid = GameObject.Find("Grid");

        this.height = height;
        this.width = width;

        int gridCells = width * height;
        int currentGridCellID = 0;
        gridObjects = new GameObject[width, height];
        List<int> bombPos = new List<int>();
        Tiles = new Tile[width, height];

        for (int i = 0; i < bombs; i++)
        {
            int tempPos = Random.Range(0, gridCells);

            if (bombPos.Count == 0)
            {
                bombPos.Add(tempPos);
                Debug.Log("First BombPos Set!");
                Debug.Log($"BombPos Set! - Pos : {tempPos}");
            }
            else
            {
                bool isConflicting = false;
                do
                {
                    foreach (int pos in bombPos)
                    {
                        if (tempPos == pos)
                        {
                            Debug.Log("Rerolling BombPos - Conflicting positions!");
                            tempPos = Random.Range(0, gridCells);
                            isConflicting = true;
                            break;
                        }
                        else
                        {
                            isConflicting = false;
                        }
                    }

                } while (isConflicting);
                Debug.Log($"BombPos Set! - Pos : {tempPos}");
                bombPos.Add(tempPos);
            }

        }

        Debug.Log($"All BombPositions have been set!");




        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                bool isBomb = false;

                foreach (int id in bombPos)
                {
                    if (currentGridCellID == id)
                    {
                        isBomb = true;
                    }
                }

                Tiles[i, j] = new Tile(currentGridCellID, i, j, isBomb, 0, false, false);

                currentGridCellID += 1;
            }
        }

        foreach (int id in bombPos)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if ( Tiles[i,j].id == id )
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            switch (k)
                            {
                                case 0:
                                    try
                                    {
                                        Tiles[i - 1, j - 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i-1}:{j-1}");
                                    }
                                    break;

                                case 1:
                                    try
                                    {
                                        Tiles[i, j - 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i}:{j - 1}");
                                    }
                                    break;

                                case 2:
                                    try
                                    {
                                        Tiles[i + 1, j - 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i + 1}:{j - 1}");
                                    }
                                    break;

                                case 3:
                                    try
                                    {
                                        Tiles[i - 1, j].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i - 1}:{j}");
                                    }
                                    break;

                                case 4:
                                    try
                                    {
                                        Tiles[i + 1, j].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i + 1}:{j}");
                                    }
                                    break;

                                case 5:
                                    try
                                    {
                                        Tiles[i - 1, j + 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i - 1}:{j + 1}");
                                    }
                                    break;

                                case 6:
                                    try
                                    {
                                        Tiles[i, j + 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i}:{j + 1}");
                                    }
                                    break;

                                case 7:
                                    try
                                    {
                                        Tiles[i + 1, j + 1].neighboringBombs += 1;
                                    }
                                    catch
                                    {
                                        Debug.LogWarning($"Can't add neighboringBombsCount - Tile dose not exist at {i + 1}:{j + 1}");
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /*
        foreach (var item in Tiles)
        {
            Debug.Log($"{item.id}\n{item.x}\n{item.y}\n{item.isBomb}\n{item.neighboringBombs}\n{item.isOpen}\n{item.isFlaged}");
        }
        */

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject GO = Object.Instantiate(GridGameObjects,new Vector3( i + ( 1 / 4 ), 0, j + ( 1 /  4 ) ), Quaternion.Euler(0,0,0));
                GO.name = ("GridObject " +  i + ":" + j);
                GO.transform.parent = grid.transform;

                GO.GetComponent<GridObjectControler>().myTile = Tiles[i, j];
                GO.GetComponent<GridObjectControler>().UpdateVisual();
                gridObjects[i, j] = GO;

                Debug.Log($"Tile Created!\nGameObject : {GO.name}\nID : {Tiles[i, j].id}\nX Pos : {Tiles[i, j].x}\nY Pos : {Tiles[i, j].y}\nisBomb : {Tiles[i, j].isBomb}\nNeighboringBombs : {Tiles[i, j].neighboringBombs}\nisOpen : {Tiles[i, j].isOpen}\nisFlaged : {Tiles[i, j].isFlaged}");

            }
        }
        
    }


    public void clearShells()
    {
        int items = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                try
                {
                    gridObjects[i, j].GetComponent<GridObjectControler>().myTile.isOpen = true;
                    gridObjects[i, j].transform.Find("Shell").GetComponent<DestroyGameObject>().DestroyObject();
                    items++;
                }
                catch
                {

                }
            }
        }
         Debug.Log($"{items} items has been cleard!");
     }
}
