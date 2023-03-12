using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    private float vert;
    private float horiz;
    Sensor up, down, left, right;

    private bool canMove = false;

    private void Start()
    {
        movePoint.parent = null;
        up = GameObject.Find("Up").GetComponent<Sensor>();
        down = GameObject.Find("Down").GetComponent<Sensor>();
        left = GameObject.Find("Left").GetComponent<Sensor>();
        right = GameObject.Find("Right").GetComponent<Sensor>();
    }
    private void Update()
    {
        up.setPosition(this.transform.position.x, this.transform.position.y + 1);
        down.setPosition(this.transform.position.x, this.transform.position.y - 1);
        left.setPosition(this.transform.position.x - 1, this.transform.position.y);
        right.setPosition(this.transform.position.x + 1, this.transform.position.y);

        vert = Input.GetAxisRaw("Vertical");
        horiz = Input.GetAxisRaw("Horizontal");


        if(up.isTriggered() && vert == 1f)
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
    public int GetX()
    {
        return (int)this.transform.position.x;
    }
    public int GetY()
    {
        return (int)this.transform.position.y;
    }
}
