using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class segment : MonoBehaviour
{
    //Private
    private bool segmentEnabled;

    //Private & Visible in Editor
    [SerializeField] private Sprite frameOn;
    [SerializeField] private Sprite frameOff;

    private void stateChanged()
    {
        if(segmentEnabled)
        {
            GetComponent<SpriteRenderer>().sprite = frameOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = frameOff;
        }
    }

    public void changeState(bool pSegmentEnabled)
    {
        segmentEnabled = pSegmentEnabled;
        stateChanged();
    }

    public bool getState()
    {
        return segmentEnabled;
    }
}
