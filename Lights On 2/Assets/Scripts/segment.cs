using UnityEngine;

public class segment : MonoBehaviour
{
    //Private
    private bool segmentEnabled;

    //Private & Visible in Editor
    [Header("References")]
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

    public void changeState()
    {
        segmentEnabled = !segmentEnabled;
        stateChanged();
    }

    public void changeStateSpecific(bool pSegmentEnabled)
    {
        segmentEnabled = pSegmentEnabled;
        stateChanged();
    }

    public bool getState()
    {
        return segmentEnabled;
    }
}
