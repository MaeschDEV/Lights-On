using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class tabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private tabGroup tabGroup;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void setTransform(int position)
    {
        transform.localPosition = new Vector2(transform.localPosition.x, position);
    }
}
