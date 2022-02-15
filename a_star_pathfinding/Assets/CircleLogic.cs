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
        h = (int)(Mathf.Pow(target.x-currentSquarePos.x, 2) + Mathf.Pow(target.y-currentSquarePos.y, 2));
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

    // Start is called before the first frame update
    void Start()
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
                        if (!closedListCoords.Contains(new Vector2Int(x, y)))
                        {
                            if (x < gridScript.squares.Count)
                            {
                                if (y < gridScript.squares[x].Count)
                                {
                                    GameObject square = gridScript.squares[x][y];
                                    if (square.GetComponent<SquareLogic>().isSolid == false)
                                    {
                                        if (openListCoords.Contains(new Vector2Int(x, y)))
                                        {
                                            int index = openListCoords.IndexOf(new Vector2Int(x, y));
                                            Debug.Log(index);
                                            if (currentSquare.g + 1 < openList[index].g)
                                            {
                                                Vector2Int pos = openList[index].pos;
                                                openList[index] = new node(pos, currentSquare.pos, currentSquare.g, target);
                                            }
                                        }
                                        else
                                        {
                                            node newSquare = new node(new Vector2Int(x, y), currentSquare.pos, currentSquare.g, target);
                                            openList.Add(newSquare);
                                            openListCoords.Add(newSquare.pos);
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
            Debug.Log(index);
            node currentNode = closedList[index];
            while (currentPos != startPos)
            {
                currentPos = currentNode.parentPos;
                index = closedListCoords.IndexOf(currentPos);
                currentNode = closedList[index];
                Debug.Log("boop");
                path.Add(currentPos);

                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
