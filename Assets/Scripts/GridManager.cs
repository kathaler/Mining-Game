using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Chunk _chunk;
    [SerializeField] private Camera _cam;
    private int cam_x;
    private int cam_y;

    private void Start()
    {
        cam_x = (int)(_cam.transform.position.x);
        cam_y = (int)(_cam.transform.position.y);
        ArrayList start_chunk = _chunk.GenerateChunk(_width, _height, _cam);
    }

    private void Update()
    {
        //int new_cam_x = (int)(_cam.transform.position.x);
        //int new_cam_y = (int)(_cam.transform.position.y);

        //if(new_cam_x != cam_x)
        //{
        //    int x_right = new_cam_x + (int)(_cam.aspect * _cam.orthographicSize) + 2;
        //    int x_left = new_cam_x - (int)(_cam.aspect * _cam.orthographicSize) - 2;
        //    int x = 0, x_kill = 0;
        //    if (new_cam_x > cam_x)
        //    {
        //        x = x_right;
        //        x_kill = x_left;
        //    }
        //    else if (new_cam_x < cam_x)
        //    {
        //        x = x_left;
        //        x_kill = x_right;
        //    }
        //    for (int j = 0; j < _height + 2; j++)
        //    {
        //        int y = j - (int)_cam.orthographicSize;
        //        var spawnedTile = Instantiate(_tilePrefab,
        //            new Vector3(x, y), Quaternion.identity);
        //        spawnedTile.name = $"Tile {x} {y}";
        //        GameObject go = GameObject.Find($"Tile {x_kill} {y}");
        //        if(go)
        //        {
        //            Destroy(go.gameObject);
        //        }
        //    }
        //    cam_x = new_cam_x;
        //}
        //else if (new_cam_y != cam_y)
        //{
        //    int y_right = new_cam_y + (int)_cam.orthographicSize;
        //    int y_left = new_cam_y - (int)_cam.orthographicSize;
        //    int y = 0, y_kill = 0;
        //    if (new_cam_y > cam_y)
        //    {
        //        y = y_right;
        //        y_kill = y_left;
        //    }
        //    else if (new_cam_y < cam_y)
        //    {
        //        y = y_left;
        //        y_kill = y_right;
        //    }
        //    for (int i = 0; i < _width + 5; i++)
        //    {
        //        int x = i - (int)(_cam.aspect * _cam.orthographicSize) - 2;
        //        var spawnedTile = Instantiate(_tilePrefab,
        //            new Vector3(x, y), Quaternion.identity);
        //        spawnedTile.name = $"Tile {x} {y}";
        //        GameObject go = GameObject.Find($"Tile {x} {y_kill}");
        //        if (go)
        //        {
        //            Destroy(go.gameObject);
        //        }
        //    }
        //    cam_y = new_cam_y;
        //}
    }

    //void GenerateGrid()
    //{
    //    ArrayList chunk = Chunk.GenerateChunk(_width, _height, _cam, _tilePrefab);
    //}
}
