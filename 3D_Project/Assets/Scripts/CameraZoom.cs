using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{

    public Transform target;
    public float zoomDistance = 0.4f;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float smoothTime = 2f;

    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;

    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // double click variable
    float lastClick = 0;
    float catchTime = .25f;

    // Vector3.Lerp Variable
    public float speedLerp = 1.0f;

    private Transform startMarker;
    private Transform endMarker;
    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start()
    {
        Rigidbody rgdbody = GetComponent<Rigidbody>();

        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;

        // Make the rigid body not change rotation
        if (rgdbody)
        {
            rgdbody.freezeRotation = true;
        }

        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vector3.Lerp
            startMarker = transform;
            endMarker = target;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            // end Vector3.Lerp

            // double click check
            if (Time.time - lastClick < catchTime)
            { // if double click
                if (Physics.Raycast(ray, out hit))
                { // if click on object
                    target = hit.transform; // camera will rotation around new target
                    distance = zoomDistance; // camera will zoom clicked object

                    // Vector3.Lerp
                    float distCovered = (Time.time - startTime) * speedLerp;
                    float fracJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
                    // end Vector.Lerp

                }
                else
                {
                    distance = 5f; // if clicked on blank space, it will zoom out
                }
                print("its double click");

            }
            else
            {
                print("its normal click");
            }
            lastClick = Time.time;
        }
    }


    // Camera Rotation / Position / Zoom
    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }

            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;

            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

            //            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 1, distanceMin, distanceMax);
            Camera.main.orthographicSize = distance; // zoom with orthographic camera

            //            RaycastHit hit;
            //            if (Physics.Linecast(target.position, transform.position, out hit))
            //            {
            //                distance -= hit.distance;
            //            }

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}