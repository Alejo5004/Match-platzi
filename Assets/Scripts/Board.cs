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

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
        PositionCamera();
        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectdPiece = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
                var ins = Instantiate(selectdPiece, new Vector3(x, y, -5), Quaternion.identity); // Muestra los square vacios
                ins.transform.parent = transform;
                ins.GetComponent<Piece>()?.Setup(x, y, this);
            }
        }
    }

    private void PositionCamera() // Posiscion de la camara
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;

        Camera.main.transform.position = new Vector3(newPosX - 0.5f, newPosY - 0.5f + cameraVerticalOffset, -10); // Centra la camara

        float horizontal = width + 1;
        float vertical = height/2 + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffset : vertical + cameraSizeOffset;
    }
    private void SetupBoard() // Crea espacios en la cuadricula
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var ins = Instantiate(tileObject, new Vector3(x,y, -5), Quaternion.identity); // Muestra los square vacios
                ins.transform.parent = transform;
                ins.GetComponent<Tile>()?.Setup(x, y, this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
