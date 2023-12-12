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
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GamemodeMenu;
    [SerializeField] private GameObject DifficultyMenu;
    [SerializeField] private GameObject CreditsMenu;

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

    public void NewGame()
    {
        PlayerPrefs.SetInt("Restarted", 0);
        SceneManager.LoadScene(0);
    }

    public void PlayBtn()
    {
        MainMenu.SetActive(false);
        GamemodeMenu.SetActive(true);
    }

    public void GamemodeBtn(int index)
    {
        GamemodeMenu.SetActive(false);
        DifficultyMenu.SetActive(true);
        PlayerPrefs.SetInt("GameMode", index); //0 = TimeRush, 1 = Counter
    }

    public void DifficultyBtn(string index)
    {
        int width = int.Parse(index.Substring(0, 1));
        int height = int.Parse(index.Substring(1, 1));
        int time = int.Parse(index.Substring(2, 2));
        float multiplier = int.Parse(index.Substring(4, 2));
        multiplier = multiplier / 10;
        DifficultyMenu.SetActive(false);
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("time", time);
        PlayerPrefs.SetFloat("multiplier", multiplier);
    }

    public void BackToMainMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void BackToPlayMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        GamemodeMenu.SetActive(true);
    }

    public void GoToCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }

    public void SwapToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
