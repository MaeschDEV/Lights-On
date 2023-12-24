using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class tabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private tabGroup tabGroup;
    public Image background;
    [SerializeField] private UnityEvent onTabSelected;
    [SerializeField] private UnityEvent onTabDeselected;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select()
    {
        if(onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if(onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    public void setTransform(int position)
    {
        transform.localPosition = new Vector2(transform.localPosition.x, position);
    }
}
