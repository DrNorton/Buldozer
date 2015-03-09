using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Levels;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SettingsProvider : MonoBehaviour {

    private Settings _settingsStore;
    AsyncOperation async;
    private static SettingsProvider instance;
   

   
    public GameObject ProgressBar;
    public List<AudioClip> WinSounds;
    public AudioClip SoundStoneAtTarget;
    public GameObject RateMenu;
    private LevelManager _levelManager;

    public bool IsMusic
    {
        get { return _settingsStore.IsMusic == 1; }
        set { _settingsStore.IsMusic = value ? 1 : 0; }
    }

    public int GetShowingLevelsNumber()
    {
        return _settingsStore.LevelNumber;
    }

    void Awake ()
{
    if (instance)
    {
        Destroy (gameObject);
    }
    else
    {
        instance = this;
        DontDestroyOnLoad (gameObject);
    }
}

    // Use this for initialization
	void OnEnable ()
	{
        var settingsObject = GameObject.Find("Managers");
	   
      
	    if (settingsObject != null)
	    {
	        _settingsStore = new Settings();
	        _levelManager = this.GetComponent<LevelManager>();
	        var audio = this.GetComponent<AudioSource>();
	        audio.volume = _settingsStore.Volume;
	        if (_settingsStore.IsMusic == 0)
	        {
	            audio.Stop();
	        }
	    }
	    else
	    {
	        
	    }
	}


    public void SetVolume(float volume)
    {
       var audio=this.GetComponent<AudioSource>();
        audio.volume = volume;
    }

    public void SetVolumeProgress()
    {

        if (UIProgressBar.current != null)
        {
            var audio = this.GetComponent<AudioSource>();
            audio.volume = UIProgressBar.current.value;
            _settingsStore.Volume = audio.volume;
        }
            
    }

    public float GetVolume()
    {
        var audio = this.GetComponent<AudioSource>();
        return audio.volume;
    }

    public void Play(SoundType type)
    {
        var audio = this.GetComponent<AudioSource>();
        switch (type)
        {
                case SoundType.Win:
                var index = Random.Range(0, WinSounds.Count-1);
                audio.PlayOneShot(WinSounds.ElementAt(index));
                break;

                case  SoundType.StoneAtPoint:
                audio.PlayOneShot(SoundStoneAtTarget);
                break;
        }
    }
    public void ShowRateMenu()
    {
        var levelItem = Instantiate(RateMenu, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
        levelItem.SetActive(true);
    }
    public void UnShowRateMenu()
    {
        RateMenu.SetActive(false);
    }
	public void Exit(){
		Application.Quit();
	}
    public void ChangeBackgroundMusic(bool isMusic)
    {
        var audio = this.GetComponent<AudioSource>();
        if (!isMusic)
        {
			if(audio.isPlaying){
             audio.Stop();
			 _settingsStore.IsMusic = 0;
			}
        }
        else
        {
			if(!audio.isPlaying){
            audio.Play();
            _settingsStore.IsMusic = 1;
			}
        }
       
    }
    public void EnableOrDisableBackground()
    {
        if (UIToggle.current != null)
        {
            ChangeBackgroundMusic(UIToggle.current.value);
        }
    }


    //private void OnLevelWasLoaded(int level)
    //{
    //    if (LevelManager.GetCurrentLevelIndex() == 6)
    //        ShowRateMenu();
    //}

    
    // Update is called once per frame
	void Update () {
        if (async != null && async.isDone)
        {
           
        }
	}

    public void LoadMenu()
    {
        Application.LoadLevel("Menu"); 
    }

    public void ResumeGame()
    {
        _levelManager.LoadLevel();
    }
}

public enum SoundType
{
    Win,
    StoneAtPoint
}


