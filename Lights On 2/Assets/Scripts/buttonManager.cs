using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    //Private
    private bool PauseBtnActive = false;
    private bool MusicBtnOn = true;
    private bool SFXBtnOn = true;

    //Private & Visible in Editor
    [Header("References")]
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject PauseBtn;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GamemodeMenu;
    [SerializeField] private GameObject DifficultyMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject CreditsMenu;
    [SerializeField] private GameObject LeaderboardMenu;
    [SerializeField] private GameObject MusicBtn;
    [SerializeField] private GameObject SFXBtn;
    [SerializeField] private Sprite MusicOn;
    [SerializeField] private Sprite MusicOff;
    [SerializeField] private Sprite SFXOn;
    [SerializeField] private Sprite SFXOff;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private leaderboardManager leaderboardManager;

    private void Start()
    {
        setFPS();
    }

    private void Update()
    {
        CheckBackButton();
    }

    private void setFPS()
    {
        Application.targetFrameRate = 60;
    }

    private void CheckBackButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("Escape!");
            //User Pressed Back Button in any Form!
            if (GamemodeMenu.activeInHierarchy)
            {
                //Zurück zum MainMenu
                BackToMainMenu(GamemodeMenu);
                AudioManager.Instance.PlaySFX("Click");
            }
            else if (DifficultyMenu.activeInHierarchy)
            {
                //Zurück zum GamemodeMenu
                BackToPlayMenu(DifficultyMenu);
                AudioManager.Instance.PlaySFX("Click");
            }
            else if (CreditsMenu.activeInHierarchy)
            {
                //Zurück zum MainMenu
                BackToMainMenu(CreditsMenu);
                AudioManager.Instance.PlaySFX("Click");
            }
            else if (LeaderboardMenu.activeInHierarchy)
            {
                //Zurück zum MainMenu
                BackToMainMenu(LeaderboardMenu);
                AudioManager.Instance.PlaySFX("Click");
            }
            else if (OptionsMenu.activeInHierarchy)
            {
                //Zurück zum MainMenu
                BackToMainMenu(OptionsMenu);
                AudioManager.Instance.PlaySFX("Click");
            }
            else
            {
                AudioManager.Instance.PlaySFX("Error");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.Instance.PlaySFX("Error");
        }
    }

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
        AudioManager.Instance.PlaySFX("Click");
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Restarted", 1);
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Restarted", 0);
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void PlayBtn()
    {
        MainMenu.SetActive(false);
        GamemodeMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void GamemodeBtn(int index)
    {
        GamemodeMenu.SetActive(false);
        DifficultyMenu.SetActive(true);
        PlayerPrefs.SetInt("GameMode", index); //0 = TimeRush, 1 = Counter
        AudioManager.Instance.PlaySFX("Click");
    }

    public void DifficultyBtn(string index)
    {
        int width = int.Parse(index.Substring(0, 1));
        int height = int.Parse(index.Substring(1, 1));
        int time = int.Parse(index.Substring(2, 2));
        float multiplier = int.Parse(index.Substring(4, 2));
        multiplier = multiplier / 10;
        DifficultyMenu.SetActive(false);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("time", time);
        PlayerPrefs.SetFloat("multiplier", multiplier);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void BackToMainMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        MainMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void BackToPlayMenu(GameObject thisMenu)
    {
        thisMenu.SetActive(false);
        GamemodeMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void GoToOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void GoToCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void GoToLeaderboard()
    {
        MainMenu.SetActive(false);
        LeaderboardMenu.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
        leaderboardManager.GetScoresEasy();
    }

    public void LeaderboardEasy()
    {
        leaderboardManager.GetScoresEasy();
        AudioManager.Instance.PlaySFX("Click");
    }

    public void LeaderboardNormal()
    {
        leaderboardManager.GetScoresNormal();
        AudioManager.Instance.PlaySFX("Click");
    }

    public void LeaderboardHard()
    {
        leaderboardManager.GetScoresHard();
        AudioManager.Instance.PlaySFX("Click");
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void SwapToMainMenu()
    {
        SceneManager.LoadScene(0);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void ToggleMusic()
    {
        MusicBtnOn = !MusicBtnOn;
        AudioManager.Instance.ToggleMusic();
        if(MusicBtnOn)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOn;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOff;
        }
        AudioManager.Instance.PlaySFX("Click");
    }

    public void ToggleSFX()
    {
        SFXBtnOn = !SFXBtnOn;
        AudioManager.Instance.ToggleSFX();
        if (SFXBtnOn)
        {
            SFXBtn.GetComponent<Image>().sprite = SFXOn;
        }
        else
        {
            SFXBtn.GetComponent<Image>().sprite = SFXOff;
        }
        AudioManager.Instance.PlaySFX("Click");
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }

    public void setMusicButton(bool value)
    {
        MusicBtnOn = !value;

        if (MusicBtnOn)
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOn;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = MusicOff;
        }
    }

    public void setSFXButton(bool value)
    {
        SFXBtnOn = !value;

        if (SFXBtnOn)
        {
            SFXBtn.GetComponent<Image>().sprite = SFXOn;
        }
        else
        {
            SFXBtn.GetComponent<Image>().sprite = SFXOff;
        }
    }
}
