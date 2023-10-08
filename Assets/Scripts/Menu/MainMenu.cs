using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Main")]
    public GameObject mainMenu;
    
    [Header("Play")]
    public GameObject playOptions;
    
    [Header("Options")]
    public GameObject optionsPanel;
    public GameObject backMain;
    public GameObject backPlay;

    [Header("Play - New Game")]
    public string newGameScene;

    [Header("Play - Continue")]
    public string continueScene;

    [Header("Play - Load Game")]
    public string loadScene;

    public void Play()
    {
        mainMenu.SetActive(false);
        playOptions.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        Debug.Log("New Game...");
    }

    public void Continue()
    {
        SceneManager.LoadScene(continueScene);
        Debug.Log("Continue Game...");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadScene);
        Debug.Log("Load Game...");
    }

    public void OptionsMain()
    {
        mainMenu.SetActive(false);
        playOptions.SetActive(false);
        backPlay.SetActive(false);
        backMain.SetActive(true);
        optionsPanel.SetActive(true);
    }

    public void OptionsPlay()
    {
        mainMenu.SetActive(false);
        playOptions.SetActive(false);
        backMain.SetActive(false);
        backPlay.SetActive(true);
        optionsPanel.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Credits...");
    }

    public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void BackMainMenu()
    {
        playOptions.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OptionsBackMainMenu()
    {
        mainMenu.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OptionBackPlay()
    {
        playOptions.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
