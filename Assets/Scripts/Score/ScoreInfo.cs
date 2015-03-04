using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Assets.Scripts.Levels;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreInfo : MonoBehaviour
    {
        public int infoWidth = 0;
        public int infoHeight = 0;
        public bool IsVisible { get; set; }
        
        GUIStyle style;
        public Texture2D texture;
        private int _nextLevel;
        private SettingsProvider _settingsProvider;
        private LevelManager _levelManager;
        private GridGenerator _gridGenerator;

        void Start()
        {
            style = new GUIStyle();
            var settingsObject = GameObject.Find("Managers");
            _gridGenerator = settingsObject.GetComponent<GridGenerator>();
            _gridGenerator.Refresh();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowNextLevel(SettingsProvider provider, LevelManager levelManager)
        {
            _settingsProvider = provider;
            _levelManager = levelManager;
            IsVisible = true;
        }
        void OnGUI()
        {
            if (IsVisible)
            {
                style.normal.background = texture;
                //GUI.Box(new Rect(100, 100, 100, 100), new GUIContent(""), style);
                //Debug.Log ("gui warehouse");
                Camera c = Camera.main; //GameObject.FindGameObjectWithTag("MainCamera") as Camera;
                Vector3 pos = c.WorldToViewportPoint(transform.position);
                GUI.BeginGroup(
                    new Rect(pos.x*Screen.width - (infoWidth/2), Screen.height - pos.y*Screen.height - (infoHeight/2),
                        infoWidth, infoHeight), style);
                GUIStyle labelStyle = GUI.skin.GetStyle("label");
                labelStyle.fontSize = 18;

                labelStyle.normal.textColor = new Color32(0, 54, 186, 255);
                var levelIndex = _levelManager.GetCurrentLevelIndex();
                var nextLevel = levelIndex+1;
                GUI.Label(new Rect(70, 50, 250, 30), "You Completed Level "+levelIndex, labelStyle);
                GUI.Label(new Rect(90, 90, 250, 30), "Prepare for Level " + nextLevel, labelStyle);
                if (GUI.Button(new Rect(125, 130, 80, 30), "OK"))
                {
                    _levelManager.IncrementLevel();
                    _gridGenerator.Refresh();
                    IsVisible = false;
                }
                GUI.EndGroup();
            }
        }

    

        
    }
}
