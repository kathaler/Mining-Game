using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wall;
    public Player player;
    public int viewportWidth = 15;


    private void Start()
    {
        Vector2 pos = player.transform.position;
        for(int x = (int)pos.x + 2; x < viewportWidth + (int)pos.x + 2; x++)
        {
            Instantiate(wall, new Vector3(x, -1, 0), Quaternion.identity);
        }

        for (int x = (int)pos.x - 2; x > (int)pos.x - 2 - viewportWidth; x--)
        {
            Instantiate(wall, new Vector3(x, -1, 0), Quaternion.identity);
        }

        for (int y = -10; y < -1; y++)
        {
            Instantiate(wall, new Vector3(-2, y, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(2, y, 0), Quaternion.identity);

        }
    }
    private void Update()
    {
        
    }

}
