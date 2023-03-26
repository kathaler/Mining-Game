using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string type;
    public Sprite sprite;

    public float influenceRange;
    public float intensity;
    public float distanceToPlayer;
    public float maxSpeed;
}
