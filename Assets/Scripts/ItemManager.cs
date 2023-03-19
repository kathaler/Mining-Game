using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float influenceRange = 100f;
    [SerializeField] private float intensity = .5f;
    [SerializeField] private float distanceToPlayer = 10f;
    [SerializeField] private float maxSpeed = 5f;

    Vector2 pullForce;
    Rigidbody2D body;




    // Start is called before the first frame update
    void Start()
    {
        int o = player.GetOrientation();
        body = this.GetComponent<Rigidbody2D>();
        if(o == 0) {
            body.velocity = new Vector2(NextFloat(-10f, 10f), NextFloat(-1f, 1f));
        } 
        else {
            body.velocity = new Vector2(NextFloat(-1f, 1f), NextFloat(-10f, 10f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(body.velocity.magnitude > maxSpeed)
         {
                body.velocity = body.velocity.normalized * maxSpeed;
         }
        if(Vector3.Distance(transform.position, player.transform.position) < influenceRange) {
            pullForce = (player.transform.position- transform.position).normalized / distanceToPlayer * intensity;
            body.AddForce(pullForce, ForceMode2D.Force);
        }
    }

    static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
