using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPieces : MonoBehaviour
{

    BoardCreate3D boardCreate3D;

    // Pieces
    public GameObject wPawn;
    public GameObject bPawn;
    public GameObject wKnight;
    public GameObject bKnight;
    public GameObject wBishop;
    public GameObject bBishop;
    public GameObject wRook;
    public GameObject bRook;
    public GameObject wQueen;
    public GameObject bQueen;
    public GameObject wKing;
    public GameObject bKing;

    // xyz 
    int x_tile_count;
    int y_tile_count;
    int z_tile_count;

    // board
    GameObject[,,] board;

    // to subtract the diameter of the cells
    Vector3 diameter = new Vector3(0, 0.2f, 0);

    // Start is called before the first frame update
    void Start()
    {
        boardCreate3D = gameObject.GetComponent<BoardCreate3D>();

        x_tile_count = boardCreate3D.x_tile_count;
        y_tile_count = boardCreate3D.y_tile_count;
        z_tile_count = boardCreate3D.z_tile_count;
        board = boardCreate3D.board;

        PlacePieces();
    }

    void Update()
    {

    }

    void PlacePieces()
    {
        // Pawns
        for (int i = 0; i < x_tile_count; i++)
        {
            for (int k = 0; k < z_tile_count; k++)
            {
                Instantiate(wPawn, board[i, 1, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                Instantiate(bPawn, board[i, y_tile_count - 2, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
            }
               
        }

        // Kings
        Instantiate(wKing, board[4, 0, 4].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(bKing, board[4, y_tile_count-1, 4].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;

        // Queens
        for (int i = 3; i <= 5; i++)
        {
            for (int k = 3; k <= 5; k++)
            {
                if (i == 3 || i == 5 || k == 3 || k == 5)
                {
                    Instantiate(wQueen, board[i, 0, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                    Instantiate(bQueen, board[i, y_tile_count - 1, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                }              
            }
        }

        // Bishops
        for (int i = 2; i <= 6; i++)
        {
            for (int k = 2; k <= 6; k++)
            {
                if (i == 2 || i == 6 || k == 2 || k == 6)
                {
                    Instantiate(wBishop, board[i, 0, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                    Instantiate(bBishop, board[i, y_tile_count - 1, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                }
            }
        }

        // Knights
        for (int i = 1; i <= 7; i++)
        {
            for (int k = 1; k <= 7; k++)
            {
                if (i == 1 || i == 7 || k == 1 || k == 7)
                {
                    Instantiate(wKnight, board[i, 0, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                    Instantiate(bKnight, board[i, y_tile_count - 1, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                }
            }
        }

        // Rooks
        for (int i = 0; i <= 8; i++)
        {
            for (int k = 0; k <= 8; k++)
            {
                if (i == 0 || i == 8 || k == 0 || k == 8)
                {
                    Instantiate(wRook, board[i, 0, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                    Instantiate(bRook, board[i, y_tile_count - 1, k].transform.position - diameter, Quaternion.identity).transform.parent = gameObject.transform;
                }
            }
        }

    }
}
