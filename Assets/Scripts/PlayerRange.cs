using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    public float scale = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(scale, scale, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
