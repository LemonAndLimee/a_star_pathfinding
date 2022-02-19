using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct node
{
    public Vector2Int pos;
    public Vector2Int parentPos;
    public int g;
    public int h;
    public int f;
    public node(Vector2Int position, Vector2Int currentSquarePos, int parentG, Vector2Int target)
    {
        pos = position;
        parentPos = currentSquarePos;
        g = parentG + 1;
        h = (int)(Mathf.Pow(target.x-pos.x, 2) + Mathf.Pow(target.y-pos.y, 2));
        f = g + h;
    }
}

public class CircleLogic : MonoBehaviour
{
    public Vector2Int startPos;
    public Vector2Int target;

    public List<node> openList = new List<node>();
    public List<node> closedList = new List<node>();

    public List<Vector2Int> openListCoords = new List<Vector2Int>();
    public List<Vector2Int> closedListCoords = new List<Vector2Int>();

    public GridSpawning gridScript;
    public bool found;

    public List<Vector2Int> path = new List<Vector2Int>();

    void Start()
    {
        //Run();
    }

    // Start is called before the first frame update
    public void Run()
    {
        gridScript = GameObject.Find("GameManager").GetComponent<GridSpawning>();

        node startNode = new node(startPos, Vector2Int.zero, 0, target);
        openList.Add(startNode);
        openListCoords.Add(startNode.pos);

        while (openList.Count > 0 && !closedListCoords.Contains(target))
        {
            int lowestF = -1;
            node lowestFNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (lowestF == -1)
                {
                    lowestF = openList[i].f;
                    lowestFNode = openList[i];
                }
                else if (openList[i].f < lowestF)
                {
                    lowestF = openList[i].f;
                    lowestFNode = openList[i];
                }
            }
            node currentSquare = lowestFNode;
            openList.Remove(currentSquare);
            openListCoords.Remove(currentSquare.pos);
            closedList.Add(currentSquare);
            closedListCoords.Add(currentSquare.pos);

            if (currentSquare.pos == target)
            {
                found = true;
            }
            else
            {
                for (int x = currentSquare.pos.x - 1; x <= currentSquare.pos.x + 1; x++)
                {
                    for (int y = currentSquare.pos.y - 1; y <= currentSquare.pos.y + 1; y++)
                    {
                        Debug.Log(new Vector2(x, y) + ": " + closedListCoords.IndexOf(new Vector2Int(x, y)));
                        if (!closedListCoords.Contains(new Vector2Int(x, y)))
                        {
                            if (x >= 0 && x < gridScript.width)
                            {
                                if (y >= 0 && y < gridScript.height)
                                {
                                    if(x == 8 && y == 8)
                                    {
                                        Debug.Log("test0");
                                    }
                                    GameObject square = gridScript.squares[x][y];
                                    if (square.GetComponent<SquareLogic>().isSolid == false)
                                    {
                                        if (x == 8 && y == 8)
                                        {
                                            Debug.Log("test1");
                                        }
                                        if (openListCoords.Contains(new Vector2Int(x, y)))
                                        {
                                            int index = openListCoords.IndexOf(new Vector2Int(x, y));
                                            if (currentSquare.g + 1 < openList[index].g)
                                            {
                                                Vector2Int pos = openList[index].pos;
                                                openList[index] = new node(pos, currentSquare.pos, currentSquare.g, target);
                                                Debug.Log("update");
                                                DisplayNodeStats(openList[index]);
                                            }
                                        }
                                        else
                                        {
                                            node newSquare = new node(new Vector2Int(x, y), currentSquare.pos, currentSquare.g, target);
                                            openList.Add(newSquare);
                                            openListCoords.Add(newSquare.pos);
                                            DisplayNodeStats(newSquare);
                                        }

                                    }
                                }
                                
                            }
                            
                        }
                    }
                }
            }
            
        }
        
        if (found)
        {
            Vector2Int currentPos = target;
            int index = closedListCoords.IndexOf(target);
            node currentNode = closedList[index];
            while (currentPos != startPos)
            {
                currentPos = currentNode.parentPos;
                index = closedListCoords.IndexOf(currentPos);
                currentNode = closedList[index];
                path.Add(currentPos);

                if (currentPos != startPos)
                {
                    gridScript.squares[currentNode.pos.x][currentNode.pos.y].GetComponent<SquareLogic>().isPath = true;
                }
            }
        }
    }

    void DisplayNodeStats(node n)
    {
        //Debug.Log(new Vector2(target.x - n.pos.x, target.y - n.pos.y));
        Debug.Log(n.pos.x.ToString() + "," + n.pos.y.ToString() + ": " + n.f + "," + n.g + "," + n.h + ": " + n.parentPos);
    }

    public void ErasePath()
    {
        for (int i = path.Count-1; i >= 0; i--)
        {
            Vector2Int pos = path[i];
            gridScript.squares[pos.x][pos.y].GetComponent<SquareLogic>().isPath = false;
            path.RemoveAt(i);
        }
        openList.Clear();
        openListCoords.Clear();
        closedList.Clear();
        closedListCoords.Clear();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
