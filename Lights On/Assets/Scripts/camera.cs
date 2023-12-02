using UnityEngine;

public class camera : MonoBehaviour
{
    public void relocateCamera(int pWidth, int pHeight)
    {
        transform.position = new Vector3(pWidth / 2f - 0.5f, pHeight / 2f - 0.5f, -1);
        if(pWidth > pHeight)
        {
            Camera.main.orthographicSize = pWidth + 1;
        }
        else
        {
            Camera.main.orthographicSize = pHeight + 1;
        }
    }
}
