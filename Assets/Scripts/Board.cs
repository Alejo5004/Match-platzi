using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tileObject;
    public float cameraSizeOffset;
    public float cameraVerticalOffset;
    public GameObject[] availablePieces;

    Tile[,] Tiles;
    Piece[,] Pieces;

    Tile startTile;
    Tile endTile;

    Piece startPiece;
    Piece endPiece;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[width, height];
        Pieces = new Piece[width, height];

        PositionCamera();
        SetupBoard();
    }
    private void PositionCamera() // Posiscion de la camara
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;

        Camera.main.transform.position = new Vector3(newPosX - 0.5f, newPosY - 0.5f + cameraVerticalOffset, -10); // Centra la camara

        float horizontal = width + 1;
        float vertical = height / 2 + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffset : vertical + cameraSizeOffset;
    }

    public void SetupBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SetupTiles(x, y);
                SetupPieces(x, y);
            }
        }
    }

    private void SetupTiles(int x, int y) // Muestra el fondo vacio
    {
        var ins = Instantiate(tileObject, new Vector3(x, y, -5), Quaternion.identity);
        ins.transform.parent = transform;
        Tiles[x, y] = ins.GetComponent<Tile>();
        Tiles[x, y]?.Setup(x, y, this);
    }

    private void SetupPieces(int x, int y) // Muestra los animales
    {
        var selectdPiece = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
        var ins = Instantiate(selectdPiece, new Vector3(x, y, -5), Quaternion.identity);
        ins.transform.parent = transform;
        Pieces[x, y] = ins.GetComponent<Piece>();
        Pieces[x, y]?.Setup(x, y, this);
    }

    public void TileDown(Tile tile_)
    {
        startTile = tile_;
        Pieces[startTile.x, startTile.y].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }

    public void TileUp(Tile tile_)
    {
        if (startTile != null && endTile != null && IsCloseTo(startTile, endTile)){
            SwapTiles();
        }

        Pieces[startTile.x, startTile.y].transform.localScale = new Vector3(1f, 1f, 1f);

        startTile = null;
        endTile = null;
    }

    private void SwapTiles() // Movimiento de la pieza
    {
        var StartPiece = Pieces[startTile.x, startTile.y];
        var EndPiece = Pieces[endTile.x, endTile.y];

        StartPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x, startTile.y);

        StartPiece.transform.localScale = new Vector3(1f, 1f, 1f);

        Pieces[startTile.x, startTile.y] = EndPiece;
        Pieces[endTile.x, endTile.y] = StartPiece;
    }

    public bool IsCloseTo(Tile start, Tile end)
    // public bool IsCloseTo(Piece start, Piece end)
    {
        if(Math.Abs(start.x - end.x) == 1 && start.y == end.y){
            return true;
        }

        if(Math.Abs(start.y - end.y) == 1 && start.x == end.x){
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
