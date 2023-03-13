using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private float current_time;
    private SpriteRenderer r;
    private Vector2 globalPosition;
    //private Vector2 localPosition;
    private int face;
    private bool destroyed = false;

    public float time = 10;
    public TextMeshPro text;
    public Sprite[] sprites;

    void Start()
    {
        current_time = time;
        text.text = "";

        r = this.GetComponent<SpriteRenderer>();
        if(this.transform.position.y == 0)
        {
            r.sprite = sprites[1];
        }
        else
        {
            r.sprite = sprites[0];
        }
    }

    void Update()
    {
        if(current_time <= 0)
        {
            Blackboard.DestroyTile(this);
            this.Deactivate();
            this.Destroyed();
        }
    }

    public void countDown()
    {
        //print(this.gameObject.name + " is counting down: " + current_time);
        current_time -= Time.deltaTime;
        text.text = Math.Round(current_time, 2).ToString();
    }

    public void resetTimer()
    {
        current_time = time;
        text.text = "";
    }

    public void setGlobalPosition(Vector2 p)
    {
        this.globalPosition = p; 
    }

    public Vector2 GetGlobalPosition()
    {
        return globalPosition;
    }

    //public Vector2 setLocalPosition(int[] p)
    //{
    //    this.localPosition = p;
    //}

    //public int[] getLocalPosition()
    //{
    //    return localPosition;
    //}

    public void setFace(int face)
    {
        this.face = face;
        r.sprite = sprites[face];
    }

    public int getFace()
    {
        return face;
    }

    public bool IsOutOfView(Vector2 pos)
    {
        if(Math.Abs(this.globalPosition.x - pos.x) > 10 || Math.Abs(this.globalPosition.y - pos.y) > 10 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    public void Destroyed()
    {
        this.destroyed = true;
    }
    public bool WasDestroyed()
    {
        return this.destroyed;
    }
}
