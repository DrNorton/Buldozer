using UnityEngine;
using System.Collections;

public class LevelEventHandler : MonoBehaviour {
    private SettingsProvider _settingsProvider;

    public GameObject SettingsMenu;
    // Use this for initialization
	void Start () {
        _settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
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
        SettingsMenu.SetActive(false);
    }
	// Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsMenu.SetActive(true);
        }

    }

    public void Replay()
    {
        _settingsProvider.RefreshLevel();
        SettingsMenu.SetActive(true);
    }

    public void GoToMenu()
    {
        _settingsProvider.LoadMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
