using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public void relocateCamera(int pWidth, int pHeight)
    {
        transform.position = new Vector3(pWidth / 2f - 0.5f, pHeight / 2f - 0.5f, -1);
    }
}
