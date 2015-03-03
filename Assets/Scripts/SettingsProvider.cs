using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SettingsProvider : MonoBehaviour {
    private List<Level> _levels;
    public int _currentLevelIndex=0;
    private Settings _settingsStore;

    AsyncOperation async;

    public GameObject LoadingScreen;

    public GameObject ProgressBar;

    public List<AudioClip> WinSounds;
    public AudioClip SoundStoneAtTarget;
    public GameObject RateMenu;
    public List<Level> Levels
    {
        get { return _levels; }
    }
    private AsyncOperation checkThisForProgress = null;
    public bool IsMusic
    {
        get { return _settingsStore.IsMusic == 1; }
        set { _settingsStore.IsMusic = value ? 1 : 0; }
    }
    public int GetShowingLevelsNumber()
    {
        return _settingsStore.LevelNumber;
    }

    private static SettingsProvider instance;
    private UISlider _slider;

// EVENTS
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
	    var settingsObject = GameObject.Find("SettingsObject");
	    _slider=ProgressBar.GetComponent<UISlider>();
        
	    if (settingsObject != null)
	    {
	       
	        _levels = new List<Level>();
	        _settingsStore = new Settings();
	      
	        _currentLevelIndex = _settingsStore.LevelNumber;
	        var audio = this.GetComponent<AudioSource>();
	        audio.volume = _settingsStore.Volume;
	        if (_settingsStore.IsMusic == 0)
	        {
	            audio.Stop();
	        }
	        LoadLevelsInArray();
	    }
	    else
	    {
	        
	    }
	}

    public Level GetCurrentLevel()
    {
        return Levels.ElementAt(_currentLevelIndex);
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

    public bool SetCurrentLevelIndex(int index)
    {
        _currentLevelIndex = index;
        return true;
    }

    public int GetCurrentLevelIndex()
    {
        return _currentLevelIndex;
    }

    public void LoadNextLevel()
    {
 
        if (IncrementLevel()) LoadLevel();
    
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

    public  void LoadLevel()
    {
      

        if (_settingsStore.LevelNumber < _currentLevelIndex)
        {
            _settingsStore.LevelNumber = _currentLevelIndex;
        }

        if (_currentLevelIndex >= _levels.Count)
        {
            _currentLevelIndex--;
        }
       
        LoadingScreen.SetActive(true);
        StartNow();

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

    public bool IncrementLevel()
    {
        if (_currentLevelIndex >= Levels.Count-1)
        {
            Application.LoadLevel("End");
            return false;
        }
        _currentLevelIndex++;
        return true;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(_currentLevelIndex==6)
        ShowRateMenu();
    }


    public bool DeincrementLevel()
    {
        _currentLevelIndex--;
        return true;
    }

    private void LoadLevelsInArray()
    {
        var reader = Resources.Load("levels") as TextAsset;
        var levelsText = reader.text;
       var levelsContent=levelsText.Split(new char[] {';'});
        
        int count = 0;
        foreach (var s in levelsContent)
        {
            var infos = s.Split(new char[] {'|'});
            if (infos.Length != 1)
            {
                Levels.Add(new Level() { LevelContent = infos[2], Number = count,ColumnNumbers = int.Parse(infos[0]),ColumnRows = int.Parse(infos[1])});
            }
			else{
				Levels.Add(new Level() { LevelContent = s, Number = count, ColumnNumbers = 16,ColumnRows = 11});
			}
           
            count++;
        }





   
        var level40 = "55555555555555555555555555555555555555444555555555555543444455555555444202345555555543021444555555554444245555555555555434555555555555544455555555555555555555555555555555555555";
        Levels.Add(new Level() { LevelContent = level40, Number = count, ColumnNumbers = 16, ColumnRows = 11 });
  
        
    }

    // Update is called once per frame
	void Update () {
        if (async != null && async.isDone)
        {
           
        }
	}

    private void StartNow()
    {
        AsyncOperation async = Application.LoadLevelAsync("Level1");

        // Set this false to wait changing the scene
        async.allowSceneActivation = false;

        StartCoroutine(LoadLevelProgress(async));
    }


    IEnumerator LoadLevelProgress(AsyncOperation async)
    {
        while (!async.isDone)
        {
            _slider.value = async.progress;
            if (async.progress >= 0.9f)
            {
                // Almost done.
                break;
            }
 
            Debug.Log("Loading " + async.progress);
            yield return null;
        }

        Debug.Log("Loading complete");

        // This is where I'm actually changing the scene
        async.allowSceneActivation = true;
    }
 

    public void RefreshLevel()
    {
        Application.LoadLevel("Level1"); 
    }

    public void LoadMenu()
    {
        Application.LoadLevel("Menu"); 
    }

    public void ResumeGame()
    {
      LoadLevel();
    }
}

public enum SoundType
{
    Win,
    StoneAtPoint
}
