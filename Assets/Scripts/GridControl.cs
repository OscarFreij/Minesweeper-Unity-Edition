using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl
{
    public GameObject grid;
    public int gridCells = 0;

    public Tile[,] Tiles;

    protected int height;
    protected int width;

    protected GameObject[,] gridObjects;

    public void CreateGrid(int width, int height, int bombs, GameObject GridGameObjects)
    {
        grid = GameObject.Find("Grid");

        this.height = height;
        this.width = width;

        int gridCells = width * height;
        int currentGridCellID = 0;
        gridObjects = new GameObject[height, width];
        int[] bombPos = new int[bombs];
        Tiles = new Tile[height, width];

        for (int i = 0; i < bombPos.Length; i++)
        {
            bombPos[i] = Random.Range(0,gridCells);
        }


        while (currentGridCellID < gridCells)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currentGridCellID++;
                    bool isBomb = false;

                    foreach (var item in bombPos)
                    {
                        
                        if (currentGridCellID == item)
                        {
                            isBomb = true;    
                            break;
                        }
                        else
                        {
                            isBomb = false;
                        }
                    } 



                    Tiles[i, j] = new Tile(currentGridCellID,i,j,isBomb,0,false,false);

                    GameObject OB = Object.Instantiate(GridGameObjects, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    OB.name = ("GridObject " + currentGridCellID + ":" + i + ":" + j);
                    OB.transform.parent = grid.transform;

                    gridObjects[i, j] = OB;

                    OB.GetComponent<GridObjectControler>().myTile = Tiles[i, j];

                    Debug.Log($"Tile Created!\nGameObject : {OB.name}\nID : {Tiles[i, j].id}\nX Pos : {Tiles[i, j].x}\nY Pos : {Tiles[i, j].y}\nisBomb : {Tiles[i, j].isBomb}\nNeighboringBombs : {Tiles[i, j].neighboringBombs}\nisOpen : {Tiles[i, j].isOpen}\nisFlaged : {Tiles[i, j].isFlaged}");

                }
            }
        }

        int neighboringBombs = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                for (int l = 0; l < 8; l++)
                {
                    int xPos = 0;
                    int yPos = 0;
                    switch (l)
                    {
                        case 0:
                            xPos = i - 1;
                            yPos = j - 1;
                            break;

                        case 1:
                            xPos = i - 1;
                            yPos = j;
                            break;

                        case 2:
                            xPos = i - 1;
                            yPos = j + 1;
                            break;

                        case 3:
                            xPos = i;
                            yPos = j - 1;
                            break;

                        case 4:
                            xPos = i;
                            yPos = j + 1;
                            break;

                        case 5:
                            xPos = i + 1;
                            yPos = j - 1;
                            break;

                        case 6:
                            xPos = i + 1;
                            yPos = j;
                            break;

                        case 7:
                            xPos = i + 1;
                            yPos = j + 1;
                            break;
                    }

                    try
                    {
                        if (Tiles[xPos, yPos].isBomb == true)
                        {
                            neighboringBombs++;
                        }
                    }
                    catch
                    {
                        Debug.LogWarning($"Can't check Tile at : {xPos}:{yPos}\nTile doesn't exist!");
                    }
                }
                Tiles[i, j].neighboringBombs = neighboringBombs;
                //Debug.Log($"ID : {Tiles[i,j].id}\nneighboringBombs : {Tiles[i, j].neighboringBombs}");
                neighboringBombs = 0;
            }
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                gridObjects[i, j].GetComponent<GridObjectControler>().myTile = Tiles[i, j];
                gridObjects[i, j].GetComponent<GridObjectControler>().UpdateVisual();
            }
        }
    }


    public void checkNeighboringTiles(int xPos, int yPos)
    {
        for (int i = 0; i < 4; i++)
        {
            int LxPos = 0;
            int LyPos = 0;
            switch (i)
            {
                case 0:
                    LxPos = xPos;
                    LyPos = yPos - 1;
                    break;

                case 1:
                    LxPos = xPos - 1;
                    LyPos = yPos;
                    break;

                case 2:
                    LxPos = xPos + 1;
                    LyPos = yPos;
                    break;

                case 3:
                    LxPos = xPos;
                    LyPos = yPos + 1;
                    break;
            }

            try
            {
                if (
                    !gridObjects[LxPos, LyPos].GetComponent<GridObjectControler>().myTile.isBomb &&
                    !gridObjects[LxPos, LyPos].GetComponent<GridObjectControler>().myTile.isOpen &&
                    !gridObjects[LxPos, LyPos].GetComponent<GridObjectControler>().myTile.isFlaged
                    )
                {
                    if ( gridObjects[LxPos, LyPos].GetComponent<GridObjectControler>().myTile.neighboringBombs == 0 )
                    {
                        gridObjects[LxPos, LyPos].transform.Find("Shell").GetComponent<GridObjectShellControl>().open(true);
                    }
                    else
                    {
                        gridObjects[LxPos, LyPos].transform.Find("Shell").GetComponent<GridObjectShellControl>().open(false);
                    }
                    
                }
            }
            catch
            {
                Debug.LogWarning($"Can't open Tile at : {LxPos}:{LyPos}\nTile doesn't exist!");
            }
        }
    }
}
