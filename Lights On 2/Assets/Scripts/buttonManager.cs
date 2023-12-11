using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    //Private
    private bool PauseBtnActive = false;

    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject PauseBtn;
    [SerializeField] private GameObject PauseMenu;

    public void TogglePauseBtn()
    {
        if(PauseBtnActive)
        {
            //PausenMenu is on, the game should go on
            PauseBtnActive = false;
            manager.setCanTouch(true);
            PauseMenu.SetActive(false);
        }
        else
        {
            //Game is running, the pause menu should appear
            PauseBtnActive = true;
            manager.setCanTouch(false);
            PauseMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Restarted", 1);
        SceneManager.LoadScene(0);
    }
}
