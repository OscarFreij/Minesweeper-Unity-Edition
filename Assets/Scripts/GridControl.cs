using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl
{
    public static int gridCells = 0;
    public static void CreateGrid(int width, int height, int bombs)
    {
        int gridCells = width * height;
        int currentGridCellID = 0;
        int[,] grid = new int[height, width];

        while (currentGridCellID < gridCells)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = currentGridCellID;
                    currentGridCellID++;
                }
            }
        }


        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log($"Height : {i}\nWidth : {j}\nID : {grid[i, j]}");


            }
        }

        for (int i = 0; i < bombs; i++)
        {
            Random.Range();
        }
    }

    public static void BoxGetPos(int id)
    {

    }
}
