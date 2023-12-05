using UnityEngine;

public class buttonManager : MonoBehaviour
{
    //Private
    bool PauseBtnActive = false;

    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject PauseBtn;

    public void TogglePauseBtn()
    {
        if(PauseBtnActive)
        {
            //PausenMenu is on, the game should go on
            PauseBtnActive = false;
            manager.setCanTouch(false);
            PauseBtn.GetComponent<Animator>().SetTrigger("SwapToPause");
        }
        else
        {
            //Game is running, the pause menu should appear
            PauseBtnActive = true;
            PauseBtn.GetComponent<Animator>().SetTrigger("SwapToPlay");
        }
    }
}
