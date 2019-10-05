using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool isBomb { get; set; }
    public int neighboringBombs { get; set; }
    public bool isOpen { get; set; }

    public Tile(int id, int posX, int posY, bool isBomb, int neighboringBombs, bool isOpen)
    {
        this.id = id;
        this.x = posX;
        this.y = posY;
        this.isBomb = isBomb;
        this.neighboringBombs = neighboringBombs;
        this.isOpen = isOpen;
    }
}
