using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool touch = false;
    private bool isPushing = false;
    private Tile tile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(this.name + " has entered block " + collision.gameObject.name);
        tile = collision.gameObject.GetComponent<Tile>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        touch = true;
        if (isPushing)
        {
            tile.countDown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touch = false;
        if (!isPushing)
        {
            tile.resetTimer();
        }
    }

    public void setPosition(float x, float y)
    {
        this.transform.position = new Vector3(x, y, 0);
    }

    public bool isTriggered()
    {
        return touch;
    }

    public void setPush(bool isPushing)
    {
        this.isPushing = isPushing;
    }
}
