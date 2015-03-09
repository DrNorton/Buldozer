using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Levels;

using UnityEngine;
using System.Collections;

public class LevelEventHandler : MonoBehaviour {
    private SettingsProvider _settingsProvider;

    public GameObject SettingsMenu;

    public GameObject HowToPlay;

    public List<GameObject> Panels;
   
    private WINRTInterfaceHandler _winrtHandler;
    private LevelManager _levelManager;

    // Use this for initialization
	void Start ()
	{
        var settingsObject = GameObject.Find("Managers");
        _settingsProvider = (SettingsProvider)(settingsObject.GetComponent("SettingsProvider"));
	    _levelManager = (settingsObject.GetComponent<LevelManager>());
        if (_levelManager.GetCurrentLevelIndex() == 0)
	    {
	        ShowHowToPlayRequest();
	    }
        _winrtHandler = (WINRTInterfaceHandler)(GameObject.Find("Managers").GetComponent("WINRTInterfaceHandler"));
	}

    private void ShowHowToPlayRequest()
    {
        HowToPlay.SetActive(true);
    }

    public void HowToPlayClose()
    {
        HowToPlay.SetActive(false);
    }
    
    public void SetVolume()
    {
        _settingsProvider.SetVolumeProgress();
    }

    public void EnableOrDisableMusic()
    {
        _settingsProvider.EnableOrDisableBackground();
    }

    public void Resume()
    {
        UnshowMenu();
    }
	// Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HowToPlay.activeSelf)
            {
                HowToPlayClose();
                return;
                
            }

            if (!SettingsMenu.activeSelf)
            {
                ShowMenu();
            }
            else
            {
                var currentWindow = GetCurrentWindow();
                if (currentWindow.name == "Main_Panel")
                {
                    UnshowMenu();
                }
                else
                {
                    var buttons = currentWindow.GetComponentsInChildren<UIButton>();
                    var back = buttons.FirstOrDefault(x => x.name == "Button - Back");
                    back.SimulateClick(); 
                }
                
            }

           
        }
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

    private void UnshowMenu()
    {
        _winrtHandler.SendRequest(1, "unshowBanner", requestCallback);
        SettingsMenu.SetActive(false);
    }

    private void ShowMenu()
    {
        _winrtHandler.SendRequest(1, "showBanner", requestCallback);
        SettingsMenu.SetActive(true);
    }

  

    public void Replay()
    {
        _levelManager.RefreshLevel();
        UnshowMenu();
       // SettingsMenu.SetActive(true);
    }

    public void GoToMenu()
    {
        UnshowMenu();
        _settingsProvider.LoadMenu();
    }

    private void requestCallback(int mirequestid, string strrequesteddata, string result)
    {
        Debug.LogWarning("test");
    }

   
}
