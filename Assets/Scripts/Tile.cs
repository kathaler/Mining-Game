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
    //public Text text;

    // Start is called before the first frame update
    void Start()
    {
        current_time = time;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(current_time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        current_time -= Time.deltaTime;
        text.text = Math.Round(current_time,2).ToString();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        current_time = time;
        text.text = "";
    }
}
