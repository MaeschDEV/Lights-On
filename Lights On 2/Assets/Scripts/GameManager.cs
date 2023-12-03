using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private
    private GameObject[,] field = new GameObject[0, 0];
    private bool alreadyPressed = false;
    private Vector2 worldPosition = Vector2.zero;
    private int avgFrameRate = 0;
    private bool canTouch = false;

    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameObject segment;
    [SerializeField] private background backgroundObject;
    [SerializeField] private camera cameraObject;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [Header("Values")]
    [SerializeField] private float segmentWidth;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        setFPS();
        GenerateField(width, height);
        SpawnSegments();
        backgroundObject.resizeBackground(width, height);
        cameraObject.relocateCamera(width, height);
        StartCoroutine(ScrambleVisual(10, 20));
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
                field[i, j].GetComponent<segment>().changeStateSpecific(false);
                Debug.Log("i:" + i + " | j: " + j + " | Pos: " + field[i, j].transform.position + " | State: " + field[i, j].GetComponent<segment>().getState());
            }
        }
    }

    IEnumerator ScrambleVisual(int minAmountScrambles, int maxAmountScrambles)
    {
        canTouch = false;
        int randomAmount = Random.Range(minAmountScrambles, maxAmountScrambles);

        for (int i = 0; i < randomAmount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            int randomX = Random.Range(0, field.GetLength(0));
            int randomY = Random.Range(0, field.GetLength(1));

            ChangeState(randomX, randomY);
        }
        canTouch = true;
    }

    private void Update()
    {
        logic();
        DisplayFPS();
    }

    private void logic()
    {
        if(Input.touchCount > 0 && !alreadyPressed && canTouch)
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

    private void DisplayFPS()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        textMeshProUGUI.text = avgFrameRate.ToString();
    }

    public GameObject[,] getField()
    {
        return field;
    }
}
