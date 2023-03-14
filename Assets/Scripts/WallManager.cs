using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Wall Manager
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
            for (int y = -10; y <= -1; y++)
            {
                GameObject newWall = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity);
                newWall.name = $"Wall {x} {y}";

                newWall.transform.SetParent(this.transform);
            }
        }

        for (int x = (int)pos.x - 2; x > (int)pos.x - 2 - viewportWidth; x--)
        {
            for (int y = -10; y <= -1; y++)
            {
                GameObject newWall = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity);
                newWall.name = $"Wall {x} {y}";

                newWall.transform.SetParent(this.transform);
            }
        }
    }
    private void Update()
    {
        
    }

}
