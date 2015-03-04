using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class LevelManager:MonoBehaviour
    {
        private List<Level> _levels;
        public UISlider Slider { get; set; }
        private Settings _settingsStore;
     
        private static LevelManager instance;

        public GameObject LoadingScreen { get; set; }
        public int _currentLevelIndex = 0;

        public List<Level> Levels
        {
            get { return _levels; }
        }

        public Level GetCurrentLevel()
        {
            return Levels.ElementAt(_currentLevelIndex);
        }

        public LevelManager()
        {
        
        }

        void Start()
        {
            _settingsStore = new Settings();
            _currentLevelIndex = _settingsStore.LevelNumber;
            
          
            _levels = new List<Level>();
            LoadLevelsInArray();
        }


        void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
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

        public void LoadLevel()
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

        private void StartNow()
        {
            AsyncOperation async = Application.LoadLevelAsync("Level1");

            // Set this false to wait changing the scene
            async.allowSceneActivation = false;

            StartCoroutine(LoadLevelProgress(async));
        }

        public bool IncrementLevel()
        {
            if (_currentLevelIndex >= Levels.Count - 1)
            {
                Application.LoadLevel("End");
                return false;
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
            var levelsContent = levelsText.Split(new char[] { ';' });

            int count = 0;
            foreach (var s in levelsContent)
            {
                var infos = s.Split(new char[] { '|' });
                if (infos.Length != 1)
                {
                    Levels.Add(new Level() { LevelContent = infos[2], Number = count, ColumnNumbers = int.Parse(infos[0]), ColumnRows = int.Parse(infos[1]) });
                }
                else
                {
                    Levels.Add(new Level() { LevelContent = s, Number = count, ColumnNumbers = 16, ColumnRows = 11 });
                }

                count++;
            }






            var level40 = "55555555555555555555555555555555555555444555555555555543444455555555444202345555555543021444555555554444245555555555555434555555555555544455555555555555555555555555555555555555";
            Levels.Add(new Level() { LevelContent = level40, Number = count, ColumnNumbers = 16, ColumnRows = 11 });


        }

        IEnumerator LoadLevelProgress(AsyncOperation async)
        {
            while (!async.isDone)
            {
                Slider.value = async.progress;
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
          
            var grid = this.GetComponent<GridGenerator>();
            grid.Refresh();
         
        }
    }
}
