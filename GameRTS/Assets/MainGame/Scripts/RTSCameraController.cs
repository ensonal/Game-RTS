using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RTSCameraController : MonoBehaviourPunCallbacks
{
    float speed = 6f;
    float zoomSpeed = 1000.0f;
    float rotateSpeed = 1.0f;

    float maxHeight = 40f;
    float minHeight = -4f;

    Vector2 p1;
    Vector2 p2;

    private PhotonView pw;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
        if (!pw.IsMine)
        {
            GetComponent<RTSCameraController>().enabled = false;
            GetComponentInChildren<Camera>().enabled = false;
        }
    }

    private void Update()
    {
        
            if (Input.GetKey(KeyCode.LeftShift))
            {

                speed = 60f;
                zoomSpeed = 2000.0f;

            }
            else
            {
                speed = 35f;
                zoomSpeed = 1000.0f;
            }

            float hsp = speed * Input.GetAxis("Horizontal");
            float vsp = speed * Input.GetAxis("Vertical");

            float scrollSp = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");


            if ((transform.position.y >= maxHeight) && (scrollSp > 0))
            {
                scrollSp = 0;
            }
            else if((transform.position.y <= minHeight) && (scrollSp < 0))
            {
                scrollSp = 0;
            }

            if ((transform.position.y + scrollSp) > maxHeight)
            {
                scrollSp = maxHeight - transform.position.y;
            }
            else if ((transform.position.y + scrollSp) < minHeight)
            {
                scrollSp = minHeight - transform.position.y;
            }


            Vector3 verticalMove = new Vector3(0, scrollSp, 0);
            Vector3 lateralMove = hsp * transform.right;
            Vector3 forwardMove = transform.forward;
            forwardMove.y = 0;
            forwardMove.Normalize();
            forwardMove *= vsp;

            Vector3 move = verticalMove + lateralMove + forwardMove;

            transform.position += move * Time.deltaTime;

            updateCameraRotation();
        
    }

    void updateCameraRotation()
    {
        if(Input.GetMouseButtonDown(2)) {
            p1 = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));

            p1 = p2;

        }
    }

}

//{
//    public float panSpeed = 5f;
//    public float panBorderThickness = 10f;

//    public float scrollSpeed = 5f;
//    public float minY = 5f;
//    public float maxY = 120f;

//    // Update is called once per frame
//    void Update()
//    {
//        HandleCameraMovement();
//        HandleZoom();
//    }

//    void HandleCameraMovement()
//    {
//        // Pan movement
//        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
//        {
//            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
//        {
//            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
//        {
//            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
//        {
//            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
//        }
//    }

//    void HandleZoom()
//    {
//        // Zoom in/out
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        Vector3 pos = transform.position;
//        pos.y -= scroll * scrollSpeed * 1000 * Time.deltaTime;
//        pos.y = Mathf.Clamp(pos.y, minY, maxY);
//        transform.position = pos;
//    }
//}
