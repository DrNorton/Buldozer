using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
    public class Settings
    {
    public int LevelNumber
    {
        get
        {
            if (!PlayerPrefs.HasKey("levelNumber"))
            {
                PlayerPrefs.SetInt("levelNumber", 20);
            }
            return PlayerPrefs.GetInt("levelNumber");
        }
        set {PlayerPrefs.SetInt("levelNumber",value);PlayerPrefs.Save(); }
    }

    public float Volume
    {
        get
        {
            if (!PlayerPrefs.HasKey("volume"))
            {
                PlayerPrefs.SetFloat("volume", 1);
            }
            return PlayerPrefs.GetFloat("volume");
        }
        set { PlayerPrefs.SetFloat("volume", value); PlayerPrefs.Save(); }
    }

    public int IsMusic
    {
        get
        {
            if (!PlayerPrefs.HasKey("isMusic"))
            {
                PlayerPrefs.SetInt("isMusic", 1);
            }
            return PlayerPrefs.GetInt("isMusic");
        }
        set { PlayerPrefs.SetInt("isMusic", value); PlayerPrefs.Save(); }
    }


    }

