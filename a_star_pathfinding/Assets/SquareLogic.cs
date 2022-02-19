using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLogic : MonoBehaviour
{
    public Vector2Int index;

    public bool isStartSquare;
    public bool isEndSquare;

    public bool isSolid;
    public bool isPath;

    // Start is called before the first frame update
    void Start()
    {
        if (isStartSquare)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5166429f, 0.7586741f, 0.8490566f);
            isSolid = false;
        }
        else if (isEndSquare)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8509804f, 0.5176471f, 0.5672382f);
            isSolid = false;
        }
        else if (isSolid == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartSquare)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5166429f, 0.7586741f, 0.8490566f);
            isSolid = false;
        }
        else if (isEndSquare)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8509804f, 0.5176471f, 0.5672382f);
            isSolid = false;
        }
        else if (isPath)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (isSolid == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
