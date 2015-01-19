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
    private int _currentLevelIndex=0;
    private Settings _settingsStore;

    public List<AudioClip> WinSounds;
    public AudioClip SoundStoneAtTarget;

    public List<Level> Levels
    {
        get { return _levels; }
    }

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
	    if (settingsObject != null)
	    {
	       
	        _levels = new List<Level>();
	        _settingsStore = new Settings();

	        _currentLevelIndex = _settingsStore.LevelNumber;
	        var audio = this.GetComponent<AudioSource>();
	        audio.volume = _settingsStore.Volume;
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
        IncrementLevel();
        LoadLevel();
    }

	public void Exit(){
		Application.Quit();
	}

    public void LoadLevel()
    {
        if (_settingsStore.LevelNumber < _currentLevelIndex)
        {
            _settingsStore.LevelNumber = _currentLevelIndex;
        }
       
        Application.LoadLevel("Level1");
    }

    public void ChangeBackgroundMusic(bool isMusic)
    {
        var audio = this.GetComponent<AudioSource>();
        if (!isMusic)
        {
			if(audio.isPlaying){
            audio.Stop();
			}
        }
        else
        {
			if(!audio.isPlaying){
            audio.Play();
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
        if (_currentLevelIndex > Levels.Count-1)
        {
            Application.LoadLevel("End");
        }
        _currentLevelIndex++;
        return true;
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



        var level30 =
  "44444444444444444444" +
  "43333000440400004004" +
  "43333000000000204404" +
  "43333404042444200004" +
  "43334000040000000404" +
  "44340042040000024404" +
  "40040040202044400204" +
  "40000020020400404404" +
  "44040440422402400404" +
  "40040002020400000044" +
  "40000400000400400014" +
  "44444444444444444444";
        Levels.Add(new Level() { LevelContent = level30, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level31 =
"44444444444444444444" +
"40004004140440044444" +
"40404002000020044444" +
"40400004444440200444" +
"40004004333340022004" +
"44244244333340000004" +
"40000004333344244244" +
"40022004333340000004" +
"40200200400400444004" +
"44444002000200002004" +
"44444040000400400044" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level31, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level32 =
"44444444444444444444" +
"40000044433404000004" +
"40220044433444002104" +
"40040443333334002004" +
"40000043333334002004" +
"44440044433444444204" +
"40002220433400004004" +
"40240002002002204204" +
"40040044020044004004" +
"40200002044020000204" +
"40040044000044004004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level32, Number = count, ColumnNumbers = 20, ColumnRows = 12 });


        count++;
        var level33 =
"44444444444444444444" +
"40000400404004004004" +
"40140404402000200044" +
"44440400004004020004" +
"40000404404204404404" +
"40000002000200020004" +
"43344422440244204404" +
"43343400402000204004" +
"43333402200044204444" +
"43333400444440000004" +
"43334440000000000004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level33, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level34 =
"44444444444444444444" +
"43333400000004004004" +
"43333404020020000004" +
"43333344002404024204" +
"43334000200024002004" +
"43344440040200022004" +
"40000004444044440444" +
"40000000040004000004" +
"40440004000204020204" +
"40440000204402002004" +
"40000014000004000404" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level34, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level35 =
"44444444444444444444" +
"43333444000000000004" +
"43333444440400424044" +
"43333444000420020004" +
"43333444000020042244" +
"44004444024004202004" +
"44004444002002004004" +
"40004444244424402004" +
"44000010004004002004" +
"44000444004002004444" +
"44444444004004000004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level35, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level36 =
"44444444444444444444" +
"40000040000014333444" +
"40000040000004433344" +
"40404044244044033334" +
"40002040002220033334" +
"44424440220044404434" +
"40000020040000404444" +
"40020040044400404004" +
"44042440000200220004" +
"40002044000400404004" +
"40000040000400400004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level36, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level37 =
"44444444444444444444" +
"40000040043334100004" +
"40400000003333400004" +
"40020040004333340004" +
"40442444404433334004" +
"40200020040043334004" +
"40220400040004022004" +
"44400222400022002004" +
"40200400400004024004" +
"40002400400000002004" +
"40040000400004004004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level37, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level38 =
"44444444444444444444" +
"40000004000400000004" +
"44044404400444404044" +
"40000404002200000004" +
"40040404020402044044" +
"40000001400422040004" +
"40044420400000044044" +
"43343402040020400004" +
"43343400204044022004" +
"43333440002200200404" +
"43333344000000004004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level38, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level39 =
"55554445444444444444" +
"55554144434433344004" +
"55544200334333400004" +
"55440000333333400204" +
"54400204333334404044" +
"54002240444440020204" +
"54024020000440022004" +
"54004004000040200204" +
"54000220444042440004" +
"54024000000202002004" +
"54400004000040000004" +
"55444444444444444444";
        Levels.Add(new Level() { LevelContent = level39, Number = count, ColumnNumbers = 20, ColumnRows = 12 });

        count++;
        var level40 =
"44444444444444444444" +
"40040000004000400044" +
"40240202044333200204" +
"40020040443333402004" +
"40440204433334000204" +
"40200004333344020004" +
"40244004333400000004" +
"40002224424400444044" +
"40404004000400400004" +
"40204002004400000004" +
"40000400004100000004" +
"44444444444444444444";
        Levels.Add(new Level() { LevelContent = level40, Number = count, ColumnNumbers = 20, ColumnRows = 12 });
  
        
    }

    // Update is called once per frame
	void Update () {
	
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
        Application.LoadLevel("Level1"); 
    }
}

public enum SoundType
{
    Win,
    StoneAtPoint
}
