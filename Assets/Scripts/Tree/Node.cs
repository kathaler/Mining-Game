using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector2 pos;
    private Node parent;
    private Node left;
    private Node center;
    private Node right;

    public Node(Vector2 pos, Node parent)
    {
        this.pos = pos;
        this.parent = parent;
    }



}
