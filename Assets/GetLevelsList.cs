using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class GetLevelsList : MonoBehaviour {

	public GameObject levelPrefab;
	private SettingsProvider _settingsProvider;

	// Use this for initialization
	void Start () {
		_settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
	    var levelNumbers = _settingsProvider.GetShowingLevelsNumber();
		var x = 0;
		foreach (var level in Enumerable.Reverse(_settingsProvider.Levels))
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

		        //gameObject.transform.position=new Vector3(x,0,0);
		        x += 100;
		    }
		}
	}

    private void ButtonClick(UILabel obj)
    {
        _settingsProvider.SetCurrentLevelIndex(int.Parse(obj.text));
        _settingsProvider.LoadLevel();
        Debug.LogWarning(obj.text);
    }
    

    // Update is called once per frame
	void Update () {
	
	}
}
