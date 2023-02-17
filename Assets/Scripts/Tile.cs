using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public float time = 10;
    private float current_time;
    public TextMeshPro text;
    public Sprite[] sprites;
    private SpriteRenderer r;
    private int[] pos;
    private int face;

    void Start()
    {
        current_time = time;
        text.text = "";

        r = this.GetComponent<SpriteRenderer>();
        r.sprite = sprites[0];
    }

    void Update()
    {
        if(current_time <= 0)
        {
            Blackboard.DestroyTile(this);
            Destroy(gameObject);
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

    public void setPosition(int[] p)
    {
        this.pos = p; 
    }

    public int[] GetPosition()
    {
        return pos;
    }

    public void setFace(int face)
    {
        this.face = face;
        r.sprite = sprites[face];
    }

    public int getFace()
    {
        return face;
    }
}
