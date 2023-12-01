using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private
    private GameObject[,] field;

    //Private & Visible in Editor
    [SerializeField] private GameObject segment;
    [SerializeField] private float segmentWidth;

    private void Start()
    {
        GenerateField(5, 5);
        SpawnSegments();
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
                field[i, j].GetComponent<segment>().changeState(false);
                Debug.Log("i:" + i + " | j: " + j + " | Pos: " + field[i, j].transform.position + " | State: " + field[i, j].GetComponent<segment>().getState());
            }
        }
    }
}
