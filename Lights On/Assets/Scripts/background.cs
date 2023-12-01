using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public void resizeBackground(int pWidth, int pHeight)
    {
        transform.localScale = new Vector2(pWidth + pWidth * 0.1f, pHeight + pWidth * 0.1f);
        transform.position = new Vector2(pWidth / 2f - 0.5f, pHeight / 2f - 0.5f);
        Debug.Log(transform.position);
    }
}
