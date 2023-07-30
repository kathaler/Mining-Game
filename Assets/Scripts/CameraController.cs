using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    public float zoomSpeed = 2f;
    public float upperBound = 10f;
    public float lowerBound = 1f;
    private Camera mainCamera;


    private Text stone;
    private Text iron;
    private Text gold;

    private Text text;


    void Start() {
        // FindObjectOfType<AudioManager>().Play("Ambience1");
        // FindObjectOfType<AudioManager>().Play("testmusic");
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update() {
        HandleZoom();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

    }

    private void HandleZoom() {
        Camera cam = GetComponent<Camera>();
        if(cam.orthographicSize >= lowerBound && cam.orthographicSize <= upperBound) {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        if(cam.orthographicSize < lowerBound) cam.orthographicSize = lowerBound;
        if(cam.orthographicSize > upperBound) cam.orthographicSize = upperBound;
    }
}
