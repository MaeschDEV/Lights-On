using System.Collections.Generic;
using UnityEngine;

public class tabGroup : MonoBehaviour
{
    [SerializeField] private List<tabButton> tabButtons;
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabActive;
    private tabButton selectedTab;
    [SerializeField] private List<GameObject> objectsToSwap;

    private void Start()
    {
        ResetTabs();
    }

    public void Subscribe(tabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<tabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabSelected(tabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        button.setTransform(600);
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(tabButton button in tabButtons)
        {
            Debug.Log(button.gameObject.name);
            if(selectedTab != null && button == selectedTab)
            {
                button.background.sprite = tabActive;
                button.setTransform(600);
                continue;
            }
            else if(selectedTab == null && button.gameObject.name == "Tab")
            {
                selectedTab = button;
                button.background.sprite = tabActive;
                button.setTransform(600);
                continue;
            }
            button.background.sprite = tabIdle;
            button.setTransform(550);
        }
    }
}
