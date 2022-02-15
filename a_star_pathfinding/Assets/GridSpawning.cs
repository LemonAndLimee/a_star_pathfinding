using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    public GameObject squarePrefab;

    int height = 10;
    int width = 15;

    public Vector3 startPos;
    public float interval = 0.5f;

    public int amountOfSolid;

    public Vector2Int startSquare;
    public Vector2Int endSquare;
    public GameObject circlePrefab;

    public List<List<GameObject>> squares = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        startPos.x = -(interval * (width-1) / 2f);
        startPos.y = -(interval * (height-1) / 2f);
        for (int x = 0; x < width; x++)
        {
            List<GameObject> col = new List<GameObject>();
            for (int y = 0; y < height; y++)
            {
                GameObject obj = Instantiate(squarePrefab);
                col.Add(obj);
                Vector3 pos = new Vector3(startPos.x + (x * interval), startPos.y + (y * interval), 0f);
                obj.transform.position = pos;
                SquareLogic squareScript = obj.GetComponent<SquareLogic>();
                squareScript.index = new Vector2Int(x, y);
                if (startSquare == new Vector2Int(x, y))
                {
                    GameObject circle = Instantiate(circlePrefab);
                    circle.transform.position = obj.transform.position;
                    circle.GetComponent<CircleLogic>().startPos = startSquare;
                    circle.GetComponent<CircleLogic>().target = endSquare;
                    squareScript.isStartSquare = true;
                }
                else if (endSquare == new Vector2Int(x, y))
                {
                    squareScript.isEndSquare = true;
                }

                int num = Random.Range(1, 100);
                if (num < amountOfSolid)
                {
                    squareScript.isSolid = true;
                }

            }
            squares.Add(col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
