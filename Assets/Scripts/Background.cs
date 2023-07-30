using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Player player;
    public GameObject background;
    public int viewportWidth = 2;
    public int viewportHeight = 2;
    public int tileSize = 2; 

    private Vector3 position;
    private List<GameObject> activePositions;
    private Vector3 lastPos;
    private int viewportBottom;
    private int viewportLeft;

    private List<GameObject> recycledTiles;

    void Start()
    {
        position = player.transform.position;
        lastPos = position;
        activePositions = new List<GameObject>();
        recycledTiles = new List<GameObject>();
        InstantiateBackground();
    }

    void Update()
    {
        position = player.transform.position;

        // moved right
        if((int)(position.x - lastPos.x) >= tileSize) {
            Debug.Log("RIGHT");
            lastPos.x = position.x;
            UpdateBackground(0);
        }
        // moved left
        if((int)(lastPos.x - position.x) == tileSize) {
            Debug.Log("LEFT");
            lastPos.x = position.x;
            UpdateBackground(1);
        }
        if((int)(position.y - lastPos.y) == tileSize) {
            Debug.Log("UP");
            lastPos.y = position.y;
            UpdateBackground(2);
        }
        // moved left
        if((int)(lastPos.y - position.y) == tileSize) {
            Debug.Log("DOWN");
            lastPos.y = position.y;
            UpdateBackground(3);
        }
    }

    void InstantiateBackground() {
        viewportBottom = ((int)position.y - viewportHeight / 2) * tileSize;
        viewportLeft = ((int)position.x - viewportWidth / 2) * tileSize;

        for (int x = viewportLeft; x < viewportLeft + (viewportWidth * tileSize); x = x + 2)
        {
            for (int y = viewportBottom; y < viewportBottom + (viewportHeight * tileSize); y = y + 2)
            {
                GameObject b = Instantiate(background, new Vector3(x, y, 100), Quaternion.identity);
                b.transform.SetParent(transform);
                activePositions.Add(b);
            }
        }
    }

    void UpdateBackground(int direction)
    {
        switch (direction)
        {
            case 0:
                // RIGHT
                foreach(GameObject g in activePositions) {
                    if(g.transform.position.x == viewportLeft) {
                        g.transform.position = new Vector3((int)(viewportLeft + (viewportWidth * tileSize)), g.transform.position.y, 100);
                    }
                }
                viewportLeft = viewportLeft + tileSize;
                break;
            case 1:
                // LEFT
                int viewportRight = (int)(viewportLeft + (viewportWidth * tileSize));
                foreach(GameObject g in activePositions) {
                    if(g.transform.position.x == viewportRight) {
                        g.transform.position = new Vector3(viewportLeft, g.transform.position.y, 100);
                    }
                }
                viewportLeft = viewportLeft - tileSize;
                break;
            case 2:
                // UP
                foreach(GameObject g in activePositions) {
                    if(g.transform.position.y == viewportBottom) {
                        g.transform.position = new Vector3(g.transform.position.x,  (int)(viewportBottom + (viewportHeight * tileSize)), 100);
                    }
                }
                viewportBottom = viewportBottom + tileSize;
                break;
            case 3:
                // DOWN
                int viewportTop = (int)(viewportBottom + (viewportHeight * tileSize));
                foreach(GameObject g in activePositions) {
                    if(g.transform.position.y == viewportTop) {
                        g.transform.position = new Vector3(g.transform.position.x,  viewportBottom, 100);
                    }
                }
                viewportBottom = viewportBottom - tileSize;
                break;
        }
    }
}
