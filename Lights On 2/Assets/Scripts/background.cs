using UnityEngine;

public class background : MonoBehaviour
{
    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameObject TitleImage;

    public void resizeBackground(int pWidth, int pHeight)
    {
        transform.localScale = new Vector2(pWidth + pWidth / 2f, pHeight + pHeight / 2f);
        transform.position = new Vector2(pWidth / 2f - 0.5f, pHeight / 2f - 0.5f);
    }
}
