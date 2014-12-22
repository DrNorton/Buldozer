using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class SettingsProvider : MonoBehaviour {
    private List<string> _levels;
    private int _currentLevelIndex=0;
    
    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	    _levels = new List<string>();
	    LoadLevelsInArray();
	}

    public string GetCurrentLevel()
    {
        return _levels.ElementAt(_currentLevelIndex);
    }

    

    public bool SetCurrentLevelIndex(int index)
    {
        _currentLevelIndex = index;
        return true;
    }

    public void LoadNextLevel()
    {
        IncrementLevel();
        Application.LoadLevel("Level1"); 
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
        var level1 = "555555555555" +
        "555555555555" +
            "555544455555" +
            "555543444455" +
            "554442023455" +
            "554302144455" +
            "554444245555" +
            "555554345555" +
            "555554445555" +
            "555555555555";

        var level2 = "340000000000" +
        "340000000000" +
            "040000000040" +
            "040020000040" +
            "040200000040" +
            "040000100040" +
            "040000000040" +
            "040000002040" +
            "000200000043" +
            "000000000043";
        _levels.Add(level1);
        _levels.Add(level2);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
