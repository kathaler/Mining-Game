using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerBeta : MonoBehaviour
{
    public Item item;

    public Player player;
    Vector2 pullForce;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Awake()
    {
        int o = player.GetOrientation();
        body = this.GetComponent<Rigidbody2D>();
        if(o == 0) {
            body.velocity = new Vector2(NextFloat(-10f, 10f), NextFloat(-1f, 1f));
        } 
        else {
            body.velocity = new Vector2(NextFloat(-1f, 1f), NextFloat(-10f, 10f));
        }
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(body.velocity.magnitude > item.maxSpeed)
         {
                body.velocity = body.velocity.normalized * item.maxSpeed;
         }
        if(Vector3.Distance(transform.position, player.transform.position) < item.influenceRange) {
            pullForce = (player.transform.position- transform.position).normalized / item.distanceToPlayer * item.intensity;
            body.AddForce(pullForce, ForceMode2D.Force);
        }
    }

    static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }

    public Item getItem() {
        return item;
    }
}
