using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool touch = false;
    private bool isPushing = false;
    private Tile tile;


    private void Update()
    {
        if (tile != null && touch)
        {
            if (isPushing)
            {
                tile.countDown(this.name);
            }
            else
            {
                if(tile.IsCountingDown()) tile.resetTimer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tile")
        {
            tile = collision.gameObject.GetComponent<Tile>();
        }
        if(collision.tag == "Wall")
        {
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tile")
        {
            touch = true;
        }
        if(collision.tag == "Wall")
        {
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Tile")
        {
            touch = false;
        }    
        if(collision.tag == "Wall")
        {
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
