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
                PlayerPrefs.SetInt("levelNumber", 0);
            }
            return PlayerPrefs.GetInt("levelNumber");
        }
        set {PlayerPrefs.SetInt("levelNumber",value); }
    }



    }

