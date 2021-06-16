// Taken from https://www.patreon.com/posts/unity-3d-drag-22917454

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DragAndDropPiece : MonoBehaviour

{
    private Vector3 mOffset;
    private float mZCoord;

    // scripts
    Outline outline;
    BoardBehavior3D bb;
    BoardCreate3D bc;
    AddPieces ap;

    // whether mouse is being pressed or not
    // (to avoid highlighting more than one piece at a time)
    bool mouseDown = false;

    // position of the piece
    // (in case we need to return it when dropped out of bounds)
    Vector3 objectPos;

    // the last highlighted tile's coordinates (to turn it back to non-highlighted) 
    int lastTile_x = -1; int lastTile_y = -1; int lastTile_z = -1;

    void Start()
    {
        // get scripts
        outline = this.GetComponent<Outline>();
        bb = this.GetComponentInParent<BoardBehavior3D>();
        bc = this.GetComponentInParent<BoardCreate3D>();
        ap = this.GetComponentInParent<AddPieces>();
    }

    void OnMouseEnter()
    {
        if (!bb.isMoving)
        {
            outline.color = 0;
            outline.enabled = true;
        }
    }

    void OnMouseExit()
    {
        if (!mouseDown)
        {
            outline.enabled = false;

        }
    }

    void OnMouseUp()
    {
        // when mouse is lifted, drop the piece and adjust the parameters
        DropPiece();

        mouseDown = false;
        bb.isMoving = false;
        outline.color = 0;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        // store gameObject pos
        objectPos = gameObject.transform.position;

        // set the parameters
        mouseDown = true;
        bb.isMoving = true;
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;

        // reset color
        outline.color = 1;

        HighlightTile();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }



    void DropPiece() {

        int[] pos = GetPieceCoordinates();
        int x_pos = pos[0];
        int y_pos = pos[1];
        int z_pos = pos[2];

        if (0 <= x_pos && x_pos < bc.x_tile_count &&
            0 <= y_pos && x_pos < bc.y_tile_count &&
            0 <= z_pos && x_pos < bc.z_tile_count)
        {
            gameObject.transform.position = bc.board[x_pos, y_pos, z_pos].transform.position - ap.diameter;
        }
        else
        {
            gameObject.transform.position = objectPos;
        }
    }

    void HighlightTile()
    {
        int[] pos = GetPieceCoordinates();
        int x_pos = pos[0];
        int y_pos = pos[1];
        int z_pos = pos[2];

        if (
           !(x_pos == lastTile_x && y_pos == lastTile_y && z_pos == lastTile_z) &&
           0 <= x_pos   &&  x_pos < bc.x_tile_count  &&
           0 <= y_pos   &&  y_pos < bc.y_tile_count  &&
           0 <= z_pos   &&  z_pos < bc.z_tile_count
           )
        {
            ChangeTileColor(x_pos, y_pos, z_pos);
        }
    }

    int[] GetPieceCoordinates()
    {
        int x_pos = Mathf.RoundToInt(this.transform.position.x / bb.x_increments);
        int y_pos = Mathf.RoundToInt(this.transform.position.y / bb.y_increments);
        int z_pos = Mathf.RoundToInt(this.transform.position.z / bb.z_increments);

        return new int[] {x_pos,y_pos,z_pos};
    }

    void ChangeTileColor(int x_pos, int y_pos, int z_pos)
    {
        bc.board[x_pos, y_pos, z_pos].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        if (lastTile_x >= 0)
        {
            bc.board[lastTile_x, lastTile_y, lastTile_z].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        }
        lastTile_x = x_pos; lastTile_y = y_pos; lastTile_z = z_pos;
    }


}