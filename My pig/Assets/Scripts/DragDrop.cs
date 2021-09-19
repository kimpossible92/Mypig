using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool isDragging;
    public Vector3 mouse_offset;
    private float mouse_z_coord;
    private float mouse_y_coord;
    private float mouse_y_offset = 1;
    private MeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        //Debug.Log(rend);
    }
    private void OnMouseDown()
    {
        transform.position += new Vector3(0, 1, 0);
        mouse_z_coord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouse_y_coord = Mathf.Clamp(Camera.main.WorldToScreenPoint(transform.position).y, 0, mouse_y_offset);

        mouse_offset = transform.position - GetMouseAsWorldPoint();
        print(mouse_offset);
        isDragging = true;
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        // Screenpoint coordinates of the mouse.
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of dragged gameobject.
        mousePoint.z = mouse_z_coord;

        mousePoint.y = mouse_y_coord;
        print(mousePoint);//
        // Convert to world space.
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void onMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mouse_offset;
    }

    private void OnMouseUp()
    {
        // Remove hardcode value.
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseAsWorldPoint() + mouse_offset;
        }
    }
}
