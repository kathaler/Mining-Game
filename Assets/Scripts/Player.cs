using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Sprite[] sprites;

    private float vert;
    private float horiz;
    Sensor up, down, left, right;

    private bool canMove = false;

    private int orientation = 0;

    private InventorySystem inventory;

    public SpriteRenderer playerSprite;

    public GameObject torch;


    private void Start()
    {
        movePoint.parent = null;

        up = GameObject.Find("Up").GetComponent<Sensor>();
        down = GameObject.Find("Down").GetComponent<Sensor>();
        left = GameObject.Find("Left").GetComponent<Sensor>();
        right = GameObject.Find("Right").GetComponent<Sensor>();

        inventory = InventorySystem.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnTorch();
        }

        up.setPosition(this.transform.position.x, this.transform.position.y + 1);
        down.setPosition(this.transform.position.x, this.transform.position.y - 1);
        left.setPosition(this.transform.position.x - 1, this.transform.position.y);
        right.setPosition(this.transform.position.x + 1, this.transform.position.y);

        vert = Input.GetAxisRaw("Vertical");
        horiz = Input.GetAxisRaw("Horizontal");

        if (up.isTriggered() && vert == 1f)
        {
            left.setPush(false);
            right.setPush(false);
            down.setPush(false);
            up.setPush(true);
            canMove = false;
        }
        else if (down.isTriggered() && vert == -1f)
        {
            left.setPush(false);
            right.setPush(false);
            up.setPush(false);
            down.setPush(true);
            canMove = false;
        }
        else if (left.isTriggered() && horiz == -1f)
        {
            right.setPush(false);
            up.setPush(false);
            down.setPush(false);
            left.setPush(true);
            canMove = false;
        }
        else if (right.isTriggered() && horiz == 1f)
        {
            left.setPush(false);
            down.setPush(false);
            up.setPush(false);
            right.setPush(true);
            canMove = false;
        }
        else
        {
            left.setPush(false);
            right.setPush(false);
            up.setPush(false);
            down.setPush(false);
            canMove = true;
        }

        ChangeSpriteOrientation(horiz, vert);

        if (canMove && Vector3.Distance(transform.position, movePoint.position) <= .05)
        {
            if (Mathf.Abs(horiz) == 1f)
            {
                Blackboard.MovePlayer();
                movePoint.position += new Vector3(horiz, 0f, 0f);
            }
            else if (Mathf.Abs(vert) == 1f)
            {
                Blackboard.MovePlayer();
                movePoint.position += new Vector3(0f, vert, 0f);
            }

        }


        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    public void SavePlayer()  {
        SaveSystem.SavePlayer(this);
    }

    private void SpawnTorch() {
        Instantiate(torch, transform.position, Quaternion.identity);
    }

    public void LoadPlayer () {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];

        movePoint.position = pos;
        transform.position = pos;
    }

    public int GetOrientation() {
        return orientation;
    }

    private void ChangeSpriteOrientation(float h, float v)
    {
        if (v != 0)
        {
            orientation = 0;
            if (v > 0)
            {
                playerSprite.sprite = sprites[1];
            }
            else
            {
                playerSprite.sprite = sprites[0];
            }
        }
        else if (h != 0)
        {
            orientation = 1;
            if (h > 0)
            {
                playerSprite.sprite = sprites[3];
            }
            else
            {
                playerSprite.sprite = sprites[2];
            }
        }

    }

    public int GetX()
    {
        return (int)this.transform.position.x;
    }
    public int GetY()
    {
        return (int)this.transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Item") {
            print(collision.gameObject.name);
            if(collision.gameObject.name == "GoldItem(Clone)") {
                FindObjectOfType<AudioManager>().Play("GoldCollected", 1.5f, 1.7f);
            }
            else {
                FindObjectOfType<AudioManager>().Play("ItemCollected", 1.5f, 3.5f);
            }
            inventory.Add(collision.gameObject.GetComponent<ItemManagerBeta>().getItem());
            inventory.PrintInventory();
            Destroy(collision.gameObject);
        }
    }
}
