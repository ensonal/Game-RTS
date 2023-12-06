using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleZoom();
    }

    void HandleCameraMovement()
    {
        // Pan movement
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
    }

    void HandleZoom()
    {
        // Zoom in/out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * 1000 * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
