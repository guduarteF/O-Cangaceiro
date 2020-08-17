using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int nodePosX, nodePosY;
    public bool isWall;
    public int gcost, hcost;
    public int fcost { get{ return gcost + hcost; } }
    public Vector3 worldPos;
    public Node(int c_nodePosX, int c_nodePosY, bool c_isWall, Vector3 c_WorldPos)
    {
        c_nodePosX = nodePosX;
        c_nodePosY = nodePosY;
        c_isWall = isWall;
        c_WorldPos = worldPos;
    }
}
