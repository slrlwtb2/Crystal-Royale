using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target; // The target we are following

    [SerializeField]
    private float distance = 30.0f; // The distance in the x-z plane to the target

    [SerializeField]
    private float height = 45.0f; // the height we want the camera to be above the target
    [SerializeField]
    private float heightDamping;
    private Camera mainCam;

    public float transitionDuration = 2.5f;

    void Start()
    {
        mainCam = GetComponent<Camera>();
    }
    void OnGUI()
    {
        GUI.Button(new Rect(Screen.width /4 , Screen.height / 4, Screen.width / 2, Screen.height / 2), "Dead Zone");
    }
    void LateUpdate()
    {
        if (!target)
            return;

        Vector3 viewPos = mainCam.WorldToViewportPoint(target.position); // Assign the coordinates of the target to viewPos in Viewport space
        if (viewPos.x >= 0.75F || viewPos.x <= 0.25F || viewPos.y <= 0.25F || viewPos.y >= 0.75F) // Check if the target is beyond the boundaries defined for the dead zone
            StartCoroutine(Transition()); // If the target is on or outside the boundary, move the camera to recenter on it
    }
    IEnumerator Transition()
    {

        Vector3 targetPos = Vector3.zero;
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        var wantedHeight = target.position.y + height;
        var currentHeight = transform.position.y;

        while (t < 0.50f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration) ; // set the duration of the camera lerp in seconds
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime); //Lerp currentHeight with the camera's desired height over time
            targetPos = target.position; // Set the targetPos of the camera to the xyz of the character
            targetPos -= Vector3.forward * distance; // Add the distance offset to the character in the targetPos's forward facing (Positive Z)
            targetPos = new Vector3(targetPos.x, currentHeight, targetPos.z); // Replace the Y of the targetPos with the value in currentHeight

            transform.position = Vector3.Lerp(startingPos, targetPos, t); // Vertex.Lerp the camera from startingPos -> targetPos over time
            yield return 0;
        }
    }
}
