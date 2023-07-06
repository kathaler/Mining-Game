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
    public Sprite[] sprites;
    public GameObject itemPrefab;


    private int face;
    private bool isDiscovered = false;
    private bool destroyed = false;
    private bool showParticles = false;
    private bool isCountingDown = false;
    private float current_time;
    private SpriteRenderer r;
    private Vector2 globalPosition;
    private ParticleSystem particles;
    // public TileParticleSystem particles;

    void Start()
    {

        current_time = time;

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
            FindObjectOfType<AudioManager>().Play("TileBreaking1", 1.0f, 2.0f);

            CreateItem();
        }

        if (showParticles && !particles.isPlaying)
        {
            particles.Play();
        }
    }

    private void CreateItem() {
        int r = getRandomInt(0, 1);
        for(int i = 0; i <= r; i++) {
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
        if(!isCountingDown) {
            isCountingDown = true;
            FindObjectOfType<AudioManager>().Play("Mining1");
        }
        slider.gameObject.SetActive(true);

        SetSliderPosition(direction);
        current_time -= Time.deltaTime;

        // int intervals = 4;
        // float curr = current_time / intervals;
        // float i = curr*intervals;

        // if(current_time < i) {
        //     FindObjectOfType<AudioManager>().PlayWithRandomPitch("Mining1", 1.0f, 2.0f);
        //     i -= curr;
        // }


        float x = time - current_time;
        float val = x / time;

        slider.value = val;
    }

    public void resetTimer()
    {
        if(isCountingDown) {
            isCountingDown = false;
            FindObjectOfType<AudioManager>().Pause("Mining1");
        }
        slider.value = 0;
        if (slider.IsActive())
        {
            slider.gameObject.SetActive(false);
        }
        current_time = time;
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

    public void Discover() {
        if(this.gameObject.name.Contains("Gold") && !isDiscovered) {
            int a = getRandomInt(0,2);
            if(a == 0) {
                FindObjectOfType<AudioManager>().Play("FoundGold1", 0.8f, 1.2f);
            }
            else {
                FindObjectOfType<AudioManager>().Play("FoundGold2", 0.8f, 1.2f);
            }
        }
        else if(this.gameObject.name.Contains("Iron") && !isDiscovered) {
            int a = getRandomInt(0,2);
            if(a == 0) {
                FindObjectOfType<AudioManager>().Play("FoundIron1", 0.8f, 1.2f);
            }
            else {
                FindObjectOfType<AudioManager>().Play("FoundIron2", 0.8f, 1.2f);
            }
        }
        isDiscovered = true;
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
