using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public Board board;

    public void Setup(int x_, int y_, Board board_)
    {
        x = x_;
        y = y_;
        board = board_;
    }

    public void OnMouseDown()
    {
        board.TileDown(this);
    }

    public void OnMouseEnter()
    {
        board.TileOver(this);
    }

    public void OnMouseUp()
    {
        board.TileUp(this);
    }
}
