using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Singleton<UIController>
{
    public Action OnLevelFail;
    public Action OnLevelSuccess;
    
    private PlayerController _playerController;
    
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject restartButton;

    [FoldoutGroup("Screens"), SerializeField] private GameObject startScreen;
    [FoldoutGroup("Screens"), SerializeField] private GameObject gameScreen;
    [FoldoutGroup("Screens"), SerializeField] private GameObject failScreen;

    public void Initialize()
    {
        GetReferences();
        InitSubscribeEvent();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    private void LevelFail()
    {
        gameScreen.SetActive(false);
        failScreen.SetActive(true);
    }

    private void LevelSuccess()
    {
        restartButton.SetActive(true);
    }

    private void JoystickEnabled()
    {
        joystick.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void InitSubscribeEvent()
    {
        _playerController.OnCityEnter += JoystickEnabled;
        OnLevelFail += LevelFail;
        OnLevelSuccess += LevelSuccess;
    }

    private void GetReferences()
    {
        _playerController = PlayerController.Instance;
    }
}
