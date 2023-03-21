using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Slider slider;
    public float time = 10;
    public TextMeshPro text;
    public Sprite[] sprites;
    public TextMeshProUGUI value;
    public GameObject itemPrefab;


    private int face;
    private bool destroyed = false;
    private bool showParticles = false;
    private bool isCountingDown = false;
    private float current_time;
    private SpriteRenderer r;
    private Vector2 globalPosition;
    private ParticleSystem particles;
    // private TileParticleSystem particles;

    void Start()
    {

        current_time = time;
        text.text = "";

        r = this.GetComponent<SpriteRenderer>();
        if (this.transform.position.y == 0)
        {
            r.sprite = sprites[1];
        }
        else
        {
            r.sprite = sprites[0];
        }

        particles = GetComponent<ParticleSystem>();
        particles.Stop();

        slider.gameObject.SetActive(false);
        // particles = GetComponent<TileParticleSystem>();

    }

    void Update()
    {
        if (current_time <= 0)
        {
            slider.gameObject.SetActive(false);
            Blackboard.DestroyTile(this);
            this.Deactivate();
            this.Destroyed();
            if(value != null) {
                int x = Int32.Parse(value.text);
                value.text = (x + 1).ToString();
            }
            CreateItem();
        }

        if (showParticles && !particles.isPlaying)
        {
            particles.Play();
        }
    }

    private void CreateItem() {
        int r = getRandomInt(1, 2);
        for(int i = 0; i < r; i++) {
            Instantiate(itemPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            
        }
    }

    private void SetSliderPosition(String d)
    {
        if (d == "Up")
        {
            slider.transform.rotation = Quaternion.Euler(0, 0, 0);
            slider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - .55f, 100);
        }
        if (d == "Down")
        {
            slider.transform.rotation = Quaternion.Euler(0, 0, 0);
            slider.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + .55f, 100);

        }
        if (d == "Left")
        {
            slider.transform.rotation = Quaternion.Euler(0, 0, 90);
            slider.transform.position = new Vector3(this.transform.position.x + .55f, this.transform.position.y, 100);
        }
        if (d == "Right")
        {
            slider.transform.rotation = Quaternion.Euler(0, 0, 90);
            slider.transform.position = new Vector3(this.transform.position.x - .55f, this.transform.position.y, 100);
        }
    }

    public void countDown(String direction)
    {
        // StartCoroutine(particles.BurstRoutine());
        // particles.Burst();
        isCountingDown = true;
        slider.gameObject.SetActive(true);

        SetSliderPosition(direction);
        current_time -= Time.deltaTime;

        float x = time - current_time;
        float val = x / time;

        // text.text = Math.Round(current_time, 2).ToString();
        slider.value = val;
    }

    public void resetTimer()
    {
        isCountingDown = false;
        slider.value = 0;
        if (slider.IsActive())
        {
            slider.gameObject.SetActive(false);
        }
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

    public void setFace(int face)
    {
        this.face = face;
        r.sprite = sprites[face];
    }

    public int getFace()
    {
        return face;
    }

    public bool IsOutOfView(Vector2 pos, int viewPortWidth, int viewPortHeight)
    {
        if (Math.Abs(this.globalPosition.x - pos.x) > viewPortWidth / 2 || Math.Abs(this.globalPosition.y - pos.y) > viewPortHeight / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowParticles()
    {
        showParticles = true;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        if (!showParticles && this.particles != null)
        {
            this.particles.Stop();
        }
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

    public bool IsCountingDown()
    {
        return isCountingDown;
    }

    public static int getRandomInt(int a, int b)
    {
        System.Random rnd = new System.Random();
        return rnd.Next(a, b);
    }


}
