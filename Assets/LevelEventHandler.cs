using UnityEngine;
using System.Collections;

public class LevelEventHandler : MonoBehaviour {
    private SettingsProvider _settingsProvider;

    public GameObject SettingsMenu;
   
    private WINRTInterfaceHandler _winrtHandler;
    // Use this for initialization
	void Start () {
        _settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
        _winrtHandler = (WINRTInterfaceHandler)(GameObject.Find("SettingsObject").GetComponent("WINRTInterfaceHandler"));
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
            if (!SettingsMenu.activeSelf)
            {
                ShowMenu();
            }
            else
            {
                UnshowMenu();
            }
        }
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
        _settingsProvider.RefreshLevel();
        UnshowMenu();
        SettingsMenu.SetActive(true);
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

    public void Exit()
    {
        Application.Quit();
    }
}
