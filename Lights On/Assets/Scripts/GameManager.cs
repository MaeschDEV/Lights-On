using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private
    private GameObject[,] field;

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

    private void logic()
    {

    }

    public GameObject[,] getField()
    {
        return field;
    }
}
