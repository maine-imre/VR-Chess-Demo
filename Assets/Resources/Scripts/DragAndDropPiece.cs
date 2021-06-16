// Taken from https://www.patreon.com/posts/unity-3d-drag-22917454

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DragAndDropPiece : MonoBehaviour

{
    private Vector3 mOffset;
    private float mZCoord;

    Outline outline;
    BoardBehavior3D bb;
    BoardCreate3D bc;

    bool mouseDown = false;

    Vector3 objectPos;

    void Start()
    {
        // get scripts
        outline = this.GetComponent<Outline>();
        bb = this.GetComponentInParent<BoardBehavior3D>();
        bc = this.GetComponentInParent<BoardCreate3D>();
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

        mouseDown = true;
        bb.isMoving = true;
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
        outline.color = 1;
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
        int x_pos = Mathf.RoundToInt(this.transform.position.x / bb.x_increments);
        int y_pos = Mathf.RoundToInt(this.transform.position.y / bb.y_increments);
        int z_pos = Mathf.RoundToInt(this.transform.position.z / bb.z_increments);

        Debug.Log(x_pos + " " +  y_pos + " " + z_pos);

        if (0 <= x_pos && x_pos < bc.x_tile_count &&
            0 <= y_pos && x_pos < bc.y_tile_count &&
            0 <= z_pos && x_pos < bc.z_tile_count)
        {
            gameObject.transform.position = bc.board[x_pos, y_pos, z_pos].transform.position;
        }
        else
        {
            gameObject.transform.position = objectPos;
        }

    }


}