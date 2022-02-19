using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    public GridSpawning gridScript;
    public CircleLogic circleScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(worldMousePosition.x, worldMousePosition.y);

            RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.tag == "Square")
                {
                    raycastHit.collider.gameObject.GetComponent<SquareLogic>().isStartSquare = true;
                    gridScript.squares[gridScript.startSquare.x][gridScript.startSquare.y].GetComponent<SquareLogic>().isStartSquare = false;
                    gridScript.startSquare = raycastHit.collider.gameObject.GetComponent<SquareLogic>().index;
                    circleScript.startPos = gridScript.startSquare;
                    circleScript.ErasePath();
                }
            }
        }
        if (Input.GetKeyDown("2"))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(worldMousePosition.x, worldMousePosition.y);

            RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.tag == "Square")
                {
                    raycastHit.collider.gameObject.GetComponent<SquareLogic>().isEndSquare = true;
                    gridScript.squares[gridScript.endSquare.x][gridScript.endSquare.y].GetComponent<SquareLogic>().isEndSquare = false;
                    gridScript.endSquare = raycastHit.collider.gameObject.GetComponent<SquareLogic>().index;
                    circleScript.target = gridScript.endSquare;
                    circleScript.ErasePath();
                }
            }
        }

        if (Input.GetKeyDown("b"))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(worldMousePosition.x, worldMousePosition.y);

            RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.tag == "Square")
                {
                    circleScript.ErasePath();
                    raycastHit.collider.gameObject.GetComponent<SquareLogic>().isSolid = true;
                }
            }
        }
        if (Input.GetKeyDown("w"))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(worldMousePosition.x, worldMousePosition.y);

            RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.tag == "Square")
                {
                    circleScript.ErasePath();
                    raycastHit.collider.gameObject.GetComponent<SquareLogic>().isSolid = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            circleScript.Run();
        }

    }
}
