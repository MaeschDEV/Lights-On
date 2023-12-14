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
    private bool hasWon = false;
    private bool hasLost = false;

    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameObject segment;
    [SerializeField] private background backgroundObject;
    [SerializeField] private camera cameraObject;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject VictoryMenu;
    [SerializeField] private GameObject DefeatMenu;

    [Header("Values")]
    [SerializeField] private float segmentWidth;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float spawnDelay;
    [SerializeField] private bool TimeRush;
    [SerializeField] private float maxTime;
    [SerializeField] private float maxMoves;
    [SerializeField] private float maxMovesMultiplier;

    private void Start()
    {
        width = PlayerPrefs.GetInt("width");
        height = PlayerPrefs.GetInt("height");
        if(PlayerPrefs.GetInt("GameMode", 0) == 0) //0 = TimeRush, 1 = Counter
        {
            TimeRush = true;
        }
        else
        {
            TimeRush = false;
        }
        maxTime = PlayerPrefs.GetInt("time");
        maxMovesMultiplier = PlayerPrefs.GetFloat("multiplier");
        CheckIfRestarted();
    }

    private void CheckIfRestarted()
    {
        if(PlayerPrefs.GetInt("Restarted", 0) == 1)
        {
            //Game got Restarted, load previous board
            PlayerPrefs.SetInt("Restarted", 0);
            GenerateField(PlayerPrefs.GetInt("pWidth", 4), PlayerPrefs.GetInt("pHeight", 4));
            LoadOldBoard();
            backgroundObject.resizeBackground(PlayerPrefs.GetInt("pWidth", 4), PlayerPrefs.GetInt("pHeight", 4));
            cameraObject.relocateCamera(PlayerPrefs.GetInt("pWidth", 4), PlayerPrefs.GetInt("pHeight", 4));
            maxMoves = PlayerPrefs.GetFloat("moves", 10);
        }
        else
        {
            //Game didn't got restarted, generate new board
            GenerateField(width, height);
            SpawnSegments();
            backgroundObject.resizeBackground(width, height);
            cameraObject.relocateCamera(width, height);
            StartCoroutine(ScrambleVisual(width * 3, width * 6));
        }
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

    private void LoadOldBoard()
    {
        canTouch = false;
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (PlayerPrefs.GetInt("Pos" + i + j) == 1)
                {
                    //Field is On
                    //1 = On
                    Debug.Log("Loaded: On " + i + " | " + j);
                    field[i, j] = Instantiate(segment, new Vector2(i * segmentWidth, j * segmentWidth), Quaternion.identity);
                    field[i, j].GetComponent<segment>().changeStateSpecific(true);
                }
                else
                {
                    //Field is Off
                    //0 = Off
                    Debug.Log("Saved: Off " + i + " | " + j);
                    field[i, j] = Instantiate(segment, new Vector2(i * segmentWidth, j * segmentWidth), Quaternion.identity);
                    field[i, j].GetComponent<segment>().changeStateSpecific(false);
                }
            }
        }
        canTouch = true;
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
            maxMoves = maxMoves + maxMovesMultiplier;
        }
        maxMoves = Mathf.Round(maxMoves);
        canTouch = true;
        SaveCurrentBoard(width, height);
    }

    private void SaveCurrentBoard(int pWidth, int pHeight)
    {
        PlayerPrefs.SetInt("pWidth", pWidth);
        PlayerPrefs.SetInt("pHeight", pHeight);
        PlayerPrefs.SetFloat("moves", maxMoves);
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j].GetComponent<segment>().getState())
                {
                    //Field is On
                    //1 = On
                    Debug.Log("Saved: On " + i + " | " + j);
                    PlayerPrefs.SetInt("Pos" + i + j, 1);
                }
                else
                {
                    //Field is Off
                    //0 = Off
                    Debug.Log("Saved: Off " + i + " | " + j);
                    PlayerPrefs.SetInt("Pos" + i + j, 0);
                }
            }
        }
    }

    private void Update()
    {
        logic();
        GameMode();
        //DisplayFPS();
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
        CheckBoard();

        if(canTouch)
        {
            maxMoves--;
        }
    }

    private void DisplayFPS()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        textMeshProUGUI.text = "FPS: " + avgFrameRate.ToString();
    }

    private void CheckBoard()
    {
        if (canTouch)
        {
            hasWon = true;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j].GetComponent<segment>().getState())
                    {
                        //This Segment is On!
                        //Player hasn't won yet!
                        hasWon = false;
                        break;
                    }
                    else
                    {
                        //This Segment is Off!
                    }
                }
            }

            if (hasWon)
            {
                Victory();
            }
        }
    }

    private void GameMode()
    {
        if (TimeRush)
        {
            UpdateTime();
            textMeshProUGUI.text = "Time Left: " + (int)maxTime;
            if(maxTime <= 0 && !hasLost)
            {
                Defeat();
                hasLost = true;
            }
        }
        else
        {
            textMeshProUGUI.text = "Moves Left: " + Mathf.Round(maxMoves);
            if (maxMoves <= 0 && !hasLost)
            {
                Defeat();
                hasLost = true;
            }
        }
    }

    private void UpdateTime()
    {
        if(canTouch)
        {
            maxTime = maxTime -= Time.deltaTime;
        }
    }

    private void Victory()
    {
        canTouch = false;
        Debug.Log("You Won!");
        VictoryMenu.SetActive(true);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Win");
    }

    private void Defeat()
    {
        canTouch = false;
        Debug.Log("You Loose!");
        DefeatMenu.SetActive(true);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySFX("Lose");
    }

    public GameObject[,] getField()
    {
        return field;
    }

    public void setCanTouch(bool pCanTouch)
    {
        canTouch = pCanTouch;
    }
}
