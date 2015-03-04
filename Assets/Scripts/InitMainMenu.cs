using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Levels;
using UnityEngine;


public class InitMainMenu:MonoBehaviour
    {
    private LevelManager _levelManager;
    public GameObject LoadingScreen;
 
    public List<GameObject> Panels;
    public UISlider Slider;
    private SettingsProvider _settingsProvider;
    private string _currentButtonPlay;

    void Start()
        {
            var settingsObject = GameObject.Find("Managers");
            _levelManager = (settingsObject.GetComponent<LevelManager>());
        _settingsProvider=settingsObject.GetComponent<SettingsProvider>();
                _levelManager.LoadingScreen = LoadingScreen;
                _levelManager.Slider = Slider;
        }

    public void ResumeGame()
    {
        _settingsProvider.ResumeGame();
    }

    public void SetVolumeProgress()
    {
        _settingsProvider.SetVolumeProgress();
    }

    public void EnableOrDisableBackground()
    {
        _settingsProvider.EnableOrDisableBackground();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var currentWindow=GetCurrentWindow();
            if (currentWindow.name == "Main_Panel")
            {
                Exit();
            }
            else
            {
                var buttons = currentWindow.GetComponentsInChildren<UIButton>();
                var back=buttons.FirstOrDefault(x => x.name == "Button - Back");
                back.SendMessage("OnClick");
                
            }
        }
    }

    public void Exit()
    {
        _settingsProvider.Exit();
    }

    private GameObject GetCurrentWindow()
    {
        foreach (var panel in Panels)
        {
            if (panel.transform.position.x == 0 && panel.activeSelf)
            {
                //значит эта панель активна
                return panel;
            }
        }
        return null;
    }

    public void SetClickButton()
    {
        _currentButtonPlay=UIButton.current.name;
    }

}

