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

    public List<AudioClip> WinSounds;
    public AudioClip SoundStoneAtTarget;

    public List<Level> Levels
    {
        get { return _levels; }
    }

    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	    _levels = new List<Level>();
	    LoadLevelsInArray();
	}

    public string GetCurrentLevel()
    {
        return Levels.ElementAt(_currentLevelIndex).LevelContent;
    }

    public void SetVolume(float volume)
    {
       var audio=this.GetComponent<AudioSource>();
        audio.volume = volume;
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

    public bool IncrementLevel()
    {
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
            Levels.Add(new Level(){LevelContent = s,Number = count});
            count++;
        }

        var level10 =
         "5555555555555555" +
         "5555444455555555" +
         "5554400455555555" +
         "5554120455555555" +
         "5554420445555555" +
         "5554402045555555" +
         "5554320045555555" +
         "5554336345555555" +
         "5554444445555555" +
         "5555555555555555" +
             "5555555555555555";
        Levels.Add(new Level() { LevelContent = level10, Number = count });
        
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void RefreshLevel()
    {
        Application.LoadLevel("Level1"); 
    }
}

public enum SoundType
{
    Win,
    StoneAtPoint
}
