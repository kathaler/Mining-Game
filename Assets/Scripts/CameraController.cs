using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    public GameObject background;

    private Text stone;
    private Text iron;
    private Text gold;

    private Text text;


    void Start() {
 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        background.transform.position = new Vector3(transform.position.x, transform.position.y);
    }
}
