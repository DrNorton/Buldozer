using System.Collections.Generic;
using Assets.Scripts.Levels;
using UnityEngine;
using System.Collections;
using System.Linq;

public class GetLevelsList : MonoBehaviour {

	public GameObject levelPrefab;
	private SettingsProvider _settingsProvider;
    private LevelManager _levelManager;
    public GameObject LoadingScreen;

    // Use this for initialization
	void Start () {
        var settingsObject = GameObject.Find("Managers");
        _settingsProvider = (SettingsProvider)(settingsObject.GetComponent("SettingsProvider"));
        _levelManager = (settingsObject.GetComponent<LevelManager>());
	    var levelNumbers = _settingsProvider.GetShowingLevelsNumber();
	
		var x = 0;
        foreach (var level in Enumerable.Reverse(_levelManager.Levels))
		{
		    if (level.Number <= levelNumbers)
		    {
		        var levelItem = Instantiate(levelPrefab, new Vector3(x, 0, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
		        var label = levelItem.GetComponent<UILabel>();
		        var button = levelItem.GetComponent<UIButton>();
		        var onClickEvent = new EventDelegate(this, "ButtonClick");
		        var param = onClickEvent.parameters[0];
		        param.obj = label;
		        button.onClick.Add(onClickEvent);
		        label.text = level.Number.ToString();
		        levelItem.transform.SetParent(this.transform, false);
		        x += 100;
		    }
		}
	}

    private void ButtonClick(UILabel obj)
    {
        _levelManager.SetCurrentLevelIndex(int.Parse(obj.text));
        _levelManager.LoadLevel();
        Debug.LogWarning(obj.text);
    }
    

    // Update is called once per frame
	void Update () {
	
	}
}
