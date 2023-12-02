using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private
    private GameObject[,] field;
    private bool alreadyPressed = false;
    private Vector2 worldPosition;

    //Private & Visible in Editor
    [SerializeField] private GameObject segment;
    [SerializeField] private float segmentWidth;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private background backgroundObject;
    [SerializeField] private camera cameraObject;

    private void Start()
    {
        GenerateField(width, height);
        SpawnSegments();
        backgroundObject.resizeBackground(width, height);
        cameraObject.relocateCamera(width, height);
    }

    private void setFPS()
    {
        Application.targetFrameRate = 60;
    }

    private void GenerateField(int pWidth, int pHeight)
    {
        field = new GameObject[pWidth, pHeight];
    }

    private void SpawnSegments()
    {
        for(int i = 0; i < field.GetLength(0); i++)
        {
            for(int j = 0; j < field.GetLength(1); j++)
            {
                field[i, j] = Instantiate(segment, new Vector2(i * segmentWidth, j * segmentWidth), Quaternion.identity);
                field[i, j].GetComponent<segment>().changeStateSpecific(true);
                Debug.Log("i:" + i + " | j: " + j + " | Pos: " + field[i, j].transform.position + " | State: " + field[i, j].GetComponent<segment>().getState());
            }
        }
    }

    private void Update()
    {
        logic();
    }

    private void logic()
    {
        if(Input.touchCount > 0 && !alreadyPressed)
        {
            alreadyPressed = true;
            Touch touch = Input.GetTouch(0);

            Vector2 position = touch.position;
            worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition = new Vector2(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y));
            ChangeState((int)worldPosition.x, (int)worldPosition.y);
        }
        else if(Input.touchCount == 0)
        {
            alreadyPressed = false;
        }
    }

    private void ChangeState(int x, int y)
    {
        if(x >= 0 && x < field.GetLength(0) && y >= 0 && y < field.GetLength(1))
        {
            field[x, y].GetComponent<segment>().changeState();
            y++;
            if (x >= 0 && x < field.GetLength(0) && y >= 0 && y < field.GetLength(1))
            {
                field[x, y].GetComponent<segment>().changeState();
            }
            y--;
            x++;
            if (x >= 0 && x < field.GetLength(0) && y >= 0 && y < field.GetLength(1))
            {
                field[x, y].GetComponent<segment>().changeState();
            }
            y--;
            x--;
            if (x >= 0 && x < field.GetLength(0) && y >= 0 && y < field.GetLength(1))
            {
                field[x, y].GetComponent<segment>().changeState();
            }
            y++;
            x--;
            if (x >= 0 && x < field.GetLength(0) && y >= 0 && y < field.GetLength(1))
            {
                field[x, y].GetComponent<segment>().changeState();
            }
        }
    }

    public GameObject[,] getField()
    {
        return field;
    }
}
