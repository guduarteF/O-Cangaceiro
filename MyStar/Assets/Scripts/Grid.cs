using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector3 GraphSize;
    public List<Node> FinalPath;
    public Node[,] NodeArray;
    float Nodediameter;
    public float Noderaius;
    public float fDistanceBetweenNodes;
    public Transform StartPosition;
    public LayerMask WallMask;
    public int iGridSizeX, iGridSizeY;
    void Start()
    {
        Nodediameter = Noderaius * 2;
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, GraphSize);
        if(NodeArray != null)
        {
            foreach (Node n in NodeArray)
            {
                if(n.isWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawCube(n.worldPos,Vector3.one *(Nodediameter - fDistanceBetweenNodes));
            }
        }
    }

    void CreateGrid()
    {
        NodeArray = new Node[iGridSizeX, iGridSizeY];
        for (int i= 0; i<iGridSizeX ; i++)
        {
            for (int j = 0; j < iGridSizeY; j++)
            {
                
                Vector3 nodeworldpoint = new Vector3(i, 1, j);
                Debug.Log(nodeworldpoint);
                bool Wall = true;
                if(Physics.CheckSphere(nodeworldpoint,Noderaius, WallMask))
                {
                    Wall = false;
                }
                NodeArray[i, j] = new Node(i, j, Wall, nodeworldpoint);
            }
        }

    }
}
